using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;
using System.Collections;

public class DonateController : MonoBehaviour {

    GestureRecognizer recognizer;
    ShotScript shotScript;
    
	// Use this for initialization
	void Start () {
        shotScript = GameObject.FindWithTag("MainCamera").GetComponent<ShotScript>();
        int shots = shotScript.numShots;
        
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            ClickButton(ray);
        };

        recognizer.StartCapturingGestures();
        
        Text text = gameObject.GetComponent<Text>();
        text.text = "You shot a total of "+shots+" dimes. A $"+shots/10.0f+" donation to Acterra would help plant nearly "+Mathf.Round(shots/10.0f)+ " trees!";
        //You shot a total of [X] pennies. With $X donation to acterra, you could plant nearly X/100 trees. 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void YesDonate() {
        // :)!!!!!!
        // $1 for one tree
    }
    
    public void NoDonate() {
        // :( :( :( 
    }
    
    void ClickButton(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if ( hit.transform.gameObject.name == "YesButton" ) {
                YesDonate();
            } else if ( hit.transform.gameObject.name == "NoButton" ) {
                NoDonate();
            }
        }
    }
}
