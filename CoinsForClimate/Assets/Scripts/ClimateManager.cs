using UnityEngine;
using System.Collections;

public class ClimateManager : MonoBehaviour {

    public delegate void ClimateAction();
    public static event ClimateAction StartGame;
    public static event ClimateAction ClimateChange;
    public static event ClimateAction ClimateImprove;
    public static event ClimateAction ClimateFall;

    private static int MIN_STRENGTH = 0;
    private static int MAX_STRENGTH = 9;
    public static int ClimateStrength = 4;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ClimateStrength++;
            TreeBoost();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ClimateStrength--;
            TreeHit();
        }
	}

    public static float GetClimateStrength()
    {
        float climateStrength = (float) ClimateStrength / MAX_STRENGTH;
        return climateStrength;
    }

    public static void BroadcastStart()
    {
        if (StartGame != null) StartGame();
    }

    public static void TreeHit()
    {
        print("Tree Hit");
        DecrementClimateStrength();
        if (ClimateChange != null) ClimateChange();
        if (ClimateFall != null) ClimateFall();
    }

    public static void TreeBoost()
    {
        IncrementClimateStrength();
        if (ClimateChange != null) ClimateChange();
        if (ClimateImprove != null) ClimateImprove();
    }

    private static void IncrementClimateStrength()
    {
        ClimateStrength = (ClimateStrength < MAX_STRENGTH) ? ClimateStrength + 1 : 9;
    }

    private static void DecrementClimateStrength()
    {
        ClimateStrength = (ClimateStrength > MIN_STRENGTH) ? ClimateStrength - 1 : 0;
    } 
}
