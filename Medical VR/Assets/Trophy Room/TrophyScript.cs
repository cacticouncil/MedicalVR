using UnityEngine;
using System.Collections;

public class TrophyScript : MonoBehaviour {

    public GameObject pedestal, trophyPos, selectPos, trophyName;
    Vector3 orgPos, target, pedestalTarget;
    public float speed, rotationSpeed;
    bool moveForward, inPedestal;
    Quaternion orgRotation;
	// Use this for initialization
	void Start ()
    {
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
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    public void GazedOn()
    {
        moveForward = true;
    }
    public void GazeOff()
    {
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
                    trophyName.SetActive(true);
                }
            }
        }
    }
    public void PlayDescription()
    {
        GetComponent<AudioSource>().Play();
    }
}
