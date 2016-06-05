using UnityEngine;
using System.Collections;

public class DonationPrompt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnYesClicked()
    {
        GameObject.Find("CoinKobe").GetComponent<CoinSpawner>().spawn = true;
        Debug.Log("Yes button clicked");
    }

    public void OnNoClicked()
    {
        Destroy(this.gameObject);
    }
}
