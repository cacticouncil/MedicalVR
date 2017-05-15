using UnityEngine;
using System.Collections;

public class phosphate_CellGameplayScript : MonoBehaviour {

    public GameObject sting, subtitles, IRF3;
    public bool goLeft;
    float moveSpeed;

	// Update is called once per frame
	void FixedUpdate ()
    {
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 184)
        {
            transform.parent = IRF3.transform;
            moveSpeed = .2f;
            if (goLeft == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, IRF3.GetComponent<IRF3_CellGameplayScript>().leftP.transform.position, Time.fixedDeltaTime * moveSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, IRF3.GetComponent<IRF3_CellGameplayScript>().rightP.transform.position, Time.fixedDeltaTime * moveSpeed);
            }
        }
        else if(subtitles.GetComponent<SubstitlesScript>().theTimer > 180)
        {
            moveSpeed = .2f;
            if(goLeft == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, sting.GetComponent<Sting_CellGameplayScript>().leftP.transform.position, Time.fixedDeltaTime * moveSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, sting.GetComponent<Sting_CellGameplayScript>().rightP.transform.position, Time.fixedDeltaTime * moveSpeed);
            }
        }
	}
}
