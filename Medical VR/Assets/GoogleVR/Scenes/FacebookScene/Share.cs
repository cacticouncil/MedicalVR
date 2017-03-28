using UnityEngine;
using System.Collections;
using System;

public class Share : MonoBehaviour, TimedInputHandler
{
    public FBscript hi;
    public void HandleTimeInput()
    {
        hi.Share();
    }
}
