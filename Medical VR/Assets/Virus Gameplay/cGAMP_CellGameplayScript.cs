using UnityEngine;
using System.Collections;

public class cGAMP_CellGameplayScript : MonoBehaviour {

    public GameObject target, subtitles, body;
    float moveSpeed = 400;
	// Use this for initialization
	void Start ()
    {
        body.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case (46):
                body.SetActive(true);
                break;
            case (64):
                body.SetActive(false);
                break;
            default:
                break;
        }
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 48)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * moveSpeed);
        }
	}
}
