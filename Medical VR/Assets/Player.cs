using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Player : MonoBehaviour, TimedInputHandler
{
    public static bool ArcadeMode = true;
    public static bool StoryMode = false;

    //Variables for tutorial
    float TutorialTimer = 0.0f;
    int WhatToRead = 0;
    bool SpawnWaveOnce = false;
    bool SpawnWaveTwice = false;

    //Variables for game
    public float Score = 0.0f;
    public int VirusLeaveCount = 0;

    float RuleTimer = 0.0f;
    float Wave1Timer = 0.0f;
    float Wave2Timer = 0.0f;
    float Wave3Timer = 0.0f;
    float Wave4Timer = 0.0f;
    float BeatGameTimer = 0.0f;

    bool DisplayRules;
    public bool isGameOver;
    public bool DisplayWaveNumber = false;

    public GameObject ScoreObj;
    public GameObject VirusCount;
    public GameObject CenterScreenObj;
    public GameObject EnemyManger;
    public GameObject BulletSpawn;
    public GameObject BlackCurtain;

    public GameObject ScoreBoard;
    public FacebookStuff FB;

    void Start()
    {
        DisplayRules = true;
        isGameOver = false;
        SetFacebook();

        if (GlobalVariables.tutorial == false)
            BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (GlobalVariables.tutorial == false)
        {
            if (isGameOver == false)
            {
                if (StoryMode == false)
                    ScoreObj.GetComponent<TextMeshPro>().text = "Score: " + Score.ToString();

                VirusCount.GetComponent<TextMeshPro>().text = "VirusCount: " + VirusLeaveCount.ToString();


                if (RuleTimer >= 4.0f && RuleTimer <= 5.0f)
                {
                    RuleTimer = 10.0f;
                    DisplayRules = false;
                    EnemyManger.GetComponent<VirusManager>().CheckCount = true;
                }

                if (DisplayRules)
                    CenterScreenObj.GetComponent<TextMeshPro>().text = "  Prevent the Virus " + "\n" + "from leaving the cell";
                else
                    CenterScreenObj.GetComponent<TextMeshPro>().text = "";

                if (DisplayWaveNumber == true)
                {
                    switch (EnemyManger.GetComponent<VirusManager>().WaveNumber)
                    {
                        case 1:
                            if (Wave1Timer <= 2.0f)
                                CenterScreenObj.GetComponent<TextMeshPro>().text = "    Wave 1";

                            else
                                DisplayWaveNumber = false;

                            Wave1Timer += Time.deltaTime;
                            break;

                        case 2:
                            if (Wave2Timer <= 2.0f)
                                CenterScreenObj.GetComponent<TextMeshPro>().text = "    Wave 2";

                            else
                                DisplayWaveNumber = false;

                            Wave2Timer += Time.deltaTime;
                            break;

                        case 3:
                            if (Wave3Timer <= 2.0f)
                                CenterScreenObj.GetComponent<TextMeshPro>().text = "    Wave 3";

                            else
                                DisplayWaveNumber = false;

                            Wave3Timer += Time.deltaTime;

                            break;

                        case 4:
                            if (Wave4Timer <= 2.0f)
                                CenterScreenObj.GetComponent<TextMeshPro>().text = "    Boss";

                            else
                                DisplayWaveNumber = false;

                            Wave4Timer += Time.deltaTime;

                            break;

                        default:
                            break;
                    }
                }

                RuleTimer += Time.deltaTime;
            }

            if (ArcadeMode == true)
            {
                //If you lose arcade bring up scorebaord
                if (VirusLeaveCount == 10)
                {
                    isGameOver = true;
                    ScoreBoard.SetActive(true);
                    CenterScreenObj.GetComponent<TextMeshPro>().text = "You lose arcade mode";
                    ScoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
                }

                //If you win arcade bring up score board
                if (EnemyManger.GetComponent<VirusManager>().WaveNumber >= 5 && EnemyManger.GetComponent<VirusManager>().VirusList.Count == 0 && VirusLeaveCount < 10)
                {
                    isGameOver = true;
                    ScoreBoard.SetActive(true);
                    CenterScreenObj.GetComponent<TextMeshPro>().text = "You win arcade mode";
                    ScoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
                }
            }

            else if (ArcadeMode == false)
            {
                //If you lose story mode keep making them play until they beat it
                if (VirusLeaveCount == 5)
                {
                    isGameOver = true;
                    CenterScreenObj.GetComponent<TextMeshPro>().text = "You lose story mode";
                    PlayStory();
                }

                //If you win story mode continue to next scene and don't forget to fade out
                if (EnemyManger.GetComponent<VirusManager>().WaveNumber >= 5 && EnemyManger.GetComponent<VirusManager>().VirusList.Count == 0)
                {
                    isGameOver = true;
                    CenterScreenObj.GetComponent<TextMeshPro>().text = "You win story mode";
                    BeatGameTimer += Time.deltaTime;
                    float a = BlackCurtain.GetComponent<Renderer>().material.color.a;
                    if (a < 0)
                        a = 0;
                    BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a + (Time.deltaTime * 1.5f));

                    if (BeatGameTimer >= 2.0f)
                    {
                        CellGameplayScript.loadCase = 4;
                        SceneManager.LoadScene("Cell zGamePlay");
                    }
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set up how turtorial will show players basic gameplay
        else if (GlobalVariables.tutorial == true)
        {
            //Need to fade in
            float a = BlackCurtain.GetComponent<Renderer>().material.color.a;
            BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a - (Time.deltaTime * 1.5f));

            switch (WhatToRead)
            {
                case 0:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Welcome to Fight Virus";

                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 1:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Your objective is to " + "\n" + "prevent any viruses from leaving the cell";

                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 2:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  If you look around " + "\n" + "there are four zones to protect";

                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 3:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Don't let any virus get to these zones";

                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 4:
                    TutorialTimer += Time.deltaTime;
                    if (SpawnWaveOnce == false)
                    {
                        SpawnWaveOnce = true;
                        EnemyManger.GetComponent<VirusManager>().CanISpawn = true;
                        EnemyManger.GetComponent<VirusManager>().WaveNumber = 1;
                    }

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  If three of them meet up they " + "\n" + "will create bigger viruses";

                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 5:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Don't let any leave";

                    if (VirusLeaveCount == 1)
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 6:
                    TutorialTimer += Time.deltaTime;
                    BulletSpawn.GetComponent<BulletManager>().CanIShoot = true;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Click the button on the " + "\n" + "headset to destroy the viruses";

                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }

                    break;

                case 7:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Press it again to stop";

                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 8:
                    if (SpawnWaveTwice == false)
                    {
                        SpawnWaveTwice = true;
                        EnemyManger.GetComponent<VirusManager>().CanISpawn = true;
                        EnemyManger.GetComponent<VirusManager>().WaveNumber = 2;
                    }

                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Remember that viruses can spawn behind you";

                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                default:
                    CenterScreenObj.GetComponent<TextMeshPro>().text = " ";
                    break;
            }
        }

        //For tutorial only it will either transition to story mode or only play once
        if (WhatToRead >= 9 && EnemyManger.GetComponent<VirusManager>().VirusList.Count == 0)
        {
            BeatGameTimer += Time.deltaTime;
            CenterScreenObj.GetComponent<TextMeshPro>().text = "Great now you're ready to play";

            if (BeatGameTimer >= 2.0f)
            {
                if (StoryMode == true)
                    PlayStory();

                else
                    PlayArcade();
            }
        }
    }

    public void HandleTimeInput()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetFacebook()
    {
        FB.userName.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + Score.ToString(); /// + FacebookManager.Instance.GlobalScore /;
        FB.facebookPic.GetComponent<Image>().sprite = FacebookManager.Instance.ProfilePic;
    }

    public void PlayArcade()
    {
        GlobalVariables.tutorial = false;
        ArcadeMode = true;
        SceneManager.LoadScene("FightVirus");
    }

    public void PlayStory()
    {
        GlobalVariables.tutorial = false;
        ArcadeMode = false;
        SceneManager.LoadScene("FightVirus");
    }

    public void PlayTutorial()
    {
        GlobalVariables.tutorial = true;
        ArcadeMode = false;
        SceneManager.LoadScene("FightVirus");
    }
}
