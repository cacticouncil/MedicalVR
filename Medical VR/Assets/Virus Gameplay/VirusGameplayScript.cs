using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class VirusGameplayScript : MonoBehaviour {

    public List<GameObject> places;
    public GameObject subtitltes, blackCurtain, theCamera, redCell, virus;
    public static int loadCase;
    // Use this for initialization
    delegate void Func();
    Func doAction;
    float moveSpeed = 1, fadeSpeed;
    int I = 0;
	void Start ()
    {
        doAction = NullFunction;
        redCell.SetActive(false);
        virus.SetActive(false);
        switch (loadCase)
        {
            case (1):
                I = 1;
                subtitltes.GetComponent<SubstitlesScript>().theTimer = 22;
                transform.position = places[I].transform.position;
                redCell.SetActive(true);
                virus.SetActive(true);
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case (2):
                I = 1;
                subtitltes.GetComponent<SubstitlesScript>().theTimer = 29;
                transform.position = places[I].transform.position;
                redCell.SetActive(true);
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
            case (7):
                doAction = NullFunction;
                if (redCell.activeSelf == false)
                {
                    redCell.SetActive(true);
                    subtitltes.GetComponent<SubstitlesScript>().Stop();
                }
                break;
            case (10):
                doAction = NullFunction;
                if (virus.activeSelf == false)
                {
                    virus.SetActive(true);
                    redCell.GetComponent<SphereCollider>().enabled = false;
                    subtitltes.GetComponent<SubstitlesScript>().Stop();
                }
                break;
            case (15):
                doAction = NullFunction;
                I = 1;
                moveSpeed = 30;
                break;
            case (20):
                MovingCamera.arcadeMode = false;
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (21):
                SceneManager.LoadScene("DodgeAnitbodies");
                break;
            case (27):
                SimonSays.arcadeMode = false;
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (28):
                SceneManager.LoadScene("SimonDNA");
                break;
            default:
                break;
        }
    }
}
