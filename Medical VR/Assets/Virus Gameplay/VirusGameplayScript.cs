using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class VirusGameplayScript : MonoBehaviour {

    public List<GameObject> places;
    public List<GameObject> Sceneries;
    public GameObject subtitltes, blackCurtain, theCamera, virus;
    public static int loadCase =1;
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
        Sceneries[1].SetActive(false);
        Sceneries[2].SetActive(false);
        switch (loadCase)
        {
            case (1):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(true);
                Sceneries[2].SetActive(false);
                I = 2;
                subtitltes.GetComponent<SubstitlesScript>().theTimer = 120.5f;
                theCamera.transform.position = places[I].transform.position;
                virus.SetActive(true);
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case (2):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 4;
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
        float t = subtitltes.GetComponent<SubstitlesScript>().theTimer;
        switch ((int)subtitltes.GetComponent<SubstitlesScript>().theTimer)
        {
            case (1):
                if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-001") == false)
                SoundManager.PlayVoice("Medical_VR_Game_VO_Line-001");
                break;
            case (4):
                doAction = RiseCurtain;
                fadeSpeed = 0.5f;
                break;
            case (16):
                if(t > 16.23f)
                if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-002") == false)
                    SoundManager.PlayVoice("Medical_VR_Game_VO_Line-002");
                break;
            case (24):
                if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-003") == false)
                    SoundManager.PlayVoice("Medical_VR_Game_VO_Line-003");
                break;
            case (35):
                doAction = NullFunction;
                subtitltes.GetComponent<SubstitlesScript>().Stop();
                break;
            case (36):
                if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-004") == false)
                    SoundManager.PlayVoice("Medical_VR_Game_VO_Line-004");
                break;
            case (48):
                if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-005") == false)
                    SoundManager.PlayVoice("Medical_VR_Game_VO_Line-005");
                break;
            case (58):
                if (t > 58.5f)
                    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-006") == false)
                    SoundManager.PlayVoice("Medical_VR_Game_VO_Line-006");
                break;
            case (65):
                doAction = NullFunction;
                if (virus.activeSelf == false)
                {
                    virus.SetActive(true);

                    subtitltes.GetComponent<SubstitlesScript>().Stop();
                }
                break;
            case (66):
                 if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-007") == false)
                     SoundManager.PlayVoice("Medical_VR_Game_VO_Line-007");
                break;
            case (78):
                if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-008") == false)
                    SoundManager.PlayVoice("Medical_VR_Game_VO_Line-008");
                break;
            case (87):
                if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-009") == false)
                    SoundManager.PlayVoice("Medical_VR_Game_VO_Line-009");
                break;
            case (95):
                if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-010") == false)
                    SoundManager.PlayVoice("Medical_VR_Game_VO_Line-010");
                break;
            case (105):
                if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-011") == false)
                    SoundManager.PlayVoice("Medical_VR_Game_VO_Line-011");

                doAction = NullFunction;
                   I = 1;
                  //moveSpeed = 30;
                break;
            case (119):
                MovingCamera.arcadeMode = false;
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (120):
                if (subtitltes.GetComponent<SubstitlesScript>().theTimer < 120.5f)
                    SceneManager.LoadScene("DodgeAnitbodies");
                break;
            case (130):
                I = 3;
                moveSpeed = 100;
                break;
            case (149):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (150):
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 4;
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
