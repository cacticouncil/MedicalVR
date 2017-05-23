using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeInputObject : MonoBehaviour
{
    public GameObject memoryui;
    public Randomsphere sphere;

    public bool move;
    public Vector3 endMarker;
    public float speed = 0.1F;

    // Use this for initialization
    void Start()
    {
        move = false;
        endMarker = transform.position;
    }

    void FixedUpdate()
    {
        if (move)
        {
            float step = speed * Time.fixedDeltaTime * 10;
            transform.position = Vector3.MoveTowards(transform.position, endMarker, step);
            if (transform.position == endMarker)
            {
                move = false;
            }
        }
    }

    public void CASES()
    {
        bool correct = false;
        switch (gameObject.tag)
        {
            case "C":
                {
                    if (sphere.que.Contains("G"))
                    {
                        int index = sphere.que.IndexOf("G") - 1;
                        endMarker = new Vector3(15 * index, -20, 30);
                        correct = true;
                    }
                    break;
                }
            case "G":
                if (sphere.que.Contains("C"))
                {
                    int index = sphere.que.IndexOf("C") - 1;
                    endMarker = new Vector3(15 * index, -20, 30);
                    correct = true;
                }
                break;
            case "U":
                if (sphere.que.Contains("A"))
                {
                    int index = sphere.que.IndexOf("A") - 1;
                    endMarker = new Vector3(15 * index, -20, 30);
                    correct = true;
                }
                break;
            case "A":
                if (sphere.que.Contains("T"))
                {
                    if (!(sphere.Genes == GNE.TCT || sphere.Genes == GNE.TCT2))
                    {
                        if (!sphere.aPlayed)
                        {
                            int index = sphere.que.IndexOf("T") - 1;
                            endMarker = new Vector3(15 * index, -20, 30);
                            correct = true;
                            sphere.aPlayed = true;
                        }
                    }
                    //First T in TCT
                    else if (!sphere.aPlayed)
                    {
                        endMarker = new Vector3(-15, -20, 30);
                        correct = true;
                        sphere.aPlayed = true;
                    }
                    //Second T in TCT
                    else
                    {
                        endMarker = new Vector3(15, -20, 30);
                        correct = true;
                    }
                }
                break;
            default:
                break;
        }

        if (correct)
        {
            if (SoundManager.IsJordanPlaying("180278__rodny5__surreal-bell") == false)
                SoundManager.PlayJordanVoice("180278__rodny5__surreal-bell");
            memoryui.GetComponent<MemoryUI>().score += 50;
            GetComponent<Renderer>().material.color = Color.green;
            sphere.Correct();
            move = true;
        }
        else
        {
            if (SoundManager.IsJordanPlaying("341633__padsterpat__karplus-strong") == false)
                SoundManager.PlayJordanVoice("341633__padsterpat__karplus-strong");
            GetComponent<Renderer>().material.color = Color.red;
            memoryui.GetComponent<MemoryUI>().LoseresetPos();
        }

        GetComponent<Collider>().enabled = false;
    }

    public void ClickedInput()
    {
        CASES();
    }
}
