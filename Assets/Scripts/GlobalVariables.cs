using UnityEngine;

public static class GlobalVariables
{
    public static bool tutorial = false;
    public static bool arcadeMode = true;

    public static int subtitles
    {
        get
        {
            return PlayerPrefs.GetInt("Subtitles", 1);
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
            return PlayerPrefs.GetFloat("TextDelay", .05f);
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
            return PlayerPrefs.GetFloat("TextSize", .75f);
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
            return PlayerPrefs.GetInt("VirusComplete", 0);
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
            return PlayerPrefs.GetInt("CellComplete", 0);
        }
        set
        {
            PlayerPrefs.SetInt("CellComplete", value);
        }
    }

    public static float sfxVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("InitialSFXVolume", .5f);
        }
        set
        {
            PlayerPrefs.SetFloat("InitialSFXVolume", value);
        }
    }

    public static float bgmVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("InitialBGMVolume", .5f);
        }
        set
        {
            PlayerPrefs.SetFloat("InitialBGMVolume", value);
        }
    }
}