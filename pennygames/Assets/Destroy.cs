﻿using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision col) {
        if (col.gameObject.name == "Cube 1(Clone)")
        {
            Destroy(col.gameObject);
        }
	}
}
