using UnityEngine;
using System.Collections;

public class StrategyVirusHostScript : StrategyVirusScript
{
    //This virus takes over the cell it is attacking using the cell's reproduction to make viruses instead of cells
    public override void Attack()
    {
        bool spawned = false;
        if (target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.proteins.None || target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.proteins.CH25H || target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.proteins.Mx1)
        {
            spawned = true;
            target.GetComponent<StrategyCellScript>().Treproduction -= target.GetComponent<StrategyCellScript>().reproduction;
            if (target.GetComponent<StrategyCellScript>().Treproduction <= 0)
            {
                //reproduce
                transform.parent.GetComponent<StrategyCellManagerScript>().SpawnVirusSingleAdjacent(target.GetComponent<StrategyCellScript>().key);
                target.GetComponent<StrategyCellScript>().Treproduction = 10 + target.GetComponent<StrategyCellScript>().Treproduction;
            }
        }

        if (!spawned ||
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.proteins.RNase_L ||
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.proteins.PKR ||
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.proteins.TRIM22)
        {
            if (target.GetComponent<StrategyCellScript>().protein != StrategyCellScript.proteins.IFIT || 
                (target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.proteins.IFIT && Random.Range(0.0f, 100.0f) > 90))
                transform.parent.GetComponent<StrategyCellManagerScript>().KillCell(target.GetComponent<StrategyCellScript>().key);
            Destroy(gameObject);
        }
    }
}
