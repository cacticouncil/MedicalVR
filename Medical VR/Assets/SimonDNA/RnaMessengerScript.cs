using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RnaMessengerScript : MonoBehaviour {

    public GameObject Refpolynuclei, simon;
    List<GameObject> chain = new List<GameObject>();
    List<GameObject> wrongChain = new List<GameObject>();
    List<Vector3> prevPos = new List<Vector3>();
    int wave = 0, tmpScore, tmpLives;
    Vector3 target;
    bool changeMove = false;
    // Use this for initialization
    void Start ()
    {
        tmpScore = simon.GetComponent<SimonSays>().score;
        tmpLives = simon.GetComponent<SimonSays>().lives;
        changeMove = false;
    }
	// Update is called once per frame
	void Update ()
    {

        if (tmpLives != simon.GetComponent<SimonSays>().lives)
        {
            tmpLives = simon.GetComponent<SimonSays>().lives;
            if (wave > 0)
            {
                RemoveNuclei();
                changeMove = true;
            }
            simon.GetComponent<SimonSays>().makeNuclei = false;
        }
       else if (simon.GetComponent<SimonSays>().makeNuclei == true)
        {
            simon.GetComponent<SimonSays>().makeNuclei = false;
           AddNuclei();
            changeMove = false;
        }
        BreakAnimation();
        if(changeMove == false)
        {
            MoveNuclei();
        }
        else
        {
            MoveBack();
        }
        if (tmpScore != simon.GetComponent<SimonSays>().score)
        {
            tmpScore = simon.GetComponent<SimonSays>().score;
            wave = 0;
        }
    }
    void AddNuclei()
    {
        wave++;
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
    void RemoveNuclei()
    {
        for (int i = chain.Count -1; i > chain.Count - 1 - wave; i--)
        {
            wrongChain.Add(chain[i]);
            
        }
        
        for (int i = 0; i < wrongChain.Count; i++)
        {
            chain.Remove(wrongChain[i]);
            prevPos.Remove(wrongChain[i].transform.position);
           // wrongChain[i].GetComponent<Animator>().enabled = true;
            //wrongChain[i].GetComponent<Animator>().SetBool("DestroyNucle", true);
        }
        target = new Vector3(chain[chain.Count - 1].transform.position.x + 1, chain[chain.Count - 1].transform.position.y, chain[chain.Count - 1].transform.position.z);
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
    void MoveBack()
    {
        if (chain.Count > 0 && prevPos.Count > 0)
        {
            for (int i = 0; i < chain.Count; i++)
            {
                if(i == chain.Count -1)
                  chain[i].transform.position = Vector3.MoveTowards(chain[i].transform.position, target, Time.deltaTime);
                else
                {
                    chain[i].transform.position = Vector3.MoveTowards(chain[i].transform.position, chain[i+1].transform.position, Time.deltaTime);
                }
            }
        }
    }
    void BreakAnimation()
    {
        if(chain.Count > 1)
        {
            chain[chain.Count - 2].GetComponent<Animator>().enabled = false;  
        }
        if (wrongChain.Count > 0)
        {
            for (int i = 0; i < wrongChain.Count; i++)
            {
                Destroy(wrongChain[i]);
            }
        }
        wrongChain.Clear();
    }
}
