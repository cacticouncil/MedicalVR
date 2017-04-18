using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CellGameplayScript : MonoBehaviour
{

    public List<GameObject> places;
    public List<GameObject> Sceneries;
    public GameObject subtitles, blackCurtain, theCamera, virus, rna;
    public static int loadCase;
    // Use this for initialization
    delegate void Func();
    Func doAction;
    public float moveSpeed;
    float fadeSpeed;
    int I = 0;
    void Start()
    {
        doAction = NullFunction;
        virus.SetActive(false);
        Sceneries[1].SetActive(false);
        Sceneries[2].SetActive(false);
        Sceneries[3].SetActive(false);
        switch (loadCase)
        {
            case (1):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(true);

                I = 2;
                subtitles.GetComponent<SubstitlesScript>().theTimer = 132.5f;
                theCamera.transform.position = places[I].transform.position;
                virus.SetActive(true);
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case (2):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(false);
                Sceneries[3].SetActive(true);
                I = 7;
                subtitles.GetComponent<SubstitlesScript>().theTimer = 287.5f;
                theCamera.transform.position = places[I].transform.position;
                virus.SetActive(true);
                virus.GetComponent<Virus_VirusGameplay>().dna.transform.position = virus.GetComponent<Virus_VirusGameplay>().places[10].transform.position;
                virus.GetComponent<Virus_VirusGameplay>().dna.transform.rotation = virus.GetComponent<Virus_VirusGameplay>().places[10].transform.rotation;
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case (3):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                Sceneries[3].SetActive(false);
                RenderSettings.fogDensity = 0;
                subtitles.GetComponent<SubstitlesScript>().theTimer = 352.5f;
                I = 9;
                theCamera.transform.position = places[I].transform.position;
                break;
            default:
                break;
        }
    }
    void NullFunction()
    {

    }
    // Update is called once per frame
    void Update()
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
        blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a - (Time.deltaTime * fadeSpeed));

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
        if (I != places.Count)
            theCamera.transform.position = Vector3.MoveTowards(theCamera.transform.position, places[I].transform.position, moveSpeed * Time.deltaTime);
    }
    void CheckCaases()
    {
        float t = subtitles.GetComponent<SubstitlesScript>().theTimer;
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case (0):
                doAction = RiseCurtain;
                fadeSpeed = 0.5f;
                break;
            case (1):
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line01") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line01");
                break;
            case (5):
                break;
            case (16):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                virus.SetActive(true);
                break;
            case (17):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(true);
                I = 1;
                theCamera.transform.position = places[I].transform.position;
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case (20):
                doAction = NullFunction;
                I = 2;
                moveSpeed = 100;
                break;
            case (28):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (29):
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 3;
                RenderSettings.fogDensity = 0;
                theCamera.transform.position = places[I].transform.position;
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case (44):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (45):
                //SceneManager.LoadScene("ATPGTPShooter");
                break;
            case (46):
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case (48):
                I = 4;
                moveSpeed = 400;
                break;
            case (58):
                subtitles.GetComponent<SubstitlesScript>().Stop();
                break;
            case (62):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (63):
                //SceneManager.LoadScene("CGampSnatcher");
                break;
            case (64):
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case (68):
                I = 5;
                moveSpeed = 200;
                break;
            case (80):
                I = 6;
                moveSpeed = 200;
                break;
            case (85):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (86):
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                Sceneries[2].SetActive(false);
                Sceneries[3].SetActive(true);
                I = 7;
                theCamera.transform.position = places[I].transform.position;
                break;
            case 90:
                I = 8;
                break;
            case 94:
                rna.SetActive(true);
                break;
            case 95:
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case 96:
                Sceneries[2].SetActive(true);
                Sceneries[3].SetActive(false);
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                I = 9;
                theCamera.transform.position = places[I].transform.position;
                break;
            case 100:
                I = 10;
                moveSpeed = 175;
                break;
            case 104:
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case 105:
               // SceneManager.LoadScene("MemoryGame");
                break;
            case (106):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case 118:
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case 119:
                // SceneManager.LoadScene("FightVirus");
                break;
            case 120:
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case 125:
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case 126:
                SceneManager.LoadScene("MainMenu");
                break;
            default:
                break;
        }
    }
}
