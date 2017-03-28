using UnityEngine;
using System.Collections;
using System;

public class Login : MonoBehaviour, TimedInputHandler
{

    public FBscript hi;
    public void HandleTimeInput()
    {
        hi.HandleTimeInput();
    }
}
