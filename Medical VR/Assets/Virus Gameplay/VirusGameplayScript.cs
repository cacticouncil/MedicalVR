using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class VirusGameplayScript : MonoBehaviour {

    public List<GameObject> places;
    public GameObject subtitltes, blackCurtain, theCamera, redCell;
    // Use this for initialization
    delegate void Func();
    Func doAction;
    float speed = 30;
    int I = 0;
	void Start ()
    {
        doAction = NullFunction;
        redCell.SetActive(false);
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
        blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0,a- (Time.deltaTime*0.5f));
        
    }
    void LowerCurtain()
    {
        float a = blackCurtain.GetComponent<Renderer>().material.color.a;
        blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a + (Time.deltaTime * 1.5f));
    }
    void MoveTo()
    {
        if(I != places.Count)
        theCamera.transform.position = Vector3.MoveTowards(theCamera.transform.position, places[I].transform.position, speed *Time.deltaTime);
    }
    void CheckCaases()
    {
        switch ((int)subtitltes.GetComponent<SubstitlesScript>().theTimer)
        {
            case (4):
                doAction = RiseCurtain;
                break;
            case (7):
                doAction = NullFunction;
                I = 1;
                break;
            case (10):
                doAction = NullFunction;
                if(redCell.activeSelf == false)
                {
                    redCell.SetActive(true);
                    subtitltes.GetComponent<SubstitlesScript>().Stop();
                }  
                break;
            case (14):
                MovingCamera.arcadeMode = false;
                doAction = LowerCurtain;
                
                break;
            case (15):
                SceneManager.LoadScene("DodgeAnitbodies");
                break;
            default:
                break;
        }
    }
}
