using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public delegate void GameAction();
    public static event GameAction StartGame;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void broadcastStart()
    {
        if (StartGame != null) StartGame();
    }
}
