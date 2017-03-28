using UnityEngine;
using System.Collections;

public class LookAtObjectScript : MonoBehaviour {

    public GameObject subtitles;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    public void LookedAtAction()
    {
        subtitles.GetComponent<SubstitlesScript>().Continue();
    }
}
