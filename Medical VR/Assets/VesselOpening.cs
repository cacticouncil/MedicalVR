using UnityEngine;
using System.Collections;

public class VesselOpening : MonoBehaviour {

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
    }
}
