using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour {

    public float grassMaxRadius = 1f;
    public int grassNumPlants = 100;

    [Range(0f, 1f)]
    public float growth = 1f;

	// Use this for initialization
	void Start () {

        // Choose random points to place grass
        for(int i = 0; i < grassNumPlants; i++)
        {

        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
