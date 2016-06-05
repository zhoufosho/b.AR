using UnityEngine;
using System.Collections;

public class AirEnemy : MonoBehaviour {
    public float maxRadius = 15f;
    public float startHeight = 10f;
    public float delay = 3.0f;
    public GameObject airEnemy;
    public Transform target;
    
    private GameObject enemyInstance;
    
    // Use this for initialization
    void Start() {
        InvokeRepeating("Spawn", delay, delay*10);
    }

    void Spawn() {
        Vector3 tpos = target.position;
        enemyInstance = Instantiate(airEnemy, new Vector3(Random.Range(tpos.x-maxRadius, tpos.x+maxRadius), 
                                                          startHeight, 
                                                          Random.Range(tpos.z-maxRadius, tpos.z+maxRadius)), 
                                              Quaternion.identity) as GameObject;
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
