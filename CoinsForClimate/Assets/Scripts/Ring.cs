using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GameFramework.OnStartTreeGrowth += Disable;
	}

    void Disable()
    {
        Destroy(this.gameObject);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
