using UnityEngine;
using System.Collections;

public class TreeDestroy : MonoBehaviour {

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
