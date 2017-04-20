using UnityEngine;
using System.Collections;

public class AntiViralProtein_CellGameplayScript : MonoBehaviour {

    public GameObject subtitles; 
	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case 214:
                
                break;
            default:
                break;
        }

    }
}
