using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RNA_CellGameplayScript : MonoBehaviour {

    public List<GameObject> places;
    public GameObject subtitltes;

    delegate void Func();
    Func doAction;
    public float moveSpeed;
    int I = 0;
    void Start()
    {
        gameObject.SetActive(false);
        doAction = NullFunction;
        switch (VirusGameplayScript.loadCase)
        {

            default:
                break;
        }
    }
    void NullFunction()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCaases();
        doAction();
        MoveTo();
    }
    void MoveTo()
    {
        if (I != places.Count)
            transform.position = Vector3.MoveTowards(transform.position, places[I].transform.position, moveSpeed * Time.deltaTime);
    }

    void CheckCaases()
    {
        switch ((int)subtitltes.GetComponent<SubstitlesScript>().theTimer)
        {          
            case (212):
                I = 1;
                transform.position = places[I].transform.position;
                break;
            case (213):
                I = 2;
                moveSpeed = 30;
                break;
            default:
                break;
        }
    }
}
