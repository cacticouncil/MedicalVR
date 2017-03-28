using UnityEngine;
using System.Collections;
using System;

public class Cancel : MonoBehaviour, TimedInputHandler {

    public OptionsController hi;
    public void HandleTimeInput()
    {
        hi.Exit();
    }
}
