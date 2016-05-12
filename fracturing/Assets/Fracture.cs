using UnityEngine;
using System.Collections;

public class Fracture : MonoBehaviour
{

    public float forceThreshold = 1.0f;
    public float explodeForce = 30.0f;

    GameObject fragmentPrefab;

    // Use this for initialization
    void Start()
    {
        fragmentPrefab = Resources.Load("CubeFragments") as GameObject;

        Debug.Assert(fragmentPrefab != null);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // Fracture
            GameObject frags = Instantiate(fragmentPrefab, transform.position, transform.rotation) as GameObject;

            // Add random perturbation to all fragments
            for (int i = 0; i < frags.transform.childCount; i++)
            {
                GameObject child = frags.transform.GetChild(i).gameObject;
                child.GetComponent<Rigidbody>().AddForce(explodeForce * child.transform.localPosition, ForceMode.Impulse);
            }

            //frac.GetComponent<Rigidbody>().AddExplosionForce(1.0f, frac.transform.position, 1.0f);

            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        // Check force threshold and fracture if it is met
        Debug.Log(string.Format("Impact: Impulse {0}, Force {1}",
            Vector3.Magnitude(col.impulse),
            Vector3.Magnitude(col.impulse / Time.deltaTime)));

        if(Vector3.Magnitude(col.impulse) > forceThreshold)
        {
            // Fracture
            GameObject frags = Instantiate(fragmentPrefab, transform.position, transform.rotation) as GameObject;

            // Add random perturbation to all fragments
            for (int i = 0; i < frags.transform.childCount; i++)
            {
                GameObject child = frags.transform.GetChild(i).gameObject;
                child.GetComponent<Rigidbody>().AddForce(explodeForce * child.transform.localPosition, ForceMode.Impulse);
            }

            //frac.GetComponent<Rigidbody>().AddExplosionForce(1.0f, frac.transform.position, 1.0f);

            Destroy(gameObject);
        }
    }
}