using UnityEngine;
using System.Collections;

public class PlantSeed : MonoBehaviour {

    Animator anim;
    GameObject growthRing;
    TreeControl tree;

    bool done = false;

    void OnEnable()
    {
        GameManager.StartGame += Plant;
    }


    void OnDisable()
    {
        GameManager.StartGame -= Plant;
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        tree = GameObject.Find("Tree").GetComponent<TreeControl>();

        Debug.Assert(anim != null, "Seed object could not find its own Animator component");
        Debug.AssertFormat(tree != null, "Seed object could not find GameObject '{0}'", "Tree");

        growthRing = GameObject.Find("Ring");
	}

    void Plant()
    {
        anim.enabled = true;
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
            Destroy(growthRing);
        }
    }
}
