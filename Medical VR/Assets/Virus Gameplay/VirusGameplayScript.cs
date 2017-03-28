using UnityEngine;
using System.Collections;

public class VirusGameplayScript : MonoBehaviour {

    public GameObject subtitltes, blackCurtain;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
       // blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
        switch ((int)subtitltes.GetComponent<SubstitlesScript>().theTimer)
        {
            case (2):

                break;
            default:
                break;
        }
    }
}
