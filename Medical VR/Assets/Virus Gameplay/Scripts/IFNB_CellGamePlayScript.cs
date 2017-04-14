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
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 106)
        {
            GetComponent<Renderer>().enabled = true;
        }
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 110)
        {
            moveSpeed = 200;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * moveSpeed);
        }
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 114)
        {
            proteins.SetActive(true);
        }

    }
}
