using UnityEngine;
using System.Collections;

public class VesselOpening : MonoBehaviour
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
            diff *= 1.05f;
            other.transform.position = other.transform.position - diff;
        }
    }
}
