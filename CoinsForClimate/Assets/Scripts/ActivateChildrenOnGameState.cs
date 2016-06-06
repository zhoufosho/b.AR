using UnityEngine;
using System.Collections;

/// <summary>
/// Make GameObject active during specified game state
/// </summary>
public class ActivateChildrenOnGameState : MonoBehaviour {

    public GameState activeState;

	// Use this for initialization
	void Awake () {
        GameFramework.ActiveDuringState(activeState, ActivateChildren, DeactivateChildren);
	}

    void ActivateChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(true);
        }
    }

    void DeactivateChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
