using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public bool grenade = false;
    public float grenadeRadius = 100.0f;
    public GameObject explosion; 
    public AudioClip sound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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

        Destroy (gameObject);
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }
}
