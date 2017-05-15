using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement_VirusGameplay : MonoBehaviour {

    public GameObject cam;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        GetComponent<Rigidbody>().velocity = cam.transform.forward*speed *10;
    }
}
