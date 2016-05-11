using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
    public float delay = 0.1f;
    public GameObject cube;
    public Object cubeInstance;

    // Use this for initialization
    void Start() {
        InvokeRepeating("Spawn", delay, delay);
    }

    void Spawn() {
        cubeInstance = Instantiate(cube, new Vector3(Random.Range(20, 40), 15, Random.Range(20, 40)), Quaternion.identity);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                print(hit.collider.gameObject);
                if (hit.collider.gameObject.name == "Cube 1(Clone)") Destroy(hit.collider.gameObject);
            }
        }
    }
}
