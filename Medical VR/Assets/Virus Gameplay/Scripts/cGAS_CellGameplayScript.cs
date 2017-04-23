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
    bool doAct = false;
    // Use this for initialization
    void Start ()
    {
        cGAS_pic.SetActive(false);
        switch (CellGameplayScript.loadCase)
        {
            case (1):
                I = 1;
                transform.position = places[I].transform.position;
                break;
            default:
                break;
        }
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
        float t = subtitles.GetComponent<SubstitlesScript>().theTimer;
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case (43):
                cGAS_pic.SetActive(true);
                break;
            case (44):
                if(t >= 44.5)
                subtitles.GetComponent<SubstitlesScript>().Stop();
                break;
            case (46):
                if(doAct == true)
                I = 1;
                moveSpeed = 50;
                break;
            default:
                break;
        }
    }
    public void DoAction()
    {
        if(((int)subtitles.GetComponent<SubstitlesScript>().theTimer == 44))
        {
            doAct = true;
            subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
            subtitles.GetComponent<SubstitlesScript>().Continue();
            cGAS_pic.SetActive(false);
        }
    }

}
