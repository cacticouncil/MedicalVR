using UnityEngine;
using System.Collections;

public class _TEnzymeController : MonoBehaviour
{
    public GameObject particles;
    public int pointsValue;

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

            if(GetComponent<_TTravelToNucleus>())
                GetComponent<_TTravelToNucleus>().StartTravel();
            if (!transform.parent)
                return;
            if (transform.parent.parent.GetComponent<_TGameController>())
            {
                transform.parent.parent.GetComponent<_TGameController>().AddToScore(pointsValue);
            }
            else
                Debug.Log("Unable to Access Game Controller");

            transform.parent = null;
        }
    }
}