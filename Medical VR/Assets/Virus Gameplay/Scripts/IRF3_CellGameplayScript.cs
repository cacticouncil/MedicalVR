using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IRF3_CellGameplayScript : MonoBehaviour {

    public GameObject subtitles, leftP, rightP;
    public List<GameObject> places;
    int I = 0;
    float moveSpeed = 0;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position = Vector3.MoveTowards(transform.position, places[I].transform.position, Time.fixedDeltaTime * moveSpeed);
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case 191:
                I = 1;
                transform.position = places[I].transform.position;
                break;
            case 194:
                I = 2;
                moveSpeed = .2f;
                break;
            default:
                break;
        }

    }
}
