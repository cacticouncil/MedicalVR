using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CellGameplayScript : MonoBehaviour
{
    public GameObject cGAS_pic, sting_pic;
    public List<Transform> places = new List<Transform>();
    public List<GameObject> Sceneries = new List<GameObject>();
    public List<Transform> rotationTargets = new List<Transform>();
    public GameObject subtitles, blackCurtain, virus, rna;
    public static int loadCase;
    public float moveSpeed;

    delegate void Func();
    Func doAction;
    private float fadeSpeed = .03f;
    int I = 0;
    private bool firstLook = true;

    // Use this for initialization
    void Start()
    {
        GlobalVariables.tutorial = true;
        doAction = NullFunction;
        virus.SetActive(false);
        Sceneries[1].SetActive(false);
        Sceneries[2].SetActive(false);
        Sceneries[3].SetActive(false);
        switch (loadCase)
        {
            case (1):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 3;
                subtitles.GetComponent<SubstitlesScript>().theTimer = 96f;
                transform.position = places[I].position;
                virus.SetActive(true);
                RenderSettings.fogDensity = 0;
                break;
            case (2):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 4;
                subtitles.GetComponent<SubstitlesScript>().theTimer = 145f;
                transform.position = places[I].position;
                virus.SetActive(true);
                RenderSettings.fogDensity = 0;
                break;
            case (3):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 10;
                subtitles.GetComponent<SubstitlesScript>().theTimer = 231f;
                transform.position = places[I].position;
                virus.SetActive(true);
                RenderSettings.fogDensity = 0;
                break;
            case 4:
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 10;
                subtitles.GetComponent<SubstitlesScript>().theTimer = 262f;
                transform.position = places[I].position;
                virus.SetActive(true);
                RenderSettings.fogDensity = 0;
                break;
            default:
                break;
        }
    }

    void NullFunction()
    {

    }

    void FixedUpdate()
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
        blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a - fadeSpeed);

    }

    void LowerCurtain()
    {
        float a = blackCurtain.GetComponent<Renderer>().material.color.a;
        if (a < 0)
            a = 0;
        blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a + fadeSpeed);
    }

    void MoveTo()
    {
        if (I != places.Count)
            transform.position = Vector3.MoveTowards(transform.position, places[I].position, moveSpeed);
    }

    void CheckCaases()
    {
        float t = subtitles.GetComponent<SubstitlesScript>().theTimer;
        switch ((int)subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            case (0):
                doAction = RiseCurtain;
                fadeSpeed = 0.01f;
                break;
            case (1):
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line01") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line01");
                break;
            case (7):
                if (t >= 7.5)
                    if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line02") == false)
                        SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line02");
                break;
            case (23):
                doAction = LowerCurtain;
                fadeSpeed = .03f;
                virus.SetActive(true);
                break;
            case (24):
                if (firstLook)
                {
                    firstLook = false;
                    GetComponent<LookCamera>().target = rotationTargets[0];
                    GetComponent<LookCamera>().enabled = true;
                }
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(true);
                I = 1;
                transform.position = places[I].position;
                doAction = RiseCurtain;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line03") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line03");
                break;
            case (25):
                firstLook = true;
                // doAction = NullFunction;
                I = 2;
                moveSpeed = .01f;
                break;
            case (30):
                doAction = LowerCurtain;
                break;
            case (31):
                if (firstLook)
                {
                    firstLook = false;
                    GetComponent<LookCamera>().enabled = true;
                }
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 3;
                RenderSettings.fogDensity = 0;
                transform.position = places[I].position;
                doAction = RiseCurtain;
                break;
            case 37:
                firstLook = true;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line04") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line04");
                break;

            case (43):
                cGAS_pic.SetActive(true);
                break;
            case (44):
                if (t >= 44.5)
                    subtitles.GetComponent<SubstitlesScript>().Stop();
                break;
            case 46:
                cGAS_pic.SetActive(false);
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line05") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line05");
                break;
            case 54:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line06") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line06");
                break;
            case 61:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line07") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line07");
                break;
            case 75:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line08") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line08");
                break;
            case 90:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line09") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line09");
                break;
            case (94):
                doAction = LowerCurtain;
                break;
            case (95):
                GlobalVariables.arcadeMode = false;
                SceneManager.LoadScene("ATPGTPShooter");
                break;
            case (96):
                if (firstLook)
                {
                    firstLook = false;
                    GetComponent<LookCamera>().target = rotationTargets[1];
                    GetComponent<LookCamera>().enabled = true;
                }
                doAction = RiseCurtain;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line10") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line10");
                break;
            case 105:
                firstLook = true;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line11") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line11");
                break;
            case (106):
                I = 4;
                moveSpeed = .008f;
                break;
            case 113:
                if (t >= 113.5)
                    if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line12") == false)
                        SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line12");
                break;
            case 120:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line13") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line13");
                break;
            case 129:
                sting_pic.SetActive(true);
                break;
            case 131:
                sting_pic.SetActive(false);
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line14") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line14");
                break;
            case (143):
                doAction = LowerCurtain;
                break;
            case (144):
                //cGAMP
                GlobalVariables.arcadeMode = false;
                SceneManager.LoadScene("CGampSnatcher");
                break;
            case (145):
                if (firstLook)
                {
                    firstLook = false;
                    GetComponent<LookCamera>().target = rotationTargets[2];
                    GetComponent<LookCamera>().enabled = true;
                }
                doAction = RiseCurtain;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line15") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line15");
                break;
            case 158:
                firstLook = true;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line16") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line16");
                break;
            case (159):
                I = 5;
                moveSpeed = .004f;
                break;
            case 167:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line17") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line17");
                break;
            case 177:
                if (t >= 177.5f)
                    if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line18") == false)
                        SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line18");
                break;
            case (182):
                I = 6;
                moveSpeed = .001f;
                break;
            case 186:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line19") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line19");
                break;
            case (190):
                doAction = LowerCurtain;
                break;
            case (191):
                if (firstLook)
                {
                    firstLook = false;
                    GetComponent<LookCamera>().target = rotationTargets[3];
                    GetComponent<LookCamera>().enabled = true;
                }
                doAction = RiseCurtain;
                Sceneries[2].SetActive(false);
                Sceneries[3].SetActive(true);
                I = 7;
                transform.position = places[I].position;
                break;
            case 194:
                firstLook = true;
                moveSpeed = .006f;
                I = 8;
                break;
            case 204:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line20") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line20");
                break;
            case 209:
                rna.SetActive(true);
                break;
            case 211:
                doAction = LowerCurtain;
                break;
            case 212:
                if (firstLook)
                {
                    firstLook = false;
                    GetComponent<LookCamera>().target = rotationTargets[4];
                    GetComponent<LookCamera>().enabled = true;
                }
                Sceneries[2].SetActive(true);
                Sceneries[3].SetActive(false);
                doAction = RiseCurtain;
                I = 9;
                transform.position = places[I].position;
                RenderSettings.fogDensity = 0;
                break;
            case 213:
                firstLook = true;
                I = 10;
                //moveSpeed = .3f;
                if (t >= 213.5f)
                    if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line21") == false)
                        SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line21");
                break;
            case 226:
                firstLook = true;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line22") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line22");
                break;
            case 229:
                doAction = LowerCurtain;
                break;
            case 230:
                //Memory Game
                GlobalVariables.arcadeMode = false;
                SceneManager.LoadScene("MemoryGame");
                break;
            case (231):
                if (firstLook)
                {
                    firstLook = false;
                    GetComponent<LookCamera>().target = rotationTargets[5];
                    GetComponent<LookCamera>().enabled = true;
                }
                doAction = RiseCurtain;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line23") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line23");
                break;
            case 238:
                firstLook = true;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line24") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line24");
                break;
            case 250:
                doAction = LowerCurtain;
                break;
            case 251:
                if (t >= 251.5)
                    if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line25") == false)
                        SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line25");
                doAction = RiseCurtain;
                break;
            case 258:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line26") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line26");

                break;
            case 260:
                doAction = LowerCurtain;
                break;
            case 261:
                //FightVirus
                GlobalVariables.arcadeMode = false;
                SceneManager.LoadScene("FightVirus");
                break;
            case 262:
                if (firstLook)
                {
                    firstLook = false;
                    GetComponent<LookCamera>().target = rotationTargets[5];
                    GetComponent<LookCamera>().enabled = true;
                }
                doAction = RiseCurtain;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line27") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line27");
                break;
            case 274:
                firstLook = true;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line28") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line28");
                break;
            case 285:
                doAction = LowerCurtain;
                break;
            case 286:
                GlobalVariables.CellGameplayCompleted = 1;
                SceneManager.LoadScene("MainMenu");
                break;
            default:
                break;
        }
    }
}
