using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    public GameObject ScoreObj;
    public GameObject VirusCount;
    public GameObject CenterScreenObj;
    public GameObject EnemyManger;
    public GameObject BulletSpawn;
    public GameObject BlackCurtain;

    public GameObject ScoreBoard;
    public FacebookStuff FB;

    //Variables for game
    public static float BestScoreForFightVirus;
    public float CurrentScore = 0.0f;
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
    public bool BeatBoss = false;

    //For text
    bool StopInput = false;
    public int WhatToRead = 0;
    public bool CanIRead = true;
    public TextMeshPro Text;
    private string[] TextList = new string[8];
    private bool last = false, text = false, finish = false;

    void Start()
    {
        BannerScript.LockTrophy("Virus Trophy");
        DisplayRules = true;
        isGameOver = false;
        SetFacebook();

        if (GlobalVariables.tutorial == false)
            BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);

        else
        {
            TextList[0] = "Welcome to Fight Virus.";
            TextList[1] = "Your objective is to " + "\n" + "prevent any viruses from leaving the cell.";
            TextList[2] = "If you look around " + "\n" + "there are four zones to protect.";
            TextList[3] = "Don't let any virus get to these zones.";
            TextList[4] = "If three of them meet up they " + "\n" + "will create bigger viruses, Don't let any leave.";
            TextList[5] = "Click the button on the " + "\n" + "headset to destroy the viruses, press it again to stop.";
            TextList[6] = "Remember that viruses can spawn behind you.";
            TextList[7] = "Great now you're ready to play.";
            TextForTutorial();
        }
    }

    void Update()
    {
        if (GlobalVariables.tutorial == false)
        {
            if (isGameOver == false)
            {
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

            if (GlobalVariables.arcadeMode)
            {
                ScoreObj.GetComponent<TextMeshPro>().text = "Score: " + CurrentScore.ToString();

                //If you lose arcade bring up scorebaord
                if (VirusLeaveCount == 10)
                {
                    isGameOver = true;
                    ScoreBoard.SetActive(true);
                    CenterScreenObj.GetComponent<TextMeshPro>().text = "You lose arcade mode";

                    if (CurrentScore > BestScoreForFightVirus)
                        BestScoreForFightVirus = CurrentScore;

                    ScoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
                }

                //If you win arcade bring up score board
                if (EnemyManger.GetComponent<VirusManager>().WaveNumber >= 5 && EnemyManger.GetComponent<VirusManager>().VirusList.Count == 0 && VirusLeaveCount < 10)
                {
                    isGameOver = true;
                    ScoreBoard.SetActive(true);
                    CenterScreenObj.GetComponent<TextMeshPro>().text = "You win arcade mode";

                    if (CurrentScore > BestScoreForFightVirus)
                        BestScoreForFightVirus = CurrentScore;

                    if (BestScoreForFightVirus > PlayerPrefs.GetFloat("FightVirusScore"))
                        PlayerPrefs.SetFloat("FightVirusScore", BestScoreForFightVirus);
                    else
                        BestScoreForFightVirus = PlayerPrefs.GetFloat("FightVirusScore");

                    ScoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);

                    if (VirusLeaveCount == 0 && BeatBoss == true)
                        BannerScript.UnlockTrophy("Virus Trophy");
                }
            }

            else
            {
                //If you lose story mode keep making them play until they beat it
                if (VirusLeaveCount == 5)
                {
                    isGameOver = true;
                    CenterScreenObj.GetComponent<TextMeshPro>().text = "You lose story mode";
                    SceneManager.LoadScene("FightVirus");
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
                        SceneManager.LoadScene("CellGameplay");
                    }
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set up turtorial 
        else if (GlobalVariables.tutorial == true)
        {
            if (StopInput == false)
            {
                bool held = Input.GetButton("Fire1");
                if (held && !last)
                {
                    if (text)
                        finish = true;

                    else
                        TextForTutorial();
                }
                last = held;
            }

            //Need to fade in
            float a = BlackCurtain.GetComponent<Renderer>().material.color.a;
            BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a - (Time.deltaTime * 1.5f));
        }

        if (VirusLeaveCount == 1 && WhatToRead == 5)
        {
            StopInput = false;
            WhatToRead += 1;
        }

        if (EnemyManger.GetComponent<VirusManager>().VirusList.Count == 0 && EnemyManger.GetComponent<VirusManager>().WaveNumber == 3 && WhatToRead == 9)
        {
            StopInput = false;
            WhatToRead++;
        }
    }

    void TextForTutorial()
    {
        switch (WhatToRead)
        {
            case 0:
                StartCoroutine(TurnTextOn(0));
                SoundManager.PlaySFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-001");
                break;

            case 1:
                StartCoroutine(TurnTextOn(1));
                SoundManager.stopSFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-001", false);
                SoundManager.PlaySFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-002");
                break;

            case 2:
                StartCoroutine(TurnTextOn(2));
                SoundManager.stopSFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-002", false);
                SoundManager.PlaySFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-003");
                break;

            case 3:
                StartCoroutine(TurnTextOn(3));
                SoundManager.stopSFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-003", false);
                SoundManager.PlaySFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-004");
                break;

            case 4:
                StartCoroutine(TurnTextOn(4));
                SoundManager.stopSFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-004", false);
                SoundManager.PlaySFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-005");
                break;

            case 5:
                //Need to spawn the wave only once but wait for them to create a big virus
                EnemyManger.GetComponent<VirusManager>().WaveNumber = 1;
                EnemyManger.GetComponent<VirusManager>().CanISpawn = true;
                StopInput = true;
                break;

            case 6:
                //StopInput = true;
                StartCoroutine(TurnTextOn(5));
                SoundManager.stopSFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-005", false);
                SoundManager.PlaySFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-006");
                break;

            case 7:
                //StopInput = false;
                BulletSpawn.GetComponent<BulletManager>().CanIShoot = true;
                WhatToRead++;
                break;

            case 8:
                StartCoroutine(TurnTextOn(6));
                SoundManager.stopSFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-006", false);
                SoundManager.PlaySFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-007");
                break;

            case 9:
                Text.text = "";
                EnemyManger.GetComponent<VirusManager>().WaveNumber = 2;
                EnemyManger.GetComponent<VirusManager>().CanISpawn = true;
                StopInput = true;
                break;

            case 10:
                StartCoroutine(TurnTextOn(7));
                break;

            case 11:
                StoryMode();
                break;

            default:
                CenterScreenObj.GetComponent<TextMeshPro>().text = " ";
                break;
        }
    }

    IEnumerator TurnTextOn(int index)
    {
        while (text)
            yield return 0;

        text = true;
        Text.text = " ";

        while (Text.text != TextList[index] && !finish)
        {
            yield return new WaitForSeconds(GlobalVariables.textDelay);

            if (Text.text.Length == TextList[index].Length)
                Text.text = TextList[index];

            else
                Text.text = Text.text.Insert(Text.text.Length - 1, TextList[index][Text.text.Length - 1].ToString());
        }

        Text.text = TextList[index];
        finish = false;
        text = false;
        WhatToRead++;
    }

    public void HandleTimeInput()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetFacebook()
    {
        FB.userName.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + CurrentScore.ToString();
        if (FacebookManager.Instance.ProfilePic != null)
            FB.facebookPic.GetComponent<Image>().sprite = FacebookManager.Instance.ProfilePic;
    }

    void StoryMode()
    {
        GlobalVariables.tutorial = false;
        SceneManager.LoadScene("FightVirus");
    }
}
