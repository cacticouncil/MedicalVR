using UnityEngine;
using System.Collections;

public class VesselOpening : MonoBehaviour {

    public GameObject otherSide;
    public bool shouldCollide;
	// Use this for initialization
	void Start () {
        shouldCollide = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "virus")
        {
            shouldCollide = !shouldCollide;
        }
        else if(otherSide != null)
        {
            other.transform.position = new Vector3(otherSide.transform.position.x, other.transform.position.y, other.transform.position.z);
        }
    }
}
