using UnityEngine;

public static class GlobalVariables
{
    public static bool tutorial = false;
    public static bool arcadeMode = true;
    public static int difficulty = 0;
    public static int subtitles
    {
        get
        {
            return PlayerPrefs.GetInt("Subtitles");
        }
        set
        {
            PlayerPrefs.SetInt("Subtitles", value);
        }
    }

    public static float textDelay
    {
        get
        {
            return PlayerPrefs.GetFloat("TextDelay");
        }
        set
        {
            PlayerPrefs.SetFloat("TextDelay", value);
        }
    }

    public static float textSize
    {
        get
        {
            return PlayerPrefs.GetFloat("TextSize");
        }
        set
        {
            PlayerPrefs.SetFloat("TextSize", value);
        }
    }

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