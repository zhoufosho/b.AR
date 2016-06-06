using UnityEngine;
using System.Collections;

public class Seeking : MonoBehaviour {
    public float vel = 5.0f;
    public float speed = 50.0f;
    public float delay = 5.0f;
    public float dropSpeed = 0.01f;
    
    private Transform mTransform;
    private Transform target;

    void Awake ()
    {
        GameFramework.OnTreeLose += Deactivate;
        GameFramework.OnTreeWin += Deactivate;
    }

    void OnDestroy()
    {
        GameFramework.OnTreeLose -= Deactivate;
        GameFramework.OnTreeWin -= Deactivate;
    }

    private void Deactivate()
    {
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        mTransform = transform;
        target = GameObject.FindWithTag("Tree").transform;
        //Object.Destroy(gameObject, delay*10);
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject) {
            if (mTransform.position.y < target.position.y) Destroy (gameObject);
            // Calculate the direction from the current position to the target
            Vector3 dir = target.position - mTransform.position;
            dir.y = 0f;
            // Calculate the rotation required to point at the target
            Quaternion rot = Quaternion.LookRotation(dir);
            // Rotate from the current rotation towards the target rotation, but not
            // faster than speed degrees per second
            mTransform.rotation = Quaternion.RotateTowards(
                                                 mTransform.rotation,
                                                 rot,
                                                 speed * Time.deltaTime);
            mTransform.rotation *= Quaternion.Euler(0f, 45f*Mathf.Sin(Time.deltaTime), 0f);
         
            // Move forward
            mTransform.position += mTransform.forward * vel * Time.deltaTime - new Vector3(0f, dropSpeed, 0f);
        }
    }
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Tree" || collision.gameObject.name == "Pot") {
            Destroy (gameObject);
            ClimateManager.TreeHit();
        }
    }
}
