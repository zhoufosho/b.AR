using UnityEngine;
using System.Collections;

public class grow : MonoBehaviour {
    public GameObject leaf;

    // Use this for initialization
    void Start () {
        GameObject l = Instantiate(leaf, new Vector3(0F, 0F, 0F), Quaternion.identity) as GameObject;
        l.AddComponent<Animation>();
        l.GetComponent<Animation>().Play("leaf");
    }
}
