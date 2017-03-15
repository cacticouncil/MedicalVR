using UnityEngine;
using System.Collections;

public class StrategyVirusExplosionScript : StrategyVirusScript
{
    //This virus takes over the cell it is attacking using the cell's reproduction to make viruses instead of cells
    public override void Attack()
    {
        bool spawned = false;
        if (target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.None || target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.CH25H || target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.Mx1)
        {
            spawned = true;
            transform.parent.GetComponent<StrategyCellManagerScript>().SpawnVirusAllAdjacent(target.GetComponent<StrategyCellScript>().key, transform.position);
        }
        if (spawned ||
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.RNase_L ||
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.PKR ||
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.TRIM22 ||
            (target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.IFIT && Random.Range(0.0f, 100.0f) > 90))
            transform.parent.GetComponent<StrategyCellManagerScript>().KillCell(target.GetComponent<StrategyCellScript>().key);
        Destroy(gameObject);
    }
}
