using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jar : MonoBehaviour {

    public List<AudioClip> coinSounds;

    AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        Debug.Assert(coinSounds.Count > 0, "Jar must contain at least one sound clip of a coin colliding with it");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
