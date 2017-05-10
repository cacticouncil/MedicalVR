using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class VirusGameplayScript : MonoBehaviour
{
    public List<GameObject> places;
    public List<GameObject> Sceneries;
    public List<Transform> rotationTargets;
    public GameObject subtitles, blackCurtain, theCamera, virus, rna, parent;
    public static int loadCase =0;
    // Use this for initialization
    delegate void Func();
    Func doAction;
    public float moveSpeed;
    float fadeSpeed;
    int I = 0;
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
                subtitles.GetComponent<SubstitlesScript>().theTimer = 353f;
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
            case (1):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-001") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-001");
                break;
            case (4):
                doAction = RiseCurtain;
                fadeSpeed = 0.5f;
                break;
            case (16):
                if (t > 16.23f)
                    if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-002") == false)
                        SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-002");
                break;
            case (24):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-003") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-003");
                break;
            case (35):
                doAction = NullFunction;
                subtitles.GetComponent<SubstitlesScript>().Stop();
                break;
            case (36):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-004") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-004");
                break;
            case (48):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-005") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-005");
                break;
            case (58):
                if (t > 58.5f)
                    if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-006") == false)
                        SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-006");
                break;
            case (65):
                doAction = NullFunction;
                if (virus.activeSelf == false)
                {
                    virus.SetActive(true);

                    subtitles.GetComponent<SubstitlesScript>().Stop();
                }
                break;
            case (66):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-007") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-007");
                break;
            case (78):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-008") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-008");
                break;
            case (87):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-009") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-009");
                break;
            case (95):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-010") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-010");
                break;
            case (100):
                if ((t > 100.5f))
                    if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-011_B") == false)
                        SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-011_B");
                break;
            case (105):
                if ((t > 105.5f))
                    if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-011") == false)
                        SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-011");

                doAction = NullFunction;
                I = 1;
                moveSpeed = 40;
                break;
            case (116):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-012") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-012");
                break;
            case (131):
                GlobalVariables.arcadeMode = false;
                GlobalVariables.tutorial = true;
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (132):
                if (subtitles.GetComponent<SubstitlesScript>().theTimer < 132.5f)
                    SceneManager.LoadScene("DodgeAntibodies");
                break;
            case (133):
                parent.GetComponent<LookCamera>().target = rotationTargets[0];
                parent.GetComponent<LookCamera>().enabled = true;
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-013") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-013");
                break;
            case (146):
                parent.GetComponent<LookCamera>().enabled = false;
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-014") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-014");
                break;
            case (151):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-015") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-015");
                I = 3;
                moveSpeed = 10;
                break;
            case (158):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-016") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-016");
                break;
            case (162):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-017") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-017");
                break;
            case (171):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-018") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-018");
                break;
            case (177):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-019") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-019");
                break;
            case (181):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (182):
                parent.GetComponent<LookCamera>().target = rotationTargets[1];
                parent.GetComponent<LookCamera>().enabled = true;
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 4;
                RenderSettings.fogDensity = 0;
                theCamera.transform.position = places[I].transform.position;
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case (183):
                parent.GetComponent<LookCamera>().enabled = false;
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-020") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-020");
                break;
            case (192):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-021") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-021");
                break;
            case (198):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-022") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-022");
                break;
            case (205):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-023") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-023");
                break;
            case (216):
                if ((t > 216.5f))
                    if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-024") == false)
                        SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-024");
                break;
            case (227):
                doAction = NullFunction;
                I = 5;
                moveSpeed = 25;
                break;
            case (236):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-026") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-026");
                break;
            case (246):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-027") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-027");
                break;
            case (248):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (249):
                I = 6;
                parent.GetComponent<LookCamera>().target = rotationTargets[2];
                parent.GetComponent<LookCamera>().enabled = true;
                Sceneries[3].SetActive(true);
                RenderSettings.fogDensity = 0;
                theCamera.transform.position = places[I].transform.position;
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-028") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-028");
                break;
            case (252):
                parent.GetComponent<LookCamera>().enabled = false;
                doAction = NullFunction;
                I = 7;
                moveSpeed = 5;
                break;
            case (257):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-029") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-029");
                break;
            case (262):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-030") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-030");
                break;
            case (270):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-031") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-031");
                break;
            case (277):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-032") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-032");
                break;
            case (286):
                GlobalVariables.tutorial = true;
                GlobalVariables.arcadeMode = false;
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (287):
                if (subtitles.GetComponent<SubstitlesScript>().theTimer < 287.5)
                    SceneManager.LoadScene("SimonDNA");
                break;
            case (288):
                parent.GetComponent<LookCamera>().target = rotationTargets[3];
                parent.GetComponent<LookCamera>().enabled = true;
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-033") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-033");
                rna.SetActive(true);
                break;
            case (295):
                parent.GetComponent<LookCamera>().enabled = false;
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (296):
                parent.GetComponent<LookCamera>().target = rotationTargets[4];
                parent.GetComponent<LookCamera>().enabled = true;
                Sceneries[3].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 8;
                RenderSettings.fogDensity = 0;
                theCamera.transform.position = places[I].transform.position;
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-034") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-034");
                break;
            case (297):
                parent.GetComponent<LookCamera>().enabled = false;
                I = 9;
                moveSpeed = 17.5f;
                break;
            case (303):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-035") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-035");
                break;
            case (316):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-036") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-036");
                break;
            case (322):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (323):
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-037") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-037");
                break;
            case (331):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-038") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-038");
                break;
            case (339):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-039") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-039");
                break;
            case (351):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (352):
                GlobalVariables.arcadeMode = false;
                SceneManager.LoadScene("DestroyTheCell");
                
                break;
            case (353):
                parent.GetComponent<LookCamera>().target = rotationTargets[5];
                parent.GetComponent<LookCamera>().enabled = true;
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-040") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-040");
                break;
            case (367):
                if (SoundManager.IsVirusVoicePlaying("Medical_VR_Game_VO_Line-041_A") == false)
                    SoundManager.PlayVirusVoice("Medical_VR_Game_VO_Line-041_A");
                break;
            case (373):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (374):
                GlobalVariables.VirusGameplayCompleted = 1;
                SceneManager.LoadScene("MainMenu");
                break;
            default:
                break;
        }
    }
}
