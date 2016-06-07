using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    //public Vector3 force;
    public static string coinSoundPath = "Coins Sfx/Mp3/Coins_Single/Coins_Single_";
    public static AudioClip[] coinSounds = null;

    void Awake()
    {
        //Debug.Log("Coin Awake func");
        if(GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().useGravity = false;
        }

        if(Coin.coinSounds == null)
        {
            Coin.coinSounds = new AudioClip[10] {
                Resources.Load(coinSoundPath + "00") as AudioClip,
                Resources.Load(coinSoundPath + "02") as AudioClip,
                Resources.Load(coinSoundPath + "07") as AudioClip,
                Resources.Load(coinSoundPath + "12") as AudioClip,
                Resources.Load(coinSoundPath + "13") as AudioClip,
                Resources.Load(coinSoundPath + "15") as AudioClip,
                Resources.Load(coinSoundPath + "19") as AudioClip,
                Resources.Load(coinSoundPath + "21") as AudioClip,
                Resources.Load(coinSoundPath + "27") as AudioClip,
                Resources.Load(coinSoundPath + "55") as AudioClip,
            };
        }
    }

	// Use this for initialization
	void Start () {
        //Debug.Log("Coin Start func");
        //GetComponent<Rigidbody>().useGravity = false;

        Debug.AssertFormat(Coin.coinSounds.Length == 10, 
            "Expecting Coin.coinSounds to have 10 elements, instead has {0}", 
            Coin.coinSounds.Length);

        Debug.AssertFormat(Coin.coinSounds[0] != null, "Coin.coinSounds could not find sound assets in Resources folder");
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("Coin Update func");
        //if (Input.GetMouseButtonDown(0))
        //{
        //    GetComponent<Rigidbody>().useGravity = true;
        //    GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        //}
	}

    // Play random coin sound when coin collides with another object
    public void OnCollisionEnter(Collision col)
    {
        //if(col.gameObject.GetComponent<Coin>() != null)
        //{
            int randIx = (int)(Random.value * Coin.coinSounds.Length);
            AudioClip randSound = Coin.coinSounds[randIx];

            GetComponent<AudioSource>().clip = randSound;
            GetComponent<AudioSource>().Play();
        //Debug.LogFormat("Playing sound clip {0}", randSound.name);
        //}
    }
}
