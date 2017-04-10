using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Virus_CellGameplayScript : MonoBehaviour {

    public List<GameObject> places;
    public GameObject subtitltes, virusWithMesh, capsid, dna;

    delegate void Func();
    Func doAction;
    public float moveSpeed;
    float fadeSpeed;
    int I = 0;
    void Start()
    {
        doAction = NullFunction;
        switch (VirusGameplayScript.loadCase)
        {
            case (1):
                I = 2;
                transform.position = places[I].transform.position;
                break;
            default:
                break;
        }
    }
    void NullFunction()
    {

    }
    // Update is called once per frame
    void Update()
    {
        CheckCaases();
        doAction();
        MoveTo();
    }
    void MoveTo()
    {
        if (I != places.Count)
            if (I == 10)
            {
                if (dna.transform.rotation == places[I].transform.rotation)
                    dna.transform.position = Vector3.MoveTowards(dna.transform.position, places[I].transform.position, moveSpeed * Time.deltaTime);
            }
            else
                transform.position = Vector3.MoveTowards(transform.position, places[I].transform.position, moveSpeed * Time.deltaTime);

    }
    void fadeCapsid()
    {
        float a = capsid.GetComponent<Renderer>().material.color.a;
        capsid.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a - Time.deltaTime);
    }
    void RotateTo()
    {
        if (I != places.Count)
            dna.transform.rotation = Quaternion.RotateTowards(dna.transform.rotation, places[I].transform.rotation, 100 * Time.deltaTime);
    }
    void CheckCaases()
    {
        switch ((int)subtitltes.GetComponent<SubstitlesScript>().theTimer)
        {
            case (17):
                I = 1;
                transform.position = places[I].transform.position;
                moveSpeed = 50;
                break;
            case (20):
                I = 2;
                moveSpeed = 50;
                break;
            case (26):
                I = 3;
                moveSpeed = 50;
                break;
            case (29):
                I = 4;
                transform.position = places[I].transform.position;
                virusWithMesh.SetActive(false);
                break;
            case (192):
                doAction = fadeCapsid;
                break;
            case (195):
                capsid.SetActive(false);
                doAction = NullFunction;
                I = 6;
                moveSpeed = 50;
                break;
            case (227):
                doAction = NullFunction;
                I = 7;
                moveSpeed = 250;
                break;
            case (249):
                doAction = NullFunction;
                I = 8;
                transform.position = places[I].transform.position;
                break;
            case (252):
                doAction = NullFunction;
                I = 9;
                moveSpeed = 50;
                break;
            case (256):
                doAction = RotateTo;
                I = 10;
                moveSpeed = 50;
                break;
            default:
                break;
        }
    }
}
