using UnityEngine;
using System.Collections;

public class StrategyVirusHostScript : StrategyVirusScript
{
    //This virus takes over the cell it is attacking using the cell's reproduction to make viruses instead of cells
    public override void Attack()
    {
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

        if (target.protein == Proteins.RNase_L || target.protein == Proteins.PKR)
        {
            parent.KillCell(target.key);
            StartCoroutine(Die());
        }
    }
}
