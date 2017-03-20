using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RnaMessengerScript : MonoBehaviour {

    public GameObject Refpolynuclei, simon;
    Stack<GameObject> chain = new Stack<GameObject>();
    Stack<GameObject> wrongChain = new Stack<GameObject>();
    List<Vector3> prevPos = new List<Vector3>();
    int wrongs = 0, tmpScore, tmpLives;
    Vector3 target;

    // Use this for initialization
    void Start ()
    {
        tmpScore = simon.GetComponent<SimonSays>().score;
        tmpLives = simon.GetComponent<SimonSays>().lives;
       
    }
	// Update is called once per frame
	void Update ()
    {
        BreakAnimation();
        if (tmpLives != simon.GetComponent<SimonSays>().lives)
        {
            tmpLives = simon.GetComponent<SimonSays>().lives;
            if (wrongs > 0)
            {
                RemoveNuclei();
               
            }
            simon.GetComponent<SimonSays>().makeNuclei = false;
            wrongs = 0;
        }
       else if (simon.GetComponent<SimonSays>().makeNuclei == true)
        {
            simon.GetComponent<SimonSays>().makeNuclei = false;
            AddNuclei();
            
        }
        
        MoveNuclei();
        
       
        if (tmpScore != simon.GetComponent<SimonSays>().score)
        {
            tmpScore = simon.GetComponent<SimonSays>().score;
            wrongs = 0;
        }
    }
    void AddNuclei()
    {
        wrongs++;
        GameObject nuclei = Instantiate(Refpolynuclei, Refpolynuclei.transform.position, Refpolynuclei.transform.rotation) as GameObject;
        switch (simon.GetComponent<SimonSays>().selectedColor)
        {
            case SimonSays.theColors.YELLOW:
                nuclei.GetComponent<Renderer>().material.color = Color.yellow;
                nuclei.GetComponentInChildren<TextMesh>().text = "C";
                break;
            case SimonSays.theColors.RED:
                nuclei.GetComponent<Renderer>().material.color = Color.red;
                nuclei.GetComponentInChildren<TextMesh>().text = "A";
                break;
            case SimonSays.theColors.BLUE:
                nuclei.GetComponent<Renderer>().material.color = Color.blue;
                nuclei.GetComponentInChildren<TextMesh>().text = "U";
                break;
            case SimonSays.theColors.GREEN:
                nuclei.GetComponent<Renderer>().material.color = Color.green;
                nuclei.GetComponentInChildren<TextMesh>().text = "G";
                break;
            default:
                break;
        }
        chain.Push(nuclei);
       
        target = new Vector3((float)(4.2 + prevPos.Count*-1), -1.49f, 3.733f);
        if (prevPos.Count < chain.Count)
            prevPos.Add(target);
    }
    void RemoveNuclei()
    {
        for (int i = 0; i < wrongs; i++)
        {
            wrongChain.Push(chain.Pop());   
        }

        foreach (GameObject i in wrongChain)
        {
           i.GetComponent<Animator>().enabled = true;
           i.GetComponent<Animator>().SetBool("DestroyNucle", true);
        }
    }
    void MoveNuclei()
    {
        if (chain.Count > 0 && prevPos.Count > 0)
        {
            int idx = 0;
            foreach (GameObject i in chain)
            {
                i.transform.position = Vector3.MoveTowards(i.transform.position, prevPos[idx], Time.deltaTime);
                idx++;
            }
            
               
        }
    }
    
    void BreakAnimation()
    {
        foreach (GameObject i in chain)
        {
            if (!i.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Polynuclei"))
            {
                i.GetComponent<Animator>().enabled= false;
            }
           
        }
        foreach (GameObject i in wrongChain)
        {
            if (!i.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DestroyNuclei"))
            {
                Destroy(i);
            }

        }
        wrongChain.Clear();
    }
    public void ResetRNA()
    {
        foreach (GameObject i in chain)
        {
            Destroy(i);
        }
        chain.Clear();
        foreach(GameObject i in wrongChain)
        {
            Destroy(i);
        }
        wrongChain.Clear();
        prevPos.Clear();
        tmpScore = simon.GetComponent<SimonSays>().score;
        tmpLives = simon.GetComponent<SimonSays>().lives;
        
    }
}
