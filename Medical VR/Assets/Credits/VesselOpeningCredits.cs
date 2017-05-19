using UnityEngine;
using System.Collections;

public class VesselOpeningCredits : MonoBehaviour
{
    public Transform otherSide = null;

    void OnTriggerExit(Collider other)
    {
        if (otherSide != null)
        {
            Vector3 diff = transform.position - otherSide.position;
            diff *= .9f;
            other.transform.position = other.transform.position - diff;
        }
    }
}
