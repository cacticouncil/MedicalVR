using UnityEngine;
using System.Collections;

public class IFNB_CellGamePlayScript : MonoBehaviour {

    public GameObject subtitles, target, proteins;
    float moveSpeed;
	// Use this for initialization
	void Start ()
    {
        GetComponent<Renderer>().enabled = false;
        proteins.SetActive(false);
        switch (CellGameplayScript.loadCase)
        {
            case 4:
                proteins.SetActive(true);
                break;
            default:
                break;
        }

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 231)
        {
            GetComponent<Renderer>().enabled = true;
        }
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 244)
        {
            moveSpeed = 20;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * moveSpeed);
        }
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 250)
        {
            proteins.SetActive(true);
        }

    }
}
