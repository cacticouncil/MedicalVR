using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sting_CellGameplayScript : MonoBehaviour
{
    public List<Transform> places = new List<Transform>();
    public GameObject subtitles, cGAMP, leftP, rightP;

    private float vComparer = .01f;
    private float moveSpeed = .004f;
    private bool moved = true;

    // Use this for initialization
    void Start()
    {
        cGAMP.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case (130):
                subtitles.GetComponent<SubstitlesScript>().Stop();
                break;
            case 145:
                cGAMP.SetActive(true);
                break;
            case 159:
                if (moved)
                {
                    moved = false;
                    StartCoroutine(MoveTo(places[0]));
                }
                break;
            case 182:
                if (moved)
                {
                    moved = false;
                    StartCoroutine(MoveTo(places[1]));
                }
                break;
            default:
                break;
        }

    }

    IEnumerator MoveTo(Transform t)
    {
        while (!V3Equal(transform.position, t.position))
        {
            transform.position = Vector3.MoveTowards(transform.position, t.position, moveSpeed);
            yield return new WaitForFixedUpdate();
        }
        moved = true;
    }

    public void DoAction()
    {
        if (((int)subtitles.GetComponent<SubstitlesScript>().theTimer == 130))
        {
            subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
            subtitles.GetComponent<SubstitlesScript>().Continue();
        }
    }

    private bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < vComparer;
    }
}
