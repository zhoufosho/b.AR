using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnSpheres : MonoBehaviour {

    public GameObject heavySpherePrefab;

    public float zmin = 3f;
    public float zmax = 8f;
    public float xmin = -3f;
    public float xmax = 3f;
    public float ymax = 5f;

    public bool autoSpawn = true;
    public float spawnRate = 2;
    float spawnTime = 0f;

    float lifespan = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        bool spawn = false;
        spawnTime += Time.deltaTime;
        if(spawnTime > 1.0f / spawnRate)
        {
            spawn = true;
            spawnTime -= (1.0f / spawnRate);
        }

        if (Input.GetMouseButtonDown(0) || spawn)
        {
            // If left click, spawn a sphere
            Vector3 spawnPos = new Vector3(Random.Range(xmin, xmax), ymax, Random.Range(zmin, zmax));
            GameObject newSphere = GameObject.Instantiate(heavySpherePrefab, spawnPos, Quaternion.identity) as GameObject;
            StartCoroutine(Countdown(newSphere));
        }
	}

    public IEnumerator Countdown(GameObject sphere)
    {
        float lifetime = 0f;

        while(lifetime < lifespan) {
            lifetime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(sphere);
        yield return true;
    }
}
