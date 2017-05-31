using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RNA_CellGameplayScript : MonoBehaviour
{
    public Transform target, cam;
    public GameObject subtitltes;
    private float moveSpeed = .012f;

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
            case (213):
                if (moved)
                {
                    transform.localScale = new Vector3(transform.localScale.x / 5, transform.localScale.y / 5, transform.localScale.z / 5);
                    transform.position = new Vector3(cam.position.x + 2, cam.position.y + 1, cam.position.z + 2);
                    moved = false;
                    StartCoroutine(MoveTo(target));
                }
                break;
            default:
                break;
        }
    }
}
