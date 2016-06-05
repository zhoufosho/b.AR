using UnityEngine;
using System.Collections;

public class InstantGrowth : MonoBehaviour {

    bool done = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (done || transform.childCount == 0)
            return;

        TreeGrowth growth = transform.GetComponentInChildren<TreeGrowth>();

        if (growth == null)
            return;

        growth.GrowthDelay = 0.01f;
        done = true;
	}
}
