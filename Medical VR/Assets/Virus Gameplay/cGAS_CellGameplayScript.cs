using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cGAS_CellGameplayScript : MonoBehaviour {

    public List<GameObject> places;
    public GameObject cGAS_pic, subtitles;
    delegate void Func();
    Func doAction;
    int I = 0;
    float moveSpeed;
    // Use this for initialization
    void Start ()
    {
        cGAS_pic.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckCase();
        //doAction();
        MoveTo();
    }
    void MoveTo()
    {
        if (I != places.Count)
            transform.position = Vector3.MoveTowards(transform.position, places[I].transform.position, moveSpeed * Time.deltaTime);
    }
    void CheckCase()
    {
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case (37):
                cGAS_pic.SetActive(true);
                break;
            case (39):
                subtitles.GetComponent<SubstitlesScript>().Stop();
                break;
            case (40):
                I = 1;
                moveSpeed = 50;
                break;
            default:
                break;
        }
    }
    public void DoAction()
    {
        if(((int)subtitles.GetComponent<SubstitlesScript>().theTimer == 39))
        {
            subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
            subtitles.GetComponent<SubstitlesScript>().Continue();
            cGAS_pic.SetActive(false);
        }
    }

}
