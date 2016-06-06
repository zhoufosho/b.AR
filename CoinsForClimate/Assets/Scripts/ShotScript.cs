using UnityEngine;
using UnityEngine.VR.WSA.Input;
using System.Collections;

public class ShotScript : MonoBehaviour
{
    public GameObject shotPrefab;
    public AudioClip shotAudioClip;

    public float forwardForce = 1200f;
    public float torqueForce = 5000f;

    GestureRecognizer recognizer;

    void Awake()
    {
        GameFramework.OnStartTreeGrowth += Activate;
        GameFramework.OnTreeLose += Deactivate;
        GameFramework.OnTreeWin += Deactivate;
    }

    // Use this for initialization
    void Activate()
    {
        enabled = true;
        Time.timeScale = 0.5f;
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            ShootObject(ray);
        };

        recognizer.StartCapturingGestures();
    }

    void Deactivate()
    {
        enabled = false;
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
        GameObject shotObj = Instantiate(shotPrefab, transform.position, transform.rotation) as GameObject;
        AudioSource shotAudio = shotObj.GetComponent<AudioSource>();
        if(shotAudio != null)
        {
            shotAudio.clip = shotAudioClip;
            shotAudio.Play();
        }

        // Apply a forward force
        Rigidbody rb = shotObj.GetComponent<Rigidbody>();
        rb.AddForce(headRay.direction * forwardForce);

        // Apply a torque so projectile spins
        rb.AddTorque(Camera.main.transform.right * -torqueForce, ForceMode.Impulse);
    }
}
