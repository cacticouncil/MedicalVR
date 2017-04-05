using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrophyScript : MonoBehaviour {

    public GameObject pedestal, trophyPos, selectPos, trophyName, sub;
    public float speed, rotationSpeed;
    public List<SubstitlesScript.Subtitle> subtitle = new List<SubstitlesScript.Subtitle>();
    bool moveForward, inPedestal;
    Vector3 orgPos, target, pedestalTarget;
    Quaternion orgRotation;
    // Use this for initialization
    void Start ()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("InitialSFXVolume");
        orgRotation = transform.rotation;
        moveForward = false;
        inPedestal = false;
        orgPos = transform.position;
        SetTarget();
	}
	void SetTarget()
    {
        target = selectPos.transform.position;
        pedestalTarget = trophyPos.transform.position;
    }
	// Update is called once per frame
	void Update ()
    {
        if(inPedestal == false)
        {
            if (moveForward)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, orgPos, speed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pedestalTarget, speed * Time.deltaTime);
            InSelection();
        }
	}

    void InSelection()
    {
        if (transform.position == pedestalTarget)
        {
            if (transform.rotation.eulerAngles.z == 0)
            {
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }
            else if (transform.rotation.eulerAngles.z == 90)
            {
                transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
            }
        }
    }
    public void GazedOn()
    {
        if(pedestal.GetComponent<PedestalScript>().inUse == true)
        moveForward = true;
    }
    public void GazeOff()
    {
        if(pedestal.GetComponent<PedestalScript>().inUse == true)
            moveForward = false;
    }
    public void BringToPedestal()
    {
        if(pedestal.GetComponent<PedestalScript>().inUse == true)
        {
            if (inPedestal == false && pedestal.GetComponent<PedestalScript>().selectedTrophy == null || inPedestal == true)
            {
                inPedestal = !inPedestal;
                if (inPedestal == true)
                {
                    pedestal.GetComponent<PedestalScript>().selectedTrophy = this.gameObject;
                    pedestal.GetComponent<PedestalScript>().descButton.SetActive(true);
                    trophyName.SetActive(false);
                }
                if (inPedestal == false)
                {
                    transform.rotation = orgRotation;
                    pedestal.GetComponent<PedestalScript>().descButton.SetActive(false);
                    pedestal.GetComponent<PedestalScript>().selectedTrophy = null;
                    GetComponent<AudioSource>().Stop();
                    sub.GetComponent<SubstitlesScript>().Stop();
                    pedestal.GetComponent<PedestalScript>().RevertButtonAction();
                    trophyName.SetActive(true);
                }
            }
        }
    }
    public void PlayDescription()
    {
        GetComponent<AudioSource>().Play();
        sub.GetComponent<SubstitlesScript>().theSubtitles = subtitle;
        sub.GetComponent<SubstitlesScript>().Replay();
    }
}
