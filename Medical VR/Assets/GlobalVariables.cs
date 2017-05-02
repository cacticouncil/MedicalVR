using UnityEngine;

public static class GlobalVariables
{
    public static float textDelay = .1f;
    public static bool tutorial = false;
    public static bool arcadeMode = true;

    public static int VirusGameplayCompleted = PlayerPrefs.GetInt("VirusComplete");
    public static int CellGameplayCompleted =  PlayerPrefs.GetInt("CellComplete");
}