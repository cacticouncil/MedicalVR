using UnityEngine;
using System.Collections;

public class _TEnzymeController : MonoBehaviour
{
    public GameObject particles;

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
        {
            if (particles)
                Instantiate(particles, transform.position, transform.rotation, transform);

            GetComponent<_TTravelToNucleus>().StartTravel();
        }
    }
}