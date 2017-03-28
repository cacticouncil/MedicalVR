using UnityEngine;
using System.Collections;
using System;

public class Invite : MonoBehaviour, TimedInputHandler
{
    public FBscript hi;
    public void HandleTimeInput()
    {
        hi.Invite();
    }
}
