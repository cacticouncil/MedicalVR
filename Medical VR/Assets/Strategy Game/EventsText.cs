using UnityEngine;
using System.Collections;

public class EventsText : MonoBehaviour
{
    void OnEnable()
    {
        string nextEvent = StrategyCellManagerScript.instance.nextEvent.Method.ToString();
        nextEvent = nextEvent.Remove(0, 5);
        nextEvent = nextEvent.Remove(nextEvent.Length - 2, 2);
        nextEvent = nextEvent.Replace('_', ' ');
        nextEvent = nextEvent.Replace('z', '-');
        GetComponent<TMPro.TextMeshPro>().text =
           "Last Event: " + StrategyCellManagerScript.instance.lastEvent +
           "\nNext Event: " + nextEvent +
           " in " + (StrategyCellManagerScript.instance.eventTurns - StrategyCellManagerScript.instance.turnNumber % StrategyCellManagerScript.instance.eventTurns) + " turns";
    }
}
