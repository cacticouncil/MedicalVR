using UnityEngine;
using System.Collections;

public class cGAMP_CellGameplayScript : MonoBehaviour {

    public GameObject target, subtitles, body;
    float moveSpeed = 40;
	// Use this for initialization
	void Start ()
    {
        body.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case (96):
                body.SetActive(true);
                break;
            case (145):
                body.SetActive(false);
                break;
            default:
                break;
        }
        if (subtitles.GetComponent<SubstitlesScript>().theTimer >106)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * moveSpeed);
        }
	}
}
