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
                //if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-001") == false)
                //    SoundManager.PlayVoice("Medical_VR_Game_VO_Line-001");
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
            //case (65):
            //    doAction = NullFunction;
            //    if (virus.activeSelf == false)
            //    {
            //        virus.SetActive(true);

            //        subtitles.GetComponent<SubstitlesScript>().Stop();
            //    }
            //    break;
            //case (66):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-007") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-007");
            //    break;
            //case (78):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-008") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-008");
            //    break;
            //case (87):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-009") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-009");
            //    break;
            //case (95):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-010") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-010");
            //    break;
            //case (100):
            //    if ((t > 100.5f))
            //        if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-011_B") == false)
            //            SoundManager.PlayVoice("Medical_VR_Game_VO_Line-011_B");
            //    break;
            //case (105):
            //    if ((t > 105.5f))
            //        if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-011") == false)
            //            SoundManager.PlayVoice("Medical_VR_Game_VO_Line-011");

            //    doAction = NullFunction;
            //    I = 1;
            //    //moveSpeed = 30;
            //    break;
            //case (116):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-012") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-012");
            //    break;
            //case (131):
            //    MovingCamera.arcadeMode = false;
            //    doAction = LowerCurtain;
            //    fadeSpeed = 1.5f;
            //    break;
            //case (132):
            //    if (subtitles.GetComponent<SubstitlesScript>().theTimer < 132.5f)
            //        SceneManager.LoadScene("DodgeAnitbodies");
            //    break;
            //case (133):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-013") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-013");
            //    break;
            //case (146):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-014") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-014");
            //    break;
            //case (151):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-015") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-015");
            //    I = 3;
            //    moveSpeed = 100;
            //    break;
            //case (158):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-016") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-016");
            //    break;
            //case (162):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-017") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-017");
            //    break;
            //case (171):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-018") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-018");
            //    break;
            //case (177):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-019") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-019");
            //    break;
            //case (181):
            //    doAction = LowerCurtain;
            //    fadeSpeed = 1.5f;
            //    break;
            //case (182):
            //    Sceneries[1].SetActive(false);
            //    Sceneries[2].SetActive(true);
            //    I = 4;
            //    RenderSettings.fogDensity = 0;
            //    theCamera.transform.position = places[I].transform.position;
            //    doAction = RiseCurtain;
            //    fadeSpeed = 1.5f;
            //    break;
            //case (183):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-020") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-020");
            //    break;
            //case (192):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-021") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-021");
            //    break;
            //case (198):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-022") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-022");
            //    break;
            //case (205):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-023") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-023");
            //    break;
            //case (216):
            //    if ((t > 216.5f))
            //        if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-024") == false)
            //            SoundManager.PlayVoice("Medical_VR_Game_VO_Line-024");
            //    break;
            //case (227):
            //    doAction = NullFunction;
            //    I = 5;
            //    moveSpeed = 250;
            //    break;
            //case (236):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-026") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-026");
            //    break;
            //case (246):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-027") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-027");
            //    break;
            //case (248):
            //    doAction = LowerCurtain;
            //    fadeSpeed = 1.5f;
            //    break;
            //case (249):
            //    I = 6;
            //    Sceneries[3].SetActive(true);
            //    RenderSettings.fogDensity = 0;
            //    theCamera.transform.position = places[I].transform.position;
            //    doAction = RiseCurtain;
            //    fadeSpeed = 1.5f;
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-028") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-028");
            //    break;
            //case (252):
            //    doAction = NullFunction;
            //    I = 7;
            //    moveSpeed = 50;
            //    break;
            //case (257):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-029") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-029");
            //    break;
            //case (262):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-030") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-030");
            //    break;
            //case (270):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-031") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-031");
            //    break;
            //case (277):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-032") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-032");
            //    break;
            //case (286):
            //    SimonSays.arcadeMode = false;
            //    doAction = LowerCurtain;
            //    fadeSpeed = 1.5f;
            //    break;
            //case (287):
            //    if (subtitles.GetComponent<SubstitlesScript>().theTimer < 287.5)
            //        SceneManager.LoadScene("SimonDNA");
            //    break;
            //case (288):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-033") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-033");
            //    rna.SetActive(true);
            //    break;
            //case (295):
            //    doAction = LowerCurtain;
            //    fadeSpeed = 1.5f;
            //    break;
            //case (296):
            //    Sceneries[3].SetActive(false);
            //    Sceneries[2].SetActive(true);
            //    I = 8;
            //    RenderSettings.fogDensity = 0;
            //    theCamera.transform.position = places[I].transform.position;
            //    doAction = RiseCurtain;
            //    fadeSpeed = 1.5f;
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-034") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-034");
            //    break;
            //case (297):
            //    I = 9;
            //    moveSpeed = 175;
            //    break;
            //case (303):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-035") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-035");
            //    break;
            //case (316):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-036") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-036");
            //    break;
            //case (322):
            //    doAction = LowerCurtain;
            //    fadeSpeed = 1.5f;
            //    break;
            //case (323):
            //    doAction = RiseCurtain;
            //    fadeSpeed = 1.5f;
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-037") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-037");
            //    break;
            //case (331):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-038") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-038");
            //    break;
            //case (339):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-039") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-039");
            //    break;
            //case (351):
            //    doAction = LowerCurtain;
            //    fadeSpeed = 1.5f;
            //    break;
            //case (352):
            //    VirusPlayer.ArcadeMode = false;
            //    //SceneManager.LoadScene("DestroyTheCell");
            //    if (t > 352.5)
            //    {
            //        doAction = RiseCurtain;
            //        fadeSpeed = 1.5f;
            //    }
            //    break;
            //case (353):

            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-040") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-040");
            //    break;
            //case (367):
            //    if (SoundManager.IsVoicePlaying("Medical_VR_Game_VO_Line-041_A") == false)
            //        SoundManager.PlayVoice("Medical_VR_Game_VO_Line-041_A");
            //    break;
            //case (373):
            //    doAction = LowerCurtain;
            //    fadeSpeed = 1.5f;
            //    break;
            //case (374):
            //    SceneManager.LoadScene("MainMenu");
            //    break;
            default:
                break;
        }
    }
}
