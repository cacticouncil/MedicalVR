using UnityEngine;
using System.Collections;

public class StrategyVirusExplosionScript : StrategyVirusScript
{
    //This virus takes over the cell it is attacking using the cell's reproduction to make viruses instead of cells
    public override void Attack()
    {
        bool spawned = false;
        if (target.protein == StrategyCellScript.Proteins.None || target.protein == StrategyCellScript.Proteins.CH25H || target.protein == StrategyCellScript.Proteins.Mx1)
        {
            spawned = true;
            parent.SpawnVirusAllAdjacent(target.key, transform.position);
        }
        if (spawned ||
            target.protein == StrategyCellScript.Proteins.RNase_L ||
            target.protein == StrategyCellScript.Proteins.PKR ||
            target.protein == StrategyCellScript.Proteins.TRIM22 ||
            (target.protein == StrategyCellScript.Proteins.IFIT && Random.Range(0.0f, 100.0f) > 90))
            parent.KillCell(target.key);
        Destroy(gameObject);
    }
}
