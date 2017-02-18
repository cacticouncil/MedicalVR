using UnityEngine;
using System.Collections;

public class StrategyVirusExplosionScript : StrategyVirusScript
{
    //This virus takes over the cell it is attacking using the cell's reproduction to make viruses instead of cells
    public override void Attack()
    {
        GetComponentInParent<StrategyCellManagerScript>().SpawnVirusAllAdjacent(target.GetComponent<StrategyCellScript>().key);
        GetComponentInParent<StrategyCellManagerScript>().KillCell(target.GetComponent<StrategyCellScript>().key);
        target = GetComponentInParent<StrategyCellManagerScript>().FindVirusNewTarget(gameObject);
        startingPosition = transform.position;
    }
}
