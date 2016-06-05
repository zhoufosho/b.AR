using UnityEngine;
using System.Collections;

/// <summary>
/// Script to ensure that the associated object is always facing the camera
/// </summary>
public class FaceTheCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 lookAtTarget = new Vector3(Camera.main.transform.position.x,
                                          transform.position.y,
                                          Camera.main.transform.position.z);

        transform.LookAt(lookAtTarget, Vector3.up);
	}
}
