using UnityEngine;
using System.Collections;

public class DeactivateOnState : MonoBehaviour {

    public GameState state;

    void Awake()
    {
        switch(state)
        {
            case GameState.PLANT_SEED:
                GameFramework.OnPlantSeed += Deactivate;
                break;
            case GameState.TREE_GROWTH:
                GameFramework.OnStartTreeGrowth += Deactivate;
                break;
            case GameState.TREE_LOSE:
                GameFramework.OnTreeLose += Deactivate;
                break;
            case GameState.TREE_WIN:
                GameFramework.OnTreeWin += Deactivate;
                break;
        }
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
