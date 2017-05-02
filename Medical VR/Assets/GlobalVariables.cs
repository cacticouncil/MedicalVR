using UnityEngine;

public static class GlobalVariables
{
    public static float textDelay = .1f;
    public static bool tutorial = false;
    public static bool arcadeMode = true;
    public static int difficulty = 0;

    public static int VirusGameplayCompleted
    {
        get
        {
            return PlayerPrefs.GetInt("VirusComplete");
        }
        set
        {
            PlayerPrefs.SetInt("VirusComplete", value);
        }
    }

    public static int CellGameplayCompleted
    {
        get
        {
            return PlayerPrefs.GetInt("CellComplete");
        }
        set
        {
            PlayerPrefs.SetInt("CellComplete", value);
        }
    }
}