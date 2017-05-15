using UnityEngine;
using System.Collections;

public class phosphate_CellGameplayScript : MonoBehaviour
{
    public Transform sting;
    public GameObject subtitles, IRF3;
    public bool goLeft;

    private float moveSpeed = .004f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 184)
        {
            transform.parent = IRF3.transform;
            if (goLeft == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, IRF3.GetComponent<IRF3_CellGameplayScript>().leftP.transform.position, moveSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, IRF3.GetComponent<IRF3_CellGameplayScript>().rightP.transform.position, moveSpeed);
            }
        }
        else if (subtitles.GetComponent<SubstitlesScript>().theTimer > 180)
        {
            if (goLeft == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, sting.GetComponent<Sting_CellGameplayScript>().leftP.transform.position, moveSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, sting.GetComponent<Sting_CellGameplayScript>().rightP.transform.position, moveSpeed);
            }
        }
    }
}
