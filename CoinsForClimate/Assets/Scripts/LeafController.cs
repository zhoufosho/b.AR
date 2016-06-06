using UnityEngine;
using System.Collections;

public class LeafController : MonoBehaviour {
    bool IsChangingColor = false;
    Color currentColor;

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
        StartCoroutine("LerpColor");
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
}
