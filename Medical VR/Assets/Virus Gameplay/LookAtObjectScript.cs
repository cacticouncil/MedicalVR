using UnityEngine;
using System.Collections;

public class LookAtObjectScript : MonoBehaviour {

    public GameObject subtitles;
    public string tag;
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
        if (tag == "Cell" && (int)subtitles.GetComponent<SubstitlesScript>().theTimer == 20)
        {
            subtitles.GetComponent<SubstitlesScript>().Continue();
            subtitles.GetComponent<SubstitlesScript>().theTimer+= 1;
        }

        else if (tag == "Virus" && (int)subtitles.GetComponent<SubstitlesScript>().theTimer == 35)
        {
            subtitles.GetComponent<SubstitlesScript>().Continue();
            subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
        }
    }
}
