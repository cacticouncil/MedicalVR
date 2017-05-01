using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RnaMessengerScript : MonoBehaviour
{

    public GameObject Refpolynuclei, simon, subtitles, blackCurtain;
    Stack<GameObject> chain = new Stack<GameObject>();
    Stack<GameObject> wrongChain = new Stack<GameObject>();
    List<Vector3> prevPos = new List<Vector3>();
    int wrongs = 0, tmpScore, tmpLives;
    Vector3 target;
    float timer = 0, timer2 = 0;
    // Use this for initialization
    void Start()
    {
        tmpScore = simon.GetComponent<SimonSays>().score;
        tmpLives = simon.GetComponent<SimonSays>().lives;
        if (GlobalVariables.arcadeMode == false)
        {
            subtitles.SetActive(false);
            subtitles.GetComponent<SubstitlesScript>().Stop();
            simon.GetComponent<SimonSays>().lives = 0;
            simon.GetComponent<SimonSays>().sign.SetActive(false);
            if (GlobalVariables.arcadeMode == false && GlobalVariables.tutorial == false)
            {
                simon.GetComponent<SimonSays>().lives = 3;
                simon.GetComponent<SimonSays>().sign.SetActive(true);
                simon.GetComponent<SimonSays>().theLives.GetComponent<TMPro.TextMeshPro>().text = "Lives: " + 3;
            }
        }
        else
        {
            blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);

        }
    }
    // Update is called once per frame
    void Update()
    {
        StoryModeStuff();
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
    void StoryModeStuff()
    {
        if (GlobalVariables.arcadeMode == false && simon.GetComponent<SimonSays>().polys != simon.GetComponent<SimonSays>().polysDone)
        {
            timer += Time.deltaTime;
            if (timer > 1 && timer < 2)
            {
                subtitles.GetComponent<SubstitlesScript>().Continue();
            }
            float a = blackCurtain.GetComponent<Renderer>().material.color.a;
            if (a > 255)
                a = 255;
            blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a - (Time.deltaTime * 1.5f));
        }
        else if (simon.GetComponent<SimonSays>().polys == simon.GetComponent<SimonSays>().polysDone)
        {
            timer2 += Time.deltaTime;
            if (timer2 > 2.5f)
            {
                //If you win story mode
                VirusGameplayScript.loadCase = 2;
                SceneManager.LoadScene("VirusGameplay");
            }
            if (timer2 > 1.5f)
            {
                simon.GetComponent<SimonSays>().sign.SetActive(false);
                float a = blackCurtain.GetComponent<Renderer>().material.color.a;
                if (a < 0)
                    a = 0;
                blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a + (Time.deltaTime * 1.5f));
            }

        }
       
      
    }
    public void AddNuclei()
    {
        wrongs++;
        GameObject nuclei = Instantiate(Refpolynuclei, Refpolynuclei.transform.position, Refpolynuclei.transform.rotation) as GameObject;
        nuclei.SetActive(true);
        switch (simon.GetComponent<SimonSays>().selectedColor)
        {
            case SimonSays.theColors.YELLOW:
                nuclei.GetComponent<Renderer>().material.color = Color.yellow;
                nuclei.GetComponentInChildren<TMPro.TextMeshPro>().text = "C";
                break;
            case SimonSays.theColors.RED:
                nuclei.GetComponent<Renderer>().material.color = Color.red;
                nuclei.GetComponentInChildren<TMPro.TextMeshPro>().text = "A";
                break;
            case SimonSays.theColors.BLUE:
                nuclei.GetComponent<Renderer>().material.color = Color.blue;
                nuclei.GetComponentInChildren<TMPro.TextMeshPro>().text = "U";
                break;
            case SimonSays.theColors.GREEN:
                nuclei.GetComponent<Renderer>().material.color = Color.green;
                nuclei.GetComponentInChildren<TMPro.TextMeshPro>().text = "G";
                break;
            default:
                break;
        }
        chain.Push(nuclei);

        target = new Vector3((float)(4.2 + prevPos.Count * -1), 36f, -53f);
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
                i.GetComponent<Animator>().enabled = false;
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
        foreach (GameObject i in wrongChain)
        {
            Destroy(i);
        }
        wrongChain.Clear();
        prevPos.Clear();
        tmpScore = simon.GetComponent<SimonSays>().score;
        tmpLives = simon.GetComponent<SimonSays>().lives;

    }
}
