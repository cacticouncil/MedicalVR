using UnityEngine;
using System.Collections;

public class OK : MonoBehaviour, TimedInputHandler {

    public OptionsController hi;
    public void HandleTimeInput()
    {
        hi.OnSettingsOK();
    }
}
