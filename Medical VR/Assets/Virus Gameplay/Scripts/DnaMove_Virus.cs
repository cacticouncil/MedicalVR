using UnityEngine;
using System.Collections;

public class DnaMove_Virus : MonoBehaviour
{
    public GameObject target, subtitltes;
    bool move = false;

    // Update is called once per frame
    void FixedUpdate()
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
        if (move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, .1f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, 2);
        }
    }
}
