using UnityEngine;
using System.Collections;

public class BloodVesselScript : MonoBehaviour {

   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "virus")
        {
            other.GetComponent<MovingCamera>().resetPos();
        }
    }
}
