using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoNucleus : MonoBehaviour {

    public GameObject Manager;
    bool doStuff = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(doStuff == true && (int)Manager.GetComponent<VirusGameplayScript>().subtitles.GetComponent<SubstitlesScript>().theTimer == 249)
        {
            Manager.GetComponent<VirusGameplayScript>().LowerCurtain();
            if(Manager.GetComponent<VirusGameplayScript>().blackCurtain.GetComponent<Renderer>().material.color.a >=1)
            {
                Manager.GetComponent<VirusGameplayScript>().GoToNucleus();
                this.gameObject.SetActive(false);
            }
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            doStuff = true;
        }
    }
}
