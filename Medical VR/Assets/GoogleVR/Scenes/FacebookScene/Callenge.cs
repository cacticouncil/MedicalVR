﻿using UnityEngine;
using System.Collections;
using System;

public class Callenge : MonoBehaviour, TimedInputHandler
{

    public FBscript hi;
    public void HandleTimeInput()
    {
        hi.ShareWithUsers();
    }
}
