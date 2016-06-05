using UnityEngine;
using System.Collections;

public class PlantSeed : MonoBehaviour {

    Animator anim;
    TreeControl tree;

    bool done = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        tree = GameObject.Find("Tree").GetComponent<TreeControl>();

        Debug.Assert(anim != null, "Seed object could not find its own Animator component");
        Debug.AssertFormat(tree != null, "Seed object could not find GameObject '{0}'", "Tree");
	}
	
	// Update is called once per frame
	void Update () {

        // Once animation has completed, begin tree growth stage
        if (done == false && anim.GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            done = true;
            if(Camera.main.GetComponent<ShotScript>())
            {
                Camera.main.GetComponent<GroundEnemy>().enabled = true;
                Camera.main.GetComponent<AirEnemy>().enabled = true;
                Camera.main.GetComponent<ShotScript>().enabled = true;
            }
            tree.enabled = true;
        }
    }
}
