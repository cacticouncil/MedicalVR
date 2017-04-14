using UnityEngine;
using System.Collections;

public class LookAtObjectScript : MonoBehaviour {

    public GameObject subtitles;
    public string theTag;
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
        if (theTag == "Cell" && (int)subtitles.GetComponent<SubstitlesScript>().theTimer == 35)
        {
            subtitles.GetComponent<SubstitlesScript>().Continue();
            subtitles.GetComponent<SubstitlesScript>().theTimer+= 1;
        }

        else if (theTag == "Virus" && (int)subtitles.GetComponent<SubstitlesScript>().theTimer == 65)
        {
            subtitles.GetComponent<SubstitlesScript>().Continue();
            subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
        }
    }
}
