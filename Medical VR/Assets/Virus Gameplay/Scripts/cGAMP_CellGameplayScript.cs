using UnityEngine;
using System.Collections;

public class cGAMP_CellGameplayScript : MonoBehaviour
{
    public GameObject target, subtitles;
    float moveSpeed = .4f;

    // Use this for initialization
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case (96):
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            case (145):
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            default:
                break;
        }
        if (subtitles.GetComponent<SubstitlesScript>().theTimer > 106)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * moveSpeed);
        }
    }
}
