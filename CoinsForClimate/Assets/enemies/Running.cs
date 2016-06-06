using UnityEngine;
using System.Collections;

public class Running : MonoBehaviour {
    public float vel = 7.0f;
    public float speed = 360.0f;
    public float delay = 5.0f;
    
    private Transform mTransform;
    private Transform target;

    void Awake()
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
        target = GameObject.FindWithTag("Jar").transform;
        //Object.Destroy(gameObject, delay);
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject) {
            Debug.LogFormat("Target position: {0}", target.position);
            // Calculate the direction from the current position to the target
            Vector3 dir = target.position - mTransform.position;
            // Calculate the rotation required to point at the target
            Quaternion rot = Quaternion.LookRotation(dir);
            // Rotate from the current rotation towards the target rotation, but not
            // faster than speed degrees per second
            mTransform.rotation = Quaternion.RotateTowards(
                                                 mTransform.rotation,
                                                 rot,
                                                 speed * Time.deltaTime);
         
            // Move forward
            mTransform.position += mTransform.forward * vel * Time.deltaTime;
        }
    }
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Tree" || collision.gameObject.name == "Pot") {
            Destroy (gameObject);
            ClimateManager.TreeHit();
        }
    }
}
