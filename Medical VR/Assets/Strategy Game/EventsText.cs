using UnityEngine;
using System.Collections;

public class EventsText : MonoBehaviour
{
    public StrategyCellScript c;
    private string baseText =
        "Migrating White Cells" +
        "\nSpread Immunity Faster" +
        "\nFree Power-ups" +
        "\nDefend Cells" +
        "\nStrengthen Viruses" +
        "\nAccelerate Viruses" +
        "\nMutate Viruses" +
        "\nMigrate Viruses" +
        "\nAsymptomatic Carriers";

    void OnEnable()
    {
        string nextEvent = c.parent.nextEvent.Method.ToString();
        nextEvent = nextEvent.Remove(0, 5);
        nextEvent = nextEvent.Remove(nextEvent.Length - 2, 2);
        nextEvent = nextEvent.Replace('_', ' ');
        nextEvent = nextEvent.Replace('z', '-');
        GetComponent<TMPro.TextMeshPro>().text = baseText +
           "\nLast Event: " + c.parent.lastEvent +
           "\nNext Event: " + nextEvent +
           " in " + (c.parent.eventTurns - c.parent.turnNumber % c.parent.eventTurns) + " turns";
    }
}
