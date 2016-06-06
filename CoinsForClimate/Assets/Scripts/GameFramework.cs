using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState
{
    PLANT_SEED,
    TREE_GROWTH,
    TREE_WIN,
    TREE_LOSE
}

/// <summary>
/// The skeleton of the game. Manages the transition between game states
/// </summary>
public class GameFramework : MonoBehaviour
{
    //public static GameFramework instance = null;

    public static float gameTime = 10f; // Starting time when defending the tree
    public static GameState startState = GameState.PLANT_SEED;

    public static GameState State { get; private set; }

    // Events and delegates to broadcast state changes
    public delegate void StateChangeHandler();
    public static event StateChangeHandler OnPlantSeed;
    public static void TriggerPlantSeed()
    {
        State = GameState.PLANT_SEED;
        if (OnPlantSeed != null)
        {
            OnPlantSeed();
        }
    }

    public static event StateChangeHandler OnStartTreeGrowth;
    public static void TriggerTreeGrowth()
    {
        State = GameState.TREE_GROWTH;
        if (OnStartTreeGrowth != null)
        {
            OnStartTreeGrowth();
        }
    }

    public static event StateChangeHandler OnTreeWin;
    public static void TriggerTreeWin()
    {
        State = GameState.TREE_WIN;
        if (OnTreeWin != null)
        {
            OnTreeWin();
        }
    }

    public static event StateChangeHandler OnTreeLose;
    public static void TriggerTreeLose()
    {
        State = GameState.TREE_LOSE;
        if (OnTreeLose != null)
        {
            OnTreeLose();
        }
    }

    /// <summary>
    /// Use this function if you want a gameobject to be active during a particular game state and inactive otherwise
    /// </summary>
    /// <param name="state">The state your object should be active</param>
    /// <param name="activateHandler">Handler for activation. Called when activation state begins.</param>
    /// <param name="deactivateHandler">Handler for deactivation. Called after activation state ends.</param>
    public static void ActiveDuringState(GameState state, StateChangeHandler activateHandler, StateChangeHandler deactivateHandler)
    {
        switch (state)
        {
            case GameState.PLANT_SEED:
                OnPlantSeed += activateHandler;
                OnStartTreeGrowth += deactivateHandler;
                break;
            case GameState.TREE_GROWTH:
                OnStartTreeGrowth += activateHandler;
                OnTreeLose += deactivateHandler;
                OnTreeWin += deactivateHandler;
                break;
            case GameState.TREE_LOSE:
                OnTreeLose += activateHandler;
                break;
            case GameState.TREE_WIN:
                OnTreeWin += activateHandler;
                break;
        }

    }

    //Awake is always called before any Start functions
    void Awake()
    {
        ////Check if instance already exists
        //if (instance == null)

        //    //if not, set instance to this
        //    instance = this;

        ////If instance already exists and it's not this:
        //else if (instance != this)

        //    //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        //    Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {

        // Initialize the first phase of the game
        switch (startState)
        {
            case GameState.PLANT_SEED:
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
    void Update()
    {

    }

}