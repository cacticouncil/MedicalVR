using UnityEngine;
using System.Collections;

public class DnaMove_Virus : MonoBehaviour {

    public GameObject target, subtitltes;
    bool move = false;
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        switch ((int)subtitltes.GetComponent<SubstitlesScript>().theTimer)
        {
            case (256):
                move = true;
                break;
            case (288):
                transform.position = target.transform.position;
                transform.rotation = target.transform.rotation;
                break;
            default:
                break;
        }
        if(move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 50);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, Time.deltaTime * 100);
        }
    }
}
