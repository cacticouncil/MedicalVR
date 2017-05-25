using UnityEngine;
using System.Collections;

public class StrategyVirusHostScript : StrategyVirusScript
{
    //This virus takes over the cell it is attacking using the cell's reproduction to make viruses instead of cells
    public override void Attack()
    {
        if (target.protein == Proteins.RNase_L || target.protein == Proteins.PKR)
        {
            StrategyCellManagerScript.instance.KillCell(target.key);
            StartCoroutine(Die());
            return;
        }

        target.Treproduction -= target.reproduction;
        if (target.Treproduction <= 0)
        {
            //reproduce
            StrategyCellManagerScript.instance.SpawnVirusSingleAdjacent(target.key, transform.position);
            if (target.RDur > 0)
            {
                target.Treproduction -= StrategyCellScript.rBonus;
                target.RDur--;
                if (target.RDur == 0)
                {
                    target.r.color = Color.white;
                }
            }
            else
            {
                target.Treproduction = StrategyCellScript.reproductionReset + target.Treproduction;
            }
        }
    }
}
