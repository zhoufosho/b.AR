using UnityEngine;
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

        // Initialize the first phase of the game
        switch(startState)
        {
            case GameState.SEED_PLACE:
                SceneManager.LoadScene("Scenes/PromptPlacement", LoadSceneMode.Single);
                break;
            case GameState.TREE_GROWTH:
                SceneManager.LoadScene("Scenes/TreeGrowthDemo", LoadSceneMode.Single);
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

    public void OnFinishSeedPlace(GameObject caller) { }
    public void OnFinishTreeWin(GameObject caller) { }
    public void OnFinishTreeLose(GameObject caller) { }
    public void OnFinishDonate(GameObject caller) { }
}
