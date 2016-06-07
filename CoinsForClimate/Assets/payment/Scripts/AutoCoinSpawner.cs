using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;

public class AutoCoinSpawner : MonoBehaviour
{

    public float spawnRate = 0.3f; // num seconds until next spawn
    public float initialDelay = 1.0f;
    public int numToSpawn = 1;

    public GameObject coin;

    float timeSinceSpawn = 0f;
    bool spawning = false;
    int numSpawned;

    void Awake()
    {
        GameFramework.OnTreeWin += Activate;
    }

    void Activate()
    {
        // Find number of shots from shot script
        ShotScript shotScriptObj = Camera.main.GetComponent<ShotScript>();
        numToSpawn = shotScriptObj.numShots;

        InvokeRepeating("Spawn", initialDelay, spawnRate);
        spawning = true;
    }

    void OnDisable()
    {
        GameFramework.OnTreeWin -= Activate;
    }

    void Start()
    {
    }

    void Update()
    {
        if(spawning && numSpawned >= numToSpawn)
        {
            CancelInvoke("Spawn");
            spawning = false;
        }
    }

    void Spawn()
    {
        GameObject newCoin = Instantiate(coin) as GameObject;
        newCoin.transform.SetParent(transform, false);
        newCoin.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere, ForceMode.Impulse);
        newCoin.GetComponent<Rigidbody>().useGravity = true;

        numSpawned += 1;
    }
}