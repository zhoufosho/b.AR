using UnityEngine;
using UnityEngine.VR.WSA.Input;
using System.Collections;

public class ShotScript : MonoBehaviour
{
    public GameObject shotPrefab;
    public AudioSource audioSrc;

    GestureRecognizer recognizer;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0.5f;
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            ShootObject(ray);
        };

        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // For prototyping without emulator
        Vector3 origin = transform.position;
        Vector3 dir = transform.forward;
        Ray r = new Ray(origin, dir);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shooting projectile");
            ShootObject(r);
        }
    }

    void ShootObject(Ray headRay)
    {
        // Spawn a ball at the point
        if(audioSrc != null)
        {
            audioSrc.Play();
        }
        GameObject shotObj = Instantiate(shotPrefab, transform.position, transform.rotation) as GameObject;

        // Apply a forward force
        Rigidbody rb = shotObj.GetComponent<Rigidbody>();
        rb.AddForce(headRay.direction * 1200);
    }
}
