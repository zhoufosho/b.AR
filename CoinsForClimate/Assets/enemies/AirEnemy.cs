using UnityEngine;
using System.Collections;

public class AirEnemy : MonoBehaviour {
    public float delay = 3.0f;
    public GameObject airEnemy;
    public Transform target;
    public float spawnRadius = 15f;
    public float spawnHeight = 10f;
    
    private GameObject enemyInstance;
    
    // Use this for initialization
    void Start() {
        InvokeRepeating("Spawn", delay, delay*10);
    }

    void Spawn() {
        Vector3 tpos = target.position;
        enemyInstance = Instantiate(airEnemy, new Vector3(Random.Range(tpos.x-spawnRadius, tpos.x+spawnRadius), spawnHeight, Random.Range(tpos.z-spawnRadius, tpos.z+spawnRadius)), Quaternion.identity) as GameObject;
        enemyInstance.transform.GetChild(0).localScale = Vector3.one;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject.name == airEnemy.name+"(Clone)") Destroy(hit.collider.gameObject);
            }
        }
    }
    
}
