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
        Tutorial.tutorial = true;
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
                theCamera.transform.position = places[I].transform.position;
                virus.SetActive(true);
                RenderSettings.fogDensity = 0;
                break;
            case (2):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 4;
                subtitles.GetComponent<SubstitlesScript>().theTimer = 145f;
                theCamera.transform.position = places[I].transform.position;
                virus.SetActive(true);
                RenderSettings.fogDensity = 0;
                break;
            case (3):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 10;
                subtitles.GetComponent<SubstitlesScript>().theTimer = 231f;
                theCamera.transform.position = places[I].transform.position;
                virus.SetActive(true);
                RenderSettings.fogDensity = 0;
                break;
            case 4:
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 10;
                subtitles.GetComponent<SubstitlesScript>().theTimer = 261f;
                theCamera.transform.position = places[I].transform.position;
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
            case (0):
                doAction = RiseCurtain;
                fadeSpeed = 0.5f;
                break;
            case (1):
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line01") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line01");
                break;
            case (7):
                if(t >= 7.5)
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line02") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line02");
                break;
            case (23):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                virus.SetActive(true);
                break;
            case (24):
                Sceneries[0].SetActive(false);
                Sceneries[1].SetActive(true);
                I = 1;
                theCamera.transform.position = places[I].transform.position;
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line03") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line03");
                break;
            case (25):
               // doAction = NullFunction;
                I = 2;
                moveSpeed = 100;
                break;
            case (30):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (31):
                Sceneries[1].SetActive(false);
                Sceneries[2].SetActive(true);
                I = 3;
                RenderSettings.fogDensity = 0;
                theCamera.transform.position = places[I].transform.position;
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case 37:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line04") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line04");
                break;
            case 46:
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
                fadeSpeed = 1.5f;
                break;
            case (95):
                //SceneManager.LoadScene("ATPGTPShooter");
                break;
            case (96):
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line10") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line10");
                break;
            case 105:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line11") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line11");
                break;
            case (106):
                I = 4;
                moveSpeed = 400;
                break;
            case 113:
                if(t >= 113.5)
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line12") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line12");
                break;
            case 120:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line13") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line13");
                break;
            case 131:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line14") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line14");
                break;
            case (143):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (144):
                //cGAMP
                Storebullets.arcadeMode = false;
                Storebullets.TutorialMode = true;
                SceneManager.LoadScene("CGampSnatcher");
                break;
            case (145):
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line15") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line15");
                break;
            case 158:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line16") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line16");
                break;
            case (159):
                I = 5;
                moveSpeed = 200;
                break;
            case 167:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line17") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line17");
                break;
            case 177:
                if(t >= 177.5f)
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line18") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line18");
                break;
            case (182):
                I = 6;
                moveSpeed = 200;
                break;
            case 186:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line19") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line19");
                break;
            case (190):
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case (191):
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                Sceneries[2].SetActive(false);
                Sceneries[3].SetActive(true);
                I = 7;
                theCamera.transform.position = places[I].transform.position;
                break;
            case 194:
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
                fadeSpeed = 1.5f;
                break;
            case 212:
                Sceneries[2].SetActive(true);
                Sceneries[3].SetActive(false);
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                I = 9;
                theCamera.transform.position = places[I].transform.position;
                RenderSettings.fogDensity = 0;
                break;
            case 213:
                I = 10;
                moveSpeed = 300;
                if(t >= 213.5f)
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line21") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line21");
                break;
            case 226:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line22") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line22");
                break;
            case 229:
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case 230:
                //Memory Game
                MemoryUI.arcadeMode = false;
                MemoryUI.TutorialMode = true;
                SceneManager.LoadScene("MemoryGame");
                break;
            case (231):
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line23") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line23");
                break;
            case 238:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line24") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line24");
                break;
            case 249:
                if(t >= 249.5)
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line25") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line25");
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case 250:
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                break;
            case 256:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line26") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line26");

                break;
            case 259:
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case 260:
                //FightVirus
                Player.ArcadeMode = false;
                Player.StoryMode = true;
                SceneManager.LoadScene("FightVirus");
                break;
            case 261:
                doAction = RiseCurtain;
                fadeSpeed = 1.5f;
                if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line27") == false)
                    SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line27");
                break;
            case 273:
                
                    if (SoundManager.IsCellVoicePlaying("Medical_VR_Cell_VO_Line28") == false)
                        SoundManager.PlayCellVoice("Medical_VR_Cell_VO_Line28");
                break;
            case 284:
                doAction = LowerCurtain;
                fadeSpeed = 1.5f;
                break;
            case 285:
                SceneManager.LoadScene("MainMenu");
                break;
            default:
                break;
        }
    }
}
