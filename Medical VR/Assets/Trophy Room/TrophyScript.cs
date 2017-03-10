using UnityEngine;
using System.Collections;

public class TrophyScript : MonoBehaviour {

    public GameObject pedestal, trophyPos, selectPos;
    Vector3 orgPos, target, pedestalTarget;
    public float speed, rotationSpeed;
    bool moveForward, inPedestal;

	// Use this for initialization
	void Start ()
    {
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
        inPedestal = !inPedestal;
    }
}
