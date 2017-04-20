using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sting_CellGameplayScript : MonoBehaviour {

    public List<GameObject> places = new List<GameObject>();
    public GameObject sting_pic, subtitles, cGAMP, leftP, rightP;
    // Use this for initialization
    float moveSpeed = 0;
    int I = 0;
	void Start ()
    {
        sting_pic.SetActive(false);
        cGAMP.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveTo();
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case (129):
                sting_pic.SetActive(true);
                break;
            case (130):
                subtitles.GetComponent<SubstitlesScript>().Stop();
                break;
            case 145:
                cGAMP.SetActive(true);
                break;
            case 159:
                I = 1;
                moveSpeed = 200;
                break;
            case 182:
                I = 2;
                moveSpeed = 200;
                break;
            default:
                break;
        }
    
    }
    void MoveTo()
    {
        if (I != places.Count)
            transform.position = Vector3.MoveTowards(transform.position, places[I].transform.position, moveSpeed * Time.deltaTime);
    }
    public void DoAction()
    {
        if (((int)subtitles.GetComponent<SubstitlesScript>().theTimer == 130))
        {
            subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
            subtitles.GetComponent<SubstitlesScript>().Continue();
            sting_pic.SetActive(false);
        }
    }
}
