using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IRF3_CellGameplayScript : MonoBehaviour {

    public GameObject subtitles, leftP, rightP;
    public List<Transform> places = new List<Transform>();

    float moveSpeed = .004f;
    private float vComparer = .01f;
    private bool moved = true;

    IEnumerator MoveTo(Transform t)
    {
        while (!V3Equal(transform.position, t.position))
        {
            transform.position = Vector3.MoveTowards(transform.position, t.position, moveSpeed);
            yield return new WaitForFixedUpdate();
        }
        moved = true;
    }

    private bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < vComparer;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        switch ((int)(subtitles.GetComponent<SubstitlesScript>().theTimer + .5f))
        {
            case 191:
                transform.position = places[0].transform.position;
                break;
            case 194:
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
}
