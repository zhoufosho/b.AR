using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState
{
    SEED_PLACE,
    TREE_GROWTH,
    TREE_WIN,
    TREE_LOSE
}

/// <summary>
/// The skeleton of the game. Manages the transition between game states
/// </summary>
public class GameFramework : MonoBehaviour {
    public static GameFramework instance = null;

    public float gameTime = 180f; // Starting time when defending the tree
    public Vector3 treePoint = Vector3.zero;
    public GameState startState = GameState.SEED_PLACE;

    public GameObject winCanvasPrefab;
    public GameObject jarWithPromptPrefab;
    public GameObject jarNoPromptPrefab;
    public GameObject clockPrefab;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {

        Debug.Assert(winCanvasPrefab != null, "GameFramework needs a WinCanvas prefab");

        // Initialize the first phase of the game
        switch(startState)
        {
            case GameState.SEED_PLACE:
                break;
            case GameState.TREE_GROWTH:
                break;
            case GameState.TREE_WIN:
                break;
            case GameState.TREE_LOSE:
                break;
            default:
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnFinishSeedPlace(GameObject caller) {
        Debug.Log("GameFramework: OnFinishSeedPlace");

        // Spawn clock
        GameObject clock = Instantiate(clockPrefab);
        GameObject ancestor = caller;
        while(ancestor.transform.parent != null)
        {
            ancestor = ancestor.transform.parent.gameObject;
        }
        clock.transform.SetParent(ancestor.transform, false);

        // Enable tree and enemies
        GameObject treeObj = GameObject.FindGameObjectWithTag("Tree");
        TreeManager tree = treeObj.GetComponent<TreeManager>();
        GameObject growthRing = GameObject.Find("Ring");

        Debug.AssertFormat(tree != null, "Seed object could not find GameObject '{0}'", "Tree");

        if(Camera.main.GetComponent<ShotScript>())
        {
            Camera.main.GetComponent<GroundEnemy>().enabled = true;
            Camera.main.GetComponent<AirEnemy>().enabled = true;
            Camera.main.GetComponent<ShotScript>().enabled = true;
        }
        tree.enabled = true;
        Destroy(growthRing);
    }
    public void OnFinishTreeWin(GameObject caller) {
        Debug.Log("GameFramework: OnFinishTreeWin");

        // Show victory prompt
        GameObject winCanvas = Instantiate(winCanvasPrefab);
        winCanvas.transform.position = caller.transform.position + 1f * Vector3.up;

        Text winText = winCanvas.GetComponent<Text>();
        winText.text = "Victory!";

        // Show jar
        GameObject jar = Instantiate(jarNoPromptPrefab);
        jar.transform.position = caller.transform.position + 0.5f * Vector3.up;
    }
    public void OnFinishTreeLose(GameObject caller) { }
    public void OnFinishDonate(GameObject caller) { }
}
