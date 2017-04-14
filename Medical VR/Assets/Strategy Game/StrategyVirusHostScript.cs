using UnityEngine;
using System.Collections;

public class StrategyVirusHostScript : StrategyVirusScript
{
    //This virus takes over the cell it is attacking using the cell's reproduction to make viruses instead of cells
    public override void Attack()
    {
        bool spawned = false;
        if (target.protein == Proteins.None || target.protein == Proteins.CH25H || target.protein == Proteins.Mx1)
        {
            spawned = true;
            target.Treproduction -= target.reproduction;
            if (target.Treproduction <= 0)
            {
                //reproduce
                parent.SpawnVirusSingleAdjacent(target.key, transform.position);
                if (target.RDur > 0)
                {
                    target.Treproduction -= target.rBonus;
                    target.RDur--;
                    if (target.RDur == 0)
                    {
                        target.r.color = Color.white;
                    }
                }
                else
                {
                    target.Treproduction = target.reproductionReset + target.Treproduction;
                }
            }
        }

        if (!spawned ||
            target.protein == Proteins.RNase_L ||
            target.protein == Proteins.PKR ||
            target.protein == Proteins.TRIM22)
        {
            if (target.protein != Proteins.IFIT || 
                (target.protein == Proteins.IFIT && Random.Range(0.0f, 100.0f) > 90))
                parent.KillCell(target.GetComponent<StrategyCellScript>().key);
            Destroy(gameObject);
        }
    }
}
