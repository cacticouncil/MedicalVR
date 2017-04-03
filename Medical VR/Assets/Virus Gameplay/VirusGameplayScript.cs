using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class VirusGameplayScript : MonoBehaviour {

    public List<GameObject> places;
    public GameObject subtitltes, blackCurtain, theCamera, virus;
    public static int loadCase;
    // Use this for initialization
    delegate void Func();
    Func doAction;
    public float moveSpeed;
    float fadeSpeed;
    int I = 0;
	void Start ()
    {
        doAction = NullFunction;
        virus.SetActive(false);
        switch (loadCase)
        {
            case (1):
                I = 1;
                subtitltes.GetComponent<SubstitlesScript>().theTimer = 65.5f;
                theCamera.transform.position = places[I].transform.position;
                virus.SetActive(true);
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case (2):
                I = 2;
                subtitltes.GetComponent<SubstitlesScript>().theTimer = 29;
                theCamera.transform.position = places[I].transform.position;
                
                virus.SetActive(true);
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            default:
                break;
        }
    }
	void NullFunction()
    {

    }
	// Update is called once per frame
	void Update ()
    {
        CheckCaases();
        doAction();
        MoveTo();
    }
    void RiseCurtain()
    {
        float a = blackCurtain.GetComponent<Renderer>().material.color.a;
        if (a > 255)
            a = 255;
        blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0,a- (Time.deltaTime*fadeSpeed));
        
    }
    void LowerCurtain()
    {
        float a = blackCurtain.GetComponent<Renderer>().material.color.a;
        if (a < 0)
            a = 0;
        blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a + (Time.deltaTime * fadeSpeed));
    }
    void MoveTo()
    {
        if(I != places.Count)
        theCamera.transform.position = Vector3.MoveTowards(theCamera.transform.position, places[I].transform.position, moveSpeed *Time.deltaTime);
    }
    void CheckCaases()
    {
        switch ((int)subtitltes.GetComponent<SubstitlesScript>().theTimer)
        {
            case (4):
                doAction = RiseCurtain;
                fadeSpeed = 0.5f;
                break;
            case (20):
                doAction = NullFunction;
                subtitltes.GetComponent<SubstitlesScript>().Stop();
                break;
            case (35):
                doAction = NullFunction;
                if (virus.activeSelf == false)
                {
                    virus.SetActive(true);
                    
                    subtitltes.GetComponent<SubstitlesScript>().Stop();
                }
                break;
            case (55):
                doAction = NullFunction;
                I = 1;
                //moveSpeed = 30;
                break;
            case (64):
                MovingCamera.arcadeMode = false;
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (65):
                if(subtitltes.GetComponent<SubstitlesScript>().theTimer < 65.5f)
                SceneManager.LoadScene("DodgeAnitbodies");
                break;
            case (94):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (95):
                I = 2;
                theCamera.transform.position = places[I].transform.position;
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case (159):
                SimonSays.arcadeMode = false;
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (160):
                SceneManager.LoadScene("SimonDNA");
                break;
            default:
                break;
        }
    }
}
