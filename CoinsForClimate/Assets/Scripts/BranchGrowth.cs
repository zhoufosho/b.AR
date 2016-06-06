using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BranchGrowth : MonoBehaviour {
    public TreeManager treeController;

    public int MaxVertices;
    public float GrowthDelay;
    public int NumSides;
    public float BaseRadius;
    public float RadiusFalloff;
    public float MinimumRadius;
    public int BranchAmount;
    public float BranchRoundness;
    public float SegmentLength;
    public float Twisting;
    public float BranchProbability;
    public float LeafProbability;
    public int BranchLevel;
    public GameObject Leaf;

    MeshFilter mFilter;
    MeshRenderer mRenderer;
    List<Vector3> vertexList; // Vertex list
    List<int> triangleList; // Triangle list
    Material mat;
    Material treeMaterialHealthy;
    Material treeMaterialDead;
    Color currentColor;
    float[] ringShape;
    int lastRingVertexIndex;
    Vector3 lastPosition;
    int branchCallsForSprout;
    int branchCalls;
    bool IsChangingColor = false;

    public float GROWTH_INCREASE_FACTOR = 2f;
    public float GROWTH_DECREASE_FACTOR = 0.5f;
    public float MIN_GROWTH = 0.01f;
    public float BRANCH_RATIO = 0.33f;
    public float NEW_BRANCH_GROWTH_FACTOR = 2f;
    public float NEW_BRANCH_TWISTING_INCREMENT = 4;
    public float NEW_BRANCH_LEAF_PROBABILITY_FALLOFF = 0.02f;
    public float RANDOM_SPROUT_PROBABILITY = 0.01f;
    public float SPROUT_RADIUS_FALLOFF = 0.9f;
    public float RANDOM_SEGMENT_EXTENSION_PROBABILITY = 0.9f;
    public float SEGMENT_EXTENSION_FACTOR = 2f;

    public int getBranchCallsForSprout()
    {
        return branchCallsForSprout;
    }

    void Awake()
    {
        treeController = GameObject.FindGameObjectWithTag("Tree").GetComponent<TreeManager>();
        print("BranchGrowth finding tree: " + GameObject.FindGameObjectWithTag("Tree").name);
    }

    // Add a Mesh Filter/Renderer
    void OnEnable()
    {
        mFilter = gameObject.GetComponent<MeshFilter>();
        if (mFilter == null) mFilter = gameObject.AddComponent<MeshFilter>();
        mRenderer = gameObject.GetComponent<MeshRenderer>();
        if (mRenderer == null) mRenderer = gameObject.AddComponent<MeshRenderer>();

        ClimateManager.ClimateChange += ChangeTreeColor;
    }

    void OnDisable()
    {
        ClimateManager.ClimateChange -= ChangeTreeColor;
    }

    void ChangeTreeColor()
    {
        float climateStrength = ClimateManager.GetClimateStrength();
        Color destinationColor = Color.Lerp(treeMaterialDead.color, treeMaterialHealthy.color, climateStrength);
        mat.color = destinationColor;
    }

    void IncreaseGrowthDelay()
    {
        print("Slowing growth");
        GrowthDelay *= GROWTH_INCREASE_FACTOR;
    }

    void DecreaseGrowthDelay()
    {
        print("Speeding up growth");
        GrowthDelay *= GROWTH_DECREASE_FACTOR;
        GrowthDelay = (GrowthDelay > MIN_GROWTH) ? GrowthDelay : MIN_GROWTH;
    }

    // Use this for initialization
    void Start () {
        vertexList = new List<Vector3>();
        triangleList = new List<int>();
        lastPosition = Vector3.zero;
        lastRingVertexIndex = -1;

        treeMaterialHealthy = Resources.Load("TreeBarkHealthy", typeof(Material)) as Material;
        treeMaterialDead = Resources.Load("TreeBarkDead", typeof(Material)) as Material;

        mat = Resources.Load("TreeTemp", typeof(Material)) as Material;

        currentColor = Color.Lerp(treeMaterialDead.color, treeMaterialHealthy.color, ClimateManager.GetClimateStrength());
        mat.color = currentColor;

        SetBranchLimits();
        StartCoroutine("Branch");
    }

    void SetBranchLimits()
    {
        int verticesPerLevel = NumSides + 1;
        int maxBranchCallsByVertexCount = MaxVertices / verticesPerLevel;
        int maxBranchCallsByRadiusFalloff = (int)(Mathf.Log(MinimumRadius / BaseRadius) / Mathf.Log(RadiusFalloff));
        branchCalls = (maxBranchCallsByVertexCount > maxBranchCallsByRadiusFalloff) ? maxBranchCallsByRadiusFalloff : maxBranchCallsByVertexCount;
        branchCallsForSprout = (int)( (float)branchCalls * BRANCH_RATIO );
        print("BranchCallsForSprout: " + branchCallsForSprout);
    }

    // Update is called once per frame
    void Update () {

    }

    void SproutBranches(Vector3 position, float radius, int numChildren)
    {
        for (int i = 0; i < BranchAmount; i++)
        {
            GameObject branch = new GameObject();
            branch.name = "BranchLevel" + BranchLevel;
            branch.transform.parent = gameObject.transform;
            branch.transform.localPosition = position;
            branch.transform.localScale = Vector3.one;

            BranchGrowth branchGrowth = branch.AddComponent<BranchGrowth>();
            branchGrowth.MaxVertices = MaxVertices;
            branchGrowth.GrowthDelay = GrowthDelay * NEW_BRANCH_GROWTH_FACTOR;
            branchGrowth.NumSides = NumSides;
            branchGrowth.BaseRadius = radius;
            branchGrowth.RadiusFalloff = RadiusFalloff;
            branchGrowth.MinimumRadius = MinimumRadius;
            branchGrowth.BranchAmount = numChildren;
            branchGrowth.BranchRoundness = BranchRoundness;
            branchGrowth.SegmentLength = SegmentLength;
            branchGrowth.Twisting = Twisting + NEW_BRANCH_TWISTING_INCREMENT;
            branchGrowth.BranchProbability = BranchProbability;
            branchGrowth.LeafProbability = LeafProbability - NEW_BRANCH_LEAF_PROBABILITY_FALLOFF;
            branchGrowth.Leaf = Leaf;
            branchGrowth.BranchLevel = BranchLevel + 1;
            branchGrowth.treeController = this.treeController;
        }
    }

    void SproutLeaf(Vector3 position, Quaternion rotation)
    {
        GameObject leaf = Instantiate(Leaf, position, rotation) as GameObject;
        leaf.name = "LeafLevel" + BranchLevel;
        leaf.transform.parent = gameObject.transform;
        leaf.transform.localScale = new Vector3(Random.Range(0.1f, 0.4f), Random.Range(0.1f, 0.4f), Random.Range(0.1f, 0.4f));
        leaf.transform.localPosition = lastPosition;
        
        treeController.AddLeaf(leaf);
    }

    IEnumerator LerpColor()
    {
        float climateStrength = ClimateManager.GetClimateStrength();
        Color destinationColor = Color.Lerp(treeMaterialDead.color, treeMaterialHealthy.color, climateStrength);
        Color startColor = new Color(currentColor.r, currentColor.g, currentColor.b);
        IsChangingColor = true;

        float t = 0f;
        while (t <= 1f)
        {
            t += 0.1f;
            currentColor = Color.Lerp(startColor, destinationColor, t);
            mat.color = currentColor;
            yield return new WaitForSeconds(0.3f);
        }
        IsChangingColor = false;
        yield break;
    }

    IEnumerator Branch()
    {
        Quaternion originalRotation = transform.localRotation;
        Quaternion q = new Quaternion();
        float radius = BaseRadius;
        int numBranchIters = 0;
        SetRingShape();

        while (numBranchIters < branchCalls)
        {
            numBranchIters++;

            if (vertexList.Count != 0) UncapBranch();
            if (Random.value < RANDOM_SPROUT_PROBABILITY) SproutBranches(lastPosition, radius * SPROUT_RADIUS_FALLOFF, 0); // Randomly sprout
            if (numBranchIters == branchCallsForSprout) SproutBranches(lastPosition, radius* SPROUT_RADIUS_FALLOFF, BranchAmount-1);

            AddRingVertices(q, radius);
            if (lastRingVertexIndex >= 0) AddTriangles(lastRingVertexIndex);
            radius *= RadiusFalloff;

            // Randomize the branch angle and extend the branch, with random offsets
            transform.rotation = q;
            float x = (Random.value - 0.5f) * Twisting;
            float z = (Random.value - 0.5f) * Twisting;
            if (Random.value > 0.7f)
            {
                x = x * 1.5f;
                z = z * 1.5f;
            }
            transform.Rotate(x, 0f, z);
            
            if (Random.value < LeafProbability || numBranchIters == branchCalls) SproutLeaf(lastPosition, transform.localRotation);

            float extension = (Random.value < RANDOM_SEGMENT_EXTENSION_PROBABILITY) ? SegmentLength : SegmentLength * SEGMENT_EXTENSION_FACTOR;
            lastPosition += q * new Vector3(0f, extension, 0f);

            // Prep for next extension
            q = transform.rotation;
            transform.localRotation = originalRotation;
            CapBranch(lastPosition);
            DrawBranch();
            lastRingVertexIndex = vertexList.Count - NumSides - 1;
            yield return new WaitForSeconds(GrowthDelay);
        }

        yield break;
    }

    private void DrawBranch()
    {
        // Get mesh or create one
        Mesh mesh = mFilter.sharedMesh;
        if (mesh == null)
            mesh = mFilter.sharedMesh = new Mesh();
        else
            mesh.Clear();
        mRenderer.sharedMaterial = mat;
        mRenderer.receiveShadows = false;
        mRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        // Assign vertex data
        mesh.vertices = vertexList.ToArray();
        mesh.triangles = triangleList.ToArray();

        // Update mesh
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    private void SetRingShape()
    {
        ringShape = new float[NumSides+1];
        
        for (int i = 0; i < NumSides; i++) ringShape[i] = 1f;
        ringShape[NumSides] = ringShape[0];
    }

    private void UncapBranch()
    {
        int numToRemove = NumSides * 3;
        triangleList.RemoveRange(triangleList.Count - numToRemove, numToRemove);
        vertexList.RemoveAt(vertexList.Count - 1); // Add central vertex
    }

    private void CapBranch(Vector3 position)
    {
        // Create a cap for ending the branch
        vertexList.Add(position); // Add central vertex
        for (var n = vertexList.Count - NumSides - 2; n < vertexList.Count - 2; n++) // Add cap
        {
            triangleList.Add(n);
            triangleList.Add(vertexList.Count - 1);
            triangleList.Add(n + 1);
        }
    }

    private void AddRingVertices(Quaternion quaternion, float radius)
    {
        Vector3 offset = Vector3.zero;
        float textureStepU = 1f / NumSides;
        float angInc = 2f * Mathf.PI * textureStepU;
        float ang = 0f;

        // Add ring vertices
        for (int n = 0; n <= NumSides; n++, ang += angInc)
        {
            float r = ringShape[n] * radius;
            offset.x = r * Mathf.Cos(ang); // Get X, Z vertex offsets
            offset.z = r * Mathf.Sin(ang);
            vertexList.Add(lastPosition + quaternion * offset); // Add Vertex position
        }
    }

    private void AddTriangles(int lastRingVertexIndex)
    {
        // Create quads between the last two tree rings
        for (int currentRingVertexIndex = vertexList.Count - NumSides - 1; currentRingVertexIndex < vertexList.Count - 1; currentRingVertexIndex++, lastRingVertexIndex++)
        {
            triangleList.Add(lastRingVertexIndex + 1); // Triangle A
            triangleList.Add(lastRingVertexIndex);
            triangleList.Add(currentRingVertexIndex);

            triangleList.Add(currentRingVertexIndex); // Triangle B
            triangleList.Add(currentRingVertexIndex + 1);
            triangleList.Add(lastRingVertexIndex + 1);
        }
    }
}
