using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    //public Vector3 force;

    void Awake()
    {
        //Debug.Log("Coin Awake func");
        if(GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().useGravity = false;
        }
    }

	// Use this for initialization
	void Start () {
        //Debug.Log("Coin Start func");
        //GetComponent<Rigidbody>().useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("Coin Update func");
        //if (Input.GetMouseButtonDown(0))
        //{
        //    GetComponent<Rigidbody>().useGravity = true;
        //    GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        //}
	}
}
