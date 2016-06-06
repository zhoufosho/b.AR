using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public bool grenade = false;
    public float grenadeRadius = 100.0f;
    public GameObject explosion;
    public float lifetime = 3f; // Number of seconds after which projectile will disappear

    float age = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        age += Time.deltaTime / Time.timeScale;	

        if(age > lifetime)
        {
            Destroy(this.gameObject);
        }
	}
    
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy") {
            Destroy (collision.gameObject);
        }
        
        if (grenade) {
            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, grenadeRadius);
            for (int i=0; i < hitColliders.Length; i++) {
                if (hitColliders[i].gameObject.tag == "Enemy") {
                    Destroy (hitColliders[i].gameObject);
                }
            }
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(expl, 3);
        }

        //Destroy (gameObject);
    }
}
