using UnityEngine;
using System.Collections;

public class VesselOpening : MonoBehaviour
{
    public GameObject otherSide;
    public bool shouldCollide;
    // Use this for initialization
    void Start()
    {
        shouldCollide = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldCollide = !shouldCollide;
        }
        else if (otherSide != null)
        {
            if (other.transform.position.x < transform.position.x)
                other.transform.position = new Vector3(otherSide.transform.position.x, other.transform.position.y, other.transform.position.z);
        }
    }
}
