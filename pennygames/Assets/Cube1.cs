using UnityEngine;
using System.Collections;

public class Cube1 : MonoBehaviour
{
    public float delay = 0.1f;
    public GameObject cube;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", delay, delay);
    }

    void Spawn()
    {
        Instantiate(cube, new Vector3(Random.Range(20, 40), 15, Random.Range(20, 40)), Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("click");
        }
    }
}
