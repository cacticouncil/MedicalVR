﻿using UnityEngine;
using System.Collections;

public class BloodVesselScript : MonoBehaviour {

    public GameObject enter;
    public GameObject exit;
    public GameObject Effects;
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
            if (enter.GetComponent<VesselOpening>().shouldCollide == true && exit.GetComponent<VesselOpening>().shouldCollide == true)
            {
                other.GetComponent<MovingCamera>().LoseresetPos();
                Effects.GetComponent<ParticleSystem>().Stop();
                Effects.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}