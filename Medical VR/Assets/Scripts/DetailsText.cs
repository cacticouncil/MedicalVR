using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DetailsText : MonoBehaviour
{
    public StrategyCellScript cell;
    public TMPro.TextMeshPro t;

    int turnSpawned = 0;
    int turnsToReproduce = 0;
    int childrenSpawned = 0;
    int immunitySpread = 0;

    void OnEnable()
    {
        if (t == null)
            t = GetComponent<TMPro.TextMeshPro>();

        if (cell == null)
            cell = GetComponentInParent<StrategyCellScript>();

        turnSpawned = cell.turnSpawned;

        float tr = cell.Treproduction;
        float r = cell.reproduction;
        float rd = cell.RDur;
        int tt = -1;
        while (tr > 0)
        {
            if (rd > 0)
            {
                tr -= StrategyCellScript.rBonus;
                rd--;
            }

            tr -= Mathf.Sqrt(r * 10 + 1);
            tt++;
        }

        turnsToReproduce = tt;
        childrenSpawned = cell.childrenSpawned;
        immunitySpread = (int)(cell.immunitySpread);
        t.text = "Turn Spawned: " + turnSpawned +
            "\nTurns To Reproduce: " + turnsToReproduce +
            "\nChildren Spawned: " + childrenSpawned +
            "\nImmunity Spread: " + immunitySpread +
            "\nCells Alive: " + StrategyCellManagerScript.instance.cellNum +
            "\nViruses Alive: " + StrategyCellManagerScript.instance.virNum;
    }
}
