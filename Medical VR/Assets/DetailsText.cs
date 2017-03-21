using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DetailsText : MonoBehaviour {

    public StrategyCellScript cell;
    public TextMesh t;

    int turnSpawned = 0;
    int turnsToReproduce = 0;
    int childrenSpawned = 0;
    int immunitySpread = 0;
    
    void OnEnable ()
    {
        if (t == null)
            t = GetComponent<TextMesh>();

        if (cell == null)
            cell = GetComponentInParent<StrategyCellScript>();

        turnSpawned = cell.turnSpawned;
        turnsToReproduce = (int)(cell.Treproduction / cell.reproduction);
        childrenSpawned = cell.childrenSpawned;
        immunitySpread = (int)(cell.immunitySpread);
        t.text = "Turn Spawned: " + turnSpawned + "\nTurns To Reproduce: " + turnsToReproduce + "\nChildren Spawned: " + childrenSpawned + "\nImmunity Spread: " + immunitySpread;
	}
}
