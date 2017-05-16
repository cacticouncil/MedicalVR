using UnityEngine;
using System.Collections;

public class LookAtObjectScript : MonoBehaviour {

    public GameObject subtitles;
    public string theTag;

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
