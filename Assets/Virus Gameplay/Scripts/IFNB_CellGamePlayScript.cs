﻿using UnityEngine;
using System.Collections;

public class IFNB_CellGamePlayScript : MonoBehaviour {

    public GameObject subtitles, target, proteins;

    private float moveSpeed = .004f;
	// Use this for initialization
	void Start ()
    {
        GetComponent<Renderer>().enabled = false;
        proteins.SetActive(false);
        switch (CellGameplayScript.loadCase)
        {
            case 4:
                proteins.SetActive(true);
                gameObject.SetActive(false);
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
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed);
        }
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 251)
        {
            gameObject.SetActive(false);
            proteins.SetActive(true);
        }

    }
}
