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

    // Play appropriate sound when a coin collides with it
    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent<Coin>() != null)
        {
            int randIx = (int)(Random.value * coinSounds.Count);
            AudioClip randSound = coinSounds[randIx];

            audio.clip = randSound;
            audio.Play();
        }
    }
}
