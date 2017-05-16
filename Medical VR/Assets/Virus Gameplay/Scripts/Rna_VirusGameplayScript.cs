using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rna_VirusGameplayScript : MonoBehaviour
{
    public List<Transform> places = new List<Transform>();
    public GameObject subtitltes;

    private float moveSpeed = .006f;
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

    void Start()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCaases();
    }

    void CheckCaases()
    {
        switch ((int)subtitltes.GetComponent<SubstitlesScript>().theTimer)
        {
            case (296):
                transform.position = places[0].transform.position;
                break;
            case (297):
                if (moved)
                {
                    moved = false;
                    StartCoroutine(MoveTo(places[1].transform));
                }
                break;
            default:
                break;
        }
    }
}

