using UnityEngine;
using System.Collections;
using Cyberconian.Unity;

public class Clock : MonoBehaviour {

    SevenSegmentDriver displayDriver;
    public float startTime = 817f;
    public bool running = false;

    void Awake()
    {
        //GameFramework.OnStartTreeGrowth += Activate;
        //GameFramework.OnTreeWin += Deactivate;
        //GameFramework.OnTreeLose += Deactivate;
    }

    void Activate()
    {
        gameObject.SetActive(true);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {

        displayDriver = transform.FindChild("7SDG").GetComponent<SevenSegmentDriver>();

        Debug.AssertFormat(displayDriver != null, "Clock script could not find SevenSegmentDriver {0}", "7SDG");

        // Set initial time
        startTime = GameFramework.gameTime;
	}
	
	// Update is called once per frame
	void Update () {

        if (running)
        {
            // Perform countdown
            startTime -= Time.deltaTime / Time.timeScale;
        }

        if(startTime < 0f)
        {
            Pause();
            if(this.gameObject.activeSelf)
            {
                //GameFramework.OnFinishTreeWin(this.gameObject);
                GameFramework.TriggerTreeWin();
                this.gameObject.SetActive(false);
            }
        }

        // Update clock asset
        if (this.gameObject.activeSelf)
        {
            displayDriver.Data = SecondsToTimeStr(startTime);	
        }
	}

    public void Run()
    {
        running = true;
    }

    public void Pause()
    {
        running = false;
    }

    public string SecondsToTimeStr(float time)
    {
        int min = (int) time / 60;
        int sec = (int) time % 60;
        return string.Format("{0:D2}{1:D2}", min, sec);
    }
}
