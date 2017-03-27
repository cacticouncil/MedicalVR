using UnityEngine;
using System.Collections;

public class _TEnzymeController : MonoBehaviour
{
    [HideInInspector]
    public bool hasATP = false, hasGTP = false;

    public void SetATP()
    {
        hasATP = true;
        UpdateSettings();
    }

    public void SetGTP()
    {
        hasGTP = true;
        UpdateSettings();
    }
    
    private void UpdateSettings()
    {
        if (hasGTP && hasATP)
            GetComponent<_TTravelToNucleus>().StartTravel();
    }
}