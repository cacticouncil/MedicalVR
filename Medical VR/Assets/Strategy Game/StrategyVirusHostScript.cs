using UnityEngine;
using System.Collections;

public class StrategyVirusHostScript : StrategyVirusScript
{
    //This virus takes over the cell it is attacking using the cell's reproduction to make viruses instead of cells
    public override void Attack()
    {
        bool spawned = false;
        if (target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.None || target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.CH25H || target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.Mx1)
        {
            spawned = true;
            target.GetComponent<StrategyCellScript>().Treproduction -= target.GetComponent<StrategyCellScript>().reproduction;
            if (target.GetComponent<StrategyCellScript>().Treproduction <= 0)
            {
                //reproduce
                transform.parent.GetComponent<StrategyCellManagerScript>().SpawnVirusSingleAdjacent(target.GetComponent<StrategyCellScript>().key, transform.position);
                target.GetComponent<StrategyCellScript>().Treproduction = 10 + target.GetComponent<StrategyCellScript>().Treproduction;
            }
        }

        if (!spawned ||
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.RNase_L ||
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.PKR ||
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.TRIM22)
        {
            if (target.GetComponent<StrategyCellScript>().protein != StrategyCellScript.Proteins.IFIT || 
                (target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.IFIT && Random.Range(0.0f, 100.0f) > 90))
                transform.parent.GetComponent<StrategyCellManagerScript>().KillCell(target.GetComponent<StrategyCellScript>().key);
            Destroy(gameObject);
        }
    }
}
