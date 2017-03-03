using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface TimedInputHandler : IEventSystemHandler {

    void HandleTimeInput();
}
