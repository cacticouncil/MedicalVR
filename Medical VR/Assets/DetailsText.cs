using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DetailsText : MonoBehaviour {

    public StrategyCellScript cell;
    private Text t;

    int turnSpawned = 0;
    int turnsToReproduce = 0;
    int childrenSpawned = 0;
    int immunitySpread = 0;

    // Update is called once per frame
    void Update ()
    {
        if (t == null)
            t = GetComponent<Text>();

        turnSpawned = cell.turnSpawned;
        turnsToReproduce = cell.Treproduction / cell.reproduction;
        childrenSpawned = cell.childrenSpawned;
        immunitySpread = cell.immunitySpread;
        t.text = "Turn Spawned: " + turnSpawned + "\nTurns To Reproduce: " + turnsToReproduce + "\nChildren Spawned: " + childrenSpawned + "\nImmunity Spread: " + immunitySpread;
	}
}
