using UnityEngine;
using System.Collections;

public class TreeControl : MonoBehaviour {
    public delegate void GrowthAction();
    public static event GrowthAction IncrementGrowth;
    public static event GrowthAction DecrementGrowth;

    public GameObject leaf;

    GameObject baseBranch;

	// Use this for initialization
	void Start () {
        // Create the base branch
        baseBranch = new GameObject();
        baseBranch.name = "Base";
        baseBranch.transform.parent = gameObject.transform;
        baseBranch.transform.localPosition = transform.position;

        baseBranch.AddComponent<TreeGrowth>();
        baseBranch.GetComponent<TreeGrowth>().Twisting = 12;
        baseBranch.GetComponent<TreeGrowth>().Leaf = leaf;

        InvokeRepeating("IncreaseGrowth", 5f, 10f);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter(Collision col)
    {
        print("Collision");
        if (col.gameObject.tag == "Enemy")
        {
            if (DecrementGrowth != null) DecrementGrowth();
            Destroy(col.gameObject);
        }
    }

    void IncreaseGrowth()
    {
        if (IncrementGrowth != null) IncrementGrowth();
    }
}
