using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubstitlesScript : MonoBehaviour {

    [System.Serializable]
    public class Subtitle
    {
        public string text;
        public float start, end;
    }
    // Use this for initialization
    public List<Subtitle> theSubtitles = new List<Subtitle>();
    public float theTimer = 0;
    public float textSpeed;
    float textTimer =0;
    string theText = "";
    int textIdx = 0;
    int i = 0;
    bool stop = false, done = false;
	void Start ()
    {
        if (theSubtitles.Count == 0)
            stop = true;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(stop == false)
        {
            theTimer += Time.deltaTime;
            if (theTimer >= theSubtitles[i].start)
            {
                if(theTimer >= theSubtitles[i].end)
                {
                    i++;
                    textTimer = 0;
                    textIdx = 0;
                    theText = "";
                    if (i >= theSubtitles.Count)
                    {
                        stop = true;
                        done = true;                        
                    }
                    GetComponent<TMPro.TextMeshPro>().text = "";
                }
                else
                {
                    textTimer += Time.deltaTime;
                    if(textTimer > textSpeed)
                    {
                        textTimer = 0;                                
                        theText = theText + theSubtitles[i].text[textIdx];
                        if (textIdx < theSubtitles[i].text.Length -1)
                            textIdx++;

                        else
                            textTimer = -1000;

                        GetComponent<TMPro.TextMeshPro>().text = theText;
                    }
                    
                }
            }
            else
                GetComponent<TMPro.TextMeshPro>().text = "";
        }   
	}

    public void Replay()
    {
        if(stop == true)
        {
            stop = false;
            done = false;
            i = 0;
            theTimer = 0;
        }
    }
    public void Stop()
    {
        stop = true;
        GetComponent<TMPro.TextMeshPro>().text = "";
    }
    public void Continue()
    {
        stop = false;
    }
    public bool IsDone()
    {
        return done;
    }
}
