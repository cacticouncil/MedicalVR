using UnityEngine;
using System.Collections;

public class StrategyItemWhiteBloodCell : MonoBehaviour
{
    public StrategyVirusScript target;

    void Start()
    {
        target.enabled = false;
        //transform.parent.GetComponent<StrategyCellManagerScript>()
    }
}