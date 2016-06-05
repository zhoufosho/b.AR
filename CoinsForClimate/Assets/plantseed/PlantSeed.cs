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
        Debug.Assert(anim != null, "Seed object could not find its own Animator component");
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

            if(GameFramework.instance != null)
            {
                GameFramework.instance.OnFinishSeedPlace(this.gameObject);
            }
        }
    }
}
