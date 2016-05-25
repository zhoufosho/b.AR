using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinSpawner : MonoBehaviour {

    public float delay = 0.5f;
    public Vector3 force;
    public float randScale;

    public GameObject quarter;
    public GameObject penny;
    public float donationThreshold;

    public bool spawn = false;

    Text jarLabel;
    float donation;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnCoin", delay, delay);
        jarLabel = GameObject.Find("Jar/JarLabel/Canvas/Text").GetComponent<Text>();

        Debug.Assert(jarLabel != null, "Could not find jar label");
	}
	
	// Update is called once per frame
	void Update () {
        jarLabel.text = string.Format("${0:f2}", donation);
	}

    public void SpawnCoin()
    {
        GameObject spawnCoin;

        if((!Input.GetMouseButton(0) && spawn == false) || donation >= donationThreshold)
        {
            Debug.Log("Finished spawning");
            return;
        }

        Vector3 randVec = new Vector3(Random.Range(-1, 1), 0f, Random.Range(-1, 1)) * randScale;

        if(Random.value < 0.5f && donationThreshold - donation >= 0.25f)
        {
            spawnCoin = Instantiate(quarter, transform.position, Random.rotation) as GameObject;
            Debug.Log("Coin Spawned");
            spawnCoin.GetComponent<Rigidbody>().AddForce(force + randVec, ForceMode.Impulse);
            spawnCoin.GetComponent<Rigidbody>().useGravity = true;
            donation += 0.25f;
        }
        else
        {
            spawnCoin = Instantiate(penny, transform.position, Random.rotation) as GameObject;
            spawnCoin.GetComponent<Rigidbody>().AddForce(force + randVec, ForceMode.Impulse);
            spawnCoin.GetComponent<Rigidbody>().useGravity = true;
            donation += 0.01f;
        }

        // Swap text to confirmation
        if(donation == donationThreshold)
        {
            GameObject.Find("DonationPrompt/QuestionGroup").SetActive(false);
            GameObject.Find("DonationPrompt/ConfirmationGroup").SetActive(true);
        }
    }

}
