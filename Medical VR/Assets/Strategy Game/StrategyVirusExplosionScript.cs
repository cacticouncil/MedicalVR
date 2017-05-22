using UnityEngine;
using System.Collections;

public class StrategyVirusExplosionScript : StrategyVirusScript
{
    //This virus takes over the cell it is attacking using the cell's reproduction to make viruses instead of cells
    public override void Attack()
    {
        bool spawned = false;
        if (target.protein == Proteins.None || target.protein == Proteins.CH25H || target.protein == Proteins.Mx1)
        {
            spawned = true;
            parent.SpawnVirusAllAdjacent(target.key, transform.position);
        }
        if (spawned ||
            target.protein == Proteins.RNase_L ||
            target.protein == Proteins.PKR ||
            target.protein == Proteins.TRIM22 ||
            (target.protein == Proteins.IFIT && Random.Range(0.0f, 100.0f) > 75))
            parent.KillCell(target.key);
        StartCoroutine(Die());
    }
}
