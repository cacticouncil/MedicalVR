using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface DragDropHandler : IEventSystemHandler {

    void HandleGazeTriggerStart();

    void HandleGazeTriggerEnd();

}

