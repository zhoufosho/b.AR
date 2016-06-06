using UnityEngine;
using System.Collections;

public class LeafController : MonoBehaviour {
    bool IsChangingColor = false;
    public bool ReadyToImplode = false;
    float t = 0.0f;
    Color currentColor;
    Vector3 originalScale = new Vector3(0.5f, 0.5f, 0.5f);

    Material leafMaterialHealthy;
    Material leafMaterialDead;
    Material mat;

    // Use this for initialization
    void Start()
    {
        leafMaterialHealthy = Resources.Load("LeafHealthy", typeof(Material)) as Material;
        leafMaterialDead = Resources.Load("LeafDead", typeof(Material)) as Material;

        mat = Resources.Load("LeafTemp", typeof(Material)) as Material;

        currentColor = Color.Lerp(leafMaterialDead.color, leafMaterialHealthy.color, ClimateManager.GetClimateStrength());
        mat.color = currentColor;
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = mat;
    }

    void Update()
    {
        if (ReadyToImplode)
        {
            t += (Time.deltaTime / 2);

            float newX = originalScale.x * (1.0f - t);
            float newY = originalScale.y * (1.0f - t);
            float newZ = originalScale.z * (1.0f - t);

            Vector3 newScale = new Vector3(newX, newY, newZ);

            this.gameObject.transform.parent.localScale = newScale;
            if (t > 1f) Destroy(gameObject.transform.parent.gameObject);
        }
    }

    void OnEnable()
    {
        ClimateManager.ClimateChange += ChangeLeafColor;
    }

    void OnDisable()
    {
        ClimateManager.ClimateChange -= ChangeLeafColor;
    }

    void ChangeLeafColor()
    {
        if (IsChangingColor) StopCoroutine("LerpColor");

        float climateStrength = ClimateManager.GetClimateStrength();
        Color destinationColor = Color.Lerp(leafMaterialDead.color, leafMaterialHealthy.color, climateStrength);
        mat.color = destinationColor;
    }

    IEnumerator LerpColor()
    {
        float climateStrength = ClimateManager.GetClimateStrength();
        Color destinationColor = Color.Lerp(leafMaterialDead.color, leafMaterialHealthy.color, climateStrength);
        Color startColor = new Color(currentColor.r, currentColor.g, currentColor.b);
        IsChangingColor = true;

        float t = 0f;
        while (t <= 1f)
        {
            t += 0.1f;
            currentColor = Color.Lerp(startColor, destinationColor, t);
            print("Lerping Color: " + currentColor);
            mat.color = currentColor;
            yield return new WaitForSeconds(0.3f);
        }
        IsChangingColor = false;
        yield break;
    }

    public void Implode()
    {
        print("Imploding");
        originalScale = gameObject.transform.localScale;
        this.gameObject.transform.parent.localScale = new Vector3(3f, 3f, 3f);
        ReadyToImplode = true;
    }
}
