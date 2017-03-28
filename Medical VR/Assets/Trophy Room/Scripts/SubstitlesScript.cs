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
    int i = 0;
    bool stop = false;
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
                    if (i >= theSubtitles.Count)
                    {
                        stop = true;
                    }
                    GetComponent<TMPro.TextMeshPro>().text = "";
                }
                else
                {
                    GetComponent<TMPro.TextMeshPro>().text = theSubtitles[i].text;
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
}
