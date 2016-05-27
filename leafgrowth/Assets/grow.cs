using UnityEngine;
using System.Collections;

public class grow : MonoBehaviour {

    public GameObject trunk;
    public GameObject leaf;
    public float growthRate;

    private bool leafSpawned = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.y < 4) {
            transform.localScale += new Vector3(0, growthRate, 0);
            transform.Translate(new Vector3(0, growthRate/2.0F, 0));
        }
        if (transform.localScale.y >= 4 && !leafSpawned) {
            GameObject l = Instantiate(leaf, new Vector3(transform.position.x, 4F, transform.position.z), Quaternion.identity) as GameObject;
            l.AddComponent<Animation>();
            l.GetComponent<Animation>().Play("leaf");
            
            // Quaternion q = Quaternion.Euler(10F,13F,20F);
            // GameObject l2 = Instantiate(leaf, new Vector3(transform.position.x-0.7F, 4F-1.2F, transform.position.z), Quaternion.identity) as GameObject;
            // l2.transform.localScale -= new Vector3(0.6F,0.6F,0.6F);
            // l2.transform.rotation = q;
            // l2.AddComponent<Animation>();
            // l2.GetComponent<Animation>().Play("leaf");
            leafSpawned = true;
        }
    }
}
