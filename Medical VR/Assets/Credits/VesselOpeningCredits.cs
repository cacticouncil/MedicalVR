using UnityEngine;
using System.Collections;

public class VesselOpeningCredits : MonoBehaviour
{
    public Transform otherSide = null;
    public bool shouldCollide;
    // Use this for initialization
    void Start()
    {
        shouldCollide = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldCollide = !shouldCollide;
        }
        else if (otherSide != null)
        {
            Vector3 diff = transform.position - otherSide.position;
            diff *= .9f;
            other.transform.position = other.transform.position - diff;
        }
    }
}
