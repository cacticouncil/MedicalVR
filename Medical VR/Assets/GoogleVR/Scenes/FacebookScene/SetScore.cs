using UnityEngine;
using System.Collections;
using System;

public class SetScore : MonoBehaviour, TimedInputHandler
{
    public FBscript hi;
    public void HandleTimeInput()
    {
        hi.SetScore(FacebookManager.Instance.GlobalScore);
    }
}
