using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RnaMessengerScript : MonoBehaviour {

    public GameObject Refpolynuclei, simon;
    List<GameObject> chain = new List<GameObject>();
    List<Vector3> prevPos = new List<Vector3>();
    int tmpScore = 0;
    Vector3 target;
    // Use this for initialization
    void Start ()
    {
       tmpScore = simon.GetComponent<SimonSays>().score;
    }
	// Update is called once per frame
	void Update ()
    {
	    if(tmpScore!= simon.GetComponent<SimonSays>().score)
        {
           tmpScore = simon.GetComponent<SimonSays>().score;
           AddNuclei();
        }
        BreakAnimation();
        MoveNuclei();
    }
    void AddNuclei()
    {
        GameObject nuclei = Instantiate(Refpolynuclei, Refpolynuclei.transform.position, Refpolynuclei.transform.rotation) as GameObject;
        switch (simon.GetComponent<SimonSays>().selectedColor)
        {
            case SimonSays.theColors.YELLOW:
                nuclei.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case SimonSays.theColors.RED:
                nuclei.GetComponent<Renderer>().material.color = Color.red;
                break;
            case SimonSays.theColors.BLUE:
                nuclei.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case SimonSays.theColors.GREEN:
                nuclei.GetComponent<Renderer>().material.color = Color.green;
                break;
            default:
                break;
        }
        chain.Add(nuclei);
        prevPos.Clear();
        target = new Vector3(chain[0].transform.position.x - 1, chain[0].transform.position.y, chain[0].transform.position.z);
        for (int i = 0; i < chain.Count; i++)
        {
            prevPos.Add(chain[i].transform.position);
        }
    }
    void MoveNuclei()
    {
        if (chain.Count > 0 && prevPos.Count > 0)
        {
            for (int i = 0; i < chain.Count; i++)
            {
                if(i == 0)
                  chain[i].transform.position = Vector3.MoveTowards(chain[i].transform.position, target, Time.deltaTime);
                else
                {
                    chain[i].transform.position = Vector3.MoveTowards(chain[i].transform.position, prevPos[i-1], Time.deltaTime);
                }
            }
        }
    }
    void BreakAnimation()
    {
        if(chain.Count > 1)
        {
            Destroy(chain[chain.Count - 2].GetComponent<Animator>());
        }
        
    }
}
