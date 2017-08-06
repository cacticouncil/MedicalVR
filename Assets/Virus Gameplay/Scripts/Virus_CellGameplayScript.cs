using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Virus_CellGameplayScript : MonoBehaviour
{
    public List<Transform> places = new List<Transform>();
    public GameObject subtitles, virusWithMesh, capsid, dna;

    int I = 0;
    private float moveSpeed = .01f;
    private bool fade = true;

    void Start()
    {
        switch (CellGameplayScript.loadCase)
        {
            case (1):
                I = 4;
                virusWithMesh.SetActive(false);
                capsid.SetActive(false);
                transform.position = places[I].transform.position;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCaases();
        MoveTo();
    }

    void MoveTo()
    {
        if (I != places.Count)
        {
            if (I == 10)
            {
                if (dna.transform.rotation == places[I].rotation)
                    dna.transform.position = Vector3.MoveTowards(dna.transform.position, places[I].position, moveSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, places[I].position, moveSpeed);
            }
        }
    }

    IEnumerator FadeOutObject(Renderer g)
    {
        if (g.material.HasProperty("_Color"))
        {
            Color c = g.material.color;
            float startTime = Time.time;
            float t = 0.0f;
            while (c.a > 0)
            {
                t = Time.time - startTime;
                c.a = 1.0f - t;
                g.material.color = c;
                g.material.SetColor("_OutlineColor", new Color(0, 0, 0, c.a));
                yield return 0;
            }
            c.a = 0.0f;
            g.material.color = c;
            g.enabled = false;
        }
        fade = true;
    }

    void CheckCaases()
    {
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case (24):
                I = 0;
                transform.position = places[I].position;
                break;
            case (25):
                I = 1;
                break;
            case (26):
                I = 2;
                break;
            case (31):
                I = 3;
                transform.position = places[I].position;
                break;
            case 32:
                if (fade)
                {
                    fade = false;
                    StartCoroutine(FadeOutObject(GetComponent<Renderer>()));
                }
                break;
            case (34):
                if (fade)
                {
                    fade = false;
                    StartCoroutine(FadeOutObject(capsid.GetComponent<Renderer>()));
                }
                break;
            case (35):
                capsid.SetActive(false);
                I = 4;
                break;
            default:
                break;
        }
    }
}
