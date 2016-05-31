using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
    public GameObject projectile;
    public float distance = 10.0f;
    
    private float x = Screen.width / 2f;
    private float y = Screen.height / 2f;
 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0)) {
            Vector3 position = new Vector3(x, y, distance);
            position = Camera.main.ScreenToWorldPoint(position);
            GameObject go = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            go.transform.LookAt(position);  
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 1000);
        }
	}
}
