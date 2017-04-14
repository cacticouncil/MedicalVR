using UnityEngine;
using System.Collections;

public class phosphate_CellGameplayScript : MonoBehaviour {

    public GameObject sting, subtitles, IRF3;
    public bool goLeft;
    float moveSpeed;
    Vector3 target;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 83)
        {
            transform.parent = IRF3.transform;
            moveSpeed = 200;
            if (goLeft == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, IRF3.GetComponent<IRF3_CellGameplayScript>().leftP.transform.position, Time.deltaTime * moveSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, IRF3.GetComponent<IRF3_CellGameplayScript>().rightP.transform.position, Time.deltaTime * moveSpeed);
            }
        }
        else if(subtitles.GetComponent<SubstitlesScript>().theTimer > 75)
        {
            moveSpeed = 200;
            if(goLeft == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, sting.GetComponent<Sting_CellGameplayScript>().leftP.transform.position, Time.deltaTime * moveSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, sting.GetComponent<Sting_CellGameplayScript>().rightP.transform.position, Time.deltaTime * moveSpeed);
            }
        }
	}
}
