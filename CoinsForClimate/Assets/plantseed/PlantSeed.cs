using UnityEngine;
using System.Collections;

public class PlantSeed : MonoBehaviour {

    Animator anim;

    bool done = false;

    void OnEnable()
    {
        ClimateManager.StartGame += Activate;
        GameFramework.OnStartTreeGrowth += Deactivate;
    }


    void OnDisable()
    {
        ClimateManager.StartGame -= Activate;
        GameFramework.OnStartTreeGrowth -= Deactivate;
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        Debug.Assert(anim != null, "Seed object could not find its own Animator component");
	}

    void Activate()
    {
        anim.enabled = true;
    }

    void Deactivate()
    {
        anim.enabled = false;
    }

	// Update is called once per frame
	void Update () {

        // Once animation has completed, begin tree growth stage
        if (done == false && anim.GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            done = true;

            GameFramework.TriggerTreeGrowth();
        }
    }
}
