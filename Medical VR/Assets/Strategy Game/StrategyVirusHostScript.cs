using UnityEngine;
using System.Collections;

public class StrategyVirusHostScript : StrategyVirusScript
{
    //This virus takes over the cell it is attacking using the cell's reproduction to make viruses instead of cells
    public override void Attack()
    {
        if (target.GetComponent<StrategyCellScript>().immunity < 10)
        {
            target.GetComponent<StrategyCellScript>().Treproduction -= target.GetComponent<StrategyCellScript>().reproduction;
            if (target.GetComponent<StrategyCellScript>().Treproduction <= 0)
            {
                //reproduce
                GetComponentInParent<StrategyCellManagerScript>().SpawnVirusSingleAdjacent(target.GetComponent<StrategyCellScript>().key);
                target.GetComponent<StrategyCellScript>().Treproduction = 10 + target.GetComponent<StrategyCellScript>().Treproduction;
            }
        }
    }
}
