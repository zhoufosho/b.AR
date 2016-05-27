using UnityEngine;
using System.Collections;

public class GroundEnemy : MonoBehaviour {
    public float delay = 4.0f;
    public GameObject groundEnemy;
    public Transform target;
    
    private GameObject enemyInstance;
    
	// Use this for initialization
    void Start() {
        InvokeRepeating("Spawn", delay, delay);
    }

    void Spawn() {
        Vector3 tpos = target.position;
        enemyInstance = Instantiate(groundEnemy, new Vector3(Random.Range(tpos.x-15, tpos.x+15), 0, Random.Range(tpos.z-15, tpos.z+15)), Quaternion.identity) as GameObject;
        enemyInstance.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyInstance.transform.localScale.x < 1.0f) {
            enemyInstance.transform.localScale += new Vector3(0.1f,0.1f,0.1f);
        }
        
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject.name == "GroundEnemy(Clone)") Destroy(hit.collider.gameObject);
            }
        }
	}
}
