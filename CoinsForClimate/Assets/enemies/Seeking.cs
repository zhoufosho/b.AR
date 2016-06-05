using UnityEngine;
using System.Collections;

public class Seeking : MonoBehaviour {
    public Transform target;
    public float vel = 5.0f;
    public float angularSpeed = 50.0f;
    public float delay = 5.0f;
    
    private Transform mTransform;
    
	// Use this for initialization
	void Start () {
        mTransform = transform;
        //Object.Destroy(gameObject, delay*10);
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject) {
            // Calculate the direction from the current position to the target
            Vector3 dir = target.position - mTransform.position;
            // Calculate the rotation required to point at the target
            Quaternion rot = Quaternion.LookRotation(dir);
            // Rotate from the current rotation towards the target rotation, but not
            // faster than speed degrees per second
            mTransform.rotation = Quaternion.RotateTowards(
                                                 mTransform.rotation,
                                                 rot,
                                                 angularSpeed * Time.deltaTime);
            mTransform.rotation *= Quaternion.Euler(0f, 45f*Mathf.Sin(Time.deltaTime), 0f);
         
            // Move forward
            mTransform.position += mTransform.forward * vel * Time.deltaTime;
        }
    }
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Tree" ) {
            Destroy (gameObject);
        }
    }
}
