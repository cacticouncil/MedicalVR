using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeInputObject : MonoBehaviour, TimedInputHandler {

    public GameObject memoryui;
    public GameObject sphere;
    private string Clonei;
    private string Clonej;
    private string Clonek;

    private Vector3 pos;
    public bool move;
    public bool IsCorrect;
    private bool IsCorrecti;
    private bool IsCorrectj;
    private bool IsCorrectk;


    public Transform endMarker;
    public float speed = 0.1F;
    // Use this for initialization
    void Start () {
        move = false;
        pos = transform.position;
        endMarker.position = transform.position;
        IsCorrect = false;
        IsCorrecti = false;
        IsCorrectj = false;
        IsCorrectk = false;
}


     void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime * 10;
        if (move == true)
        {
           transform.position = Vector3.MoveTowards(transform.position, endMarker.position, step);
            //transform.position += endMarker.position * speed;
            //Debug.Log("position: " + transform.position);
            if(transform.position == endMarker.position)
            move = false;
        }
    }
    public void TCTCASE()
    {
        if ((Clonei == "A" && gameObject.tag == "U") || (Clonei == "G" && gameObject.tag == "C") || (Clonei == "C" && gameObject.tag == "G") || (Clonei == "T" && gameObject.tag == "A"))
        {

            if (sphere.GetComponent<Randomsphere>().ans.Contains("A"))
            {
                endMarker.position = new Vector3(15, -20, 30);
                move = true;
            }             
            else
            {
                endMarker.position = new Vector3(-15, -20, 30);
                move = true;
                sphere.GetComponent<Randomsphere>().ans.Add(gameObject.tag);
            }


            memoryui.GetComponent<MemoryUI>().score += 50;
            GetComponent<Renderer>().material.color = Color.green;
            IsCorrecti = true;
        }
        else
            IsCorrecti = false;

        if ((Clonej == "A" && gameObject.tag == "U") || (Clonej == "G" && gameObject.tag == "C") || (Clonej == "C" && gameObject.tag == "G"))
        {
            endMarker.position = new Vector3(0, -20, 30);
            move = true;
            memoryui.GetComponent<MemoryUI>().score += 50;
            GetComponent<Renderer>().material.color = Color.green;
            IsCorrectj = true;
        }
        else
            IsCorrectj = false;

        if ((Clonek == "C" && gameObject.tag == "G") || (Clonek == "T2" && gameObject.tag == "A2" ) /*|| (Clonek == "T2" && gameObject.tag == "A")*/)
        {
            if (sphere.GetComponent<Randomsphere>().ans.Contains("A2"))
            {
                endMarker.position = new Vector3(-15, -20, 30);
                move = true;
            }
            else
            {
                endMarker.position = new Vector3(15, -20, 30);
                move = true;
                sphere.GetComponent<Randomsphere>().ans.Add(gameObject.tag);
            }

            memoryui.GetComponent<MemoryUI>().score += 50;
            GetComponent<Renderer>().material.color = Color.green;
            IsCorrectk = true;
        }
        else
            IsCorrectk = false;
    }


    public void OTHERCASES()
    {
        if (!(sphere.GetComponent<Randomsphere>().ans.Contains("A")) && !(sphere.GetComponent<Randomsphere>().ans.Contains("A2")))
        {
            if ((Clonei == "A" && gameObject.tag == "U") || (Clonei == "G" && gameObject.tag == "C") || (Clonei == "C" && gameObject.tag == "G") || (Clonei == "T" && (gameObject.tag == "A2" || gameObject.tag == "A")))
            { 
                endMarker.position = new Vector3(-15, -20, 30);
                move = true;
                sphere.GetComponent<Randomsphere>().ans.Add(gameObject.tag);
                memoryui.GetComponent<MemoryUI>().score += 50;
                GetComponent<Renderer>().material.color = Color.green;
                IsCorrecti = true;
            }
                
        }
        else if((Clonei == "A" && gameObject.tag == "U") || (Clonei == "G" && gameObject.tag == "C") || (Clonei == "C" && gameObject.tag == "G"))
        {
            endMarker.position = new Vector3(-15, -20, 30);
            move = true;
            memoryui.GetComponent<MemoryUI>().score += 50;
            GetComponent<Renderer>().material.color = Color.green;
            IsCorrecti = true;
        }
        else
            IsCorrecti = false;

        if ((Clonej == "A" && gameObject.tag == "U") || (Clonej == "G" && gameObject.tag == "C") || (Clonej == "C" && gameObject.tag == "G"))
        {
            endMarker.position = new Vector3(0, -20, 30);
            move = true;
            memoryui.GetComponent<MemoryUI>().score += 50;
            GetComponent<Renderer>().material.color = Color.green;
            IsCorrectj = true;
        }
        else
            IsCorrectj = false;

        if (!(sphere.GetComponent<Randomsphere>().ans.Contains("A")) && !(sphere.GetComponent<Randomsphere>().ans.Contains("A2")))
        {
            if ((Clonek == "C" && gameObject.tag == "G") || (Clonek == "T2" && gameObject.tag == "A2") || (Clonek == "T2" && gameObject.tag == "A"))
            {
                endMarker.position = new Vector3(15, -20, 30);
                move = true;

                sphere.GetComponent<Randomsphere>().ans.Add(gameObject.tag);
                memoryui.GetComponent<MemoryUI>().score += 50;
                GetComponent<Renderer>().material.color = Color.green;
                IsCorrectk = true;
            }
        }
        else if((Clonek == "C" && gameObject.tag == "G"))
        {
            endMarker.position = new Vector3(15, -20, 30);
            move = true;


            memoryui.GetComponent<MemoryUI>().score += 50;
            GetComponent<Renderer>().material.color = Color.green;
            IsCorrectk = true;
        }
        else
            IsCorrectk = false;
    }

    public void HandleTimeInput()
    {
        Clonei = sphere.GetComponent<Randomsphere>().Getclonei().tag;
        Clonej = sphere.GetComponent<Randomsphere>().Getclonej().tag;
        Clonek = sphere.GetComponent<Randomsphere>().Getclonek().tag;

        switch (sphere.GetComponent<Randomsphere>().Genes)
        {
            case GNE.TCT:
                {
                    TCTCASE();
                }
                break;
            case GNE.TCT2:
                {
                    TCTCASE();
                }
                break;
            default:
                {
                    OTHERCASES();
                }
                break;
        }

        

        if (IsCorrecti == true || IsCorrectj == true || IsCorrectk == true)
            IsCorrect = true;
        else
            IsCorrect = false;

        if (IsCorrect)
            memoryui.GetComponent<MemoryUI>().score += 50;
        else
        {
            memoryui.GetComponent<MemoryUI>().score -= 0;
            GetComponent<Renderer>().material.color = Color.red;
            memoryui.GetComponent<MemoryUI>().LoseresetPos();
            transform.position = pos;
        }


     
    }
}
