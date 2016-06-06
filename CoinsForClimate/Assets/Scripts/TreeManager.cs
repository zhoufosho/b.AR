using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeManager : MonoBehaviour {
    // Configurable Tree Values
    [Range(500, 1500)]
    public int MaxVertices;
    [Range(0.01f, 1f)]
    public float GrowthDelay;
    [Range(4, 20)]
    public int NumSides;
    [Range(0.25f, 4f)]
    public float BaseRadius;
    [Range(0.75f, 1.0f)]
    public float RadiusFalloff;
    [Range(0.01f, 0.2f)]
    public float MinimumRadius;
    [Range(2, 5)]
    public int BranchAmount;
    [Range(0.5f, 1f)]
    public float BranchRoundness;
    [Range(0.01f, 2f)]
    public float SegmentLength;
    [Range(0f, 40f)]
    public float Twisting;
    [Range(0f, 0.3f)]
    public float BranchProbability;
    [Range(0f, 1f)]
    public float LeafProbability;
    public GameObject Leaf;
    private int BranchLevel = 0;

    GameObject baseBranch;
    public List<GameObject> leaves;

    void Awake()
    {
        GameFramework.OnStartTreeGrowth += RunGrowth;
        ClimateManager.ClimateFall += BurstLeaves;
        ClimateManager.ClimateImprove += ImproveAura;

    }

    void OnDisable()
    {
        ClimateManager.ClimateFall -= BurstLeaves;
        ClimateManager.ClimateImprove -= ImproveAura;
    }

    void BurstLeaves()
    {
        print("Bursting Leaves");
        for (int i = 0; i < 20; i++)
        {
            if (leaves.Count == 0) break;
            int randIndex = Random.Range(0, leaves.Count);

            GameObject leaf = leaves[randIndex];
            leaves.RemoveAt(randIndex);

            leaf.GetComponentInChildren<LeafController>().Implode();
        }
    }

    void ImproveAura()
    {

    }

    // Use this for initialization
    void RunGrowth () {
        // Create the base branch
        baseBranch = new GameObject();
        baseBranch.name = "Base";
        baseBranch.transform.parent = gameObject.transform;
        baseBranch.transform.localPosition = transform.position;
        baseBranch.transform.localScale = Vector3.one;

        BranchGrowth baseGrowth = baseBranch.AddComponent<BranchGrowth>();
        baseGrowth.MaxVertices = MaxVertices;
        baseGrowth.GrowthDelay = GrowthDelay;
        baseGrowth.NumSides = NumSides;
        baseGrowth.BaseRadius = BaseRadius;
        baseGrowth.RadiusFalloff = RadiusFalloff;
        baseGrowth.MinimumRadius = MinimumRadius;
        baseGrowth.BranchAmount = BranchAmount;
        baseGrowth.BranchRoundness = BranchRoundness;
        baseGrowth.SegmentLength = SegmentLength;
        baseGrowth.Twisting = Twisting;
        baseGrowth.BranchProbability = BranchProbability;
        baseGrowth.LeafProbability = LeafProbability;
        baseGrowth.Leaf = Leaf;
        baseGrowth.BranchLevel = BranchLevel;

        leaves = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void AddLeaf(GameObject leaf)
    {
        leaves.Add(leaf);
    }
}
