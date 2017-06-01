using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    public GameObject PressToContinue;
    public GameObject EnemyManger;
    public BulletManager BulletSpawn;
    //public GameObject BlackCurtain;
    public GameObject ScoreBoard;
    public FacebookStuff FB;

    //Variables for game
    public static float BestScoreForFightVirus;
    public float CurrentScore = 0.0f;
    public int VirusLeaveCount = 0;

    public bool isGameOver;
    public bool DisplayWaveNumber = false;
    float BeatGameTimer = 0.0f;
    public bool BeatBoss = false;

    //For text
    bool StopInput = false;
    public int WhatToRead = 0;
    public TextMeshPro ScoreObj;
    public TextMeshPro VirusCount;
    public TextMeshPro Text;
    private string[] TextList = new string[8];
    private bool last = false, text = false, finish = false;

    public int IncrementKill;
    public int SeeValue;

    void Start()
    {
        BestScoreForFightVirus = PlayerPrefs.GetFloat("FightVirusScore");
        //BannerScript.LockTrophy("Virus Trophy");
        //BannerScript.LockTrophy("Virus Capsid");
        VirusKillCount = 0;

        isGameOver = false;
       // SetFacebook();

        if (GlobalVariables.tutorial == false)
        {
            //BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
            TextForArcade();
            PressToContinue.GetComponent<TextMeshPro>().text = null;
        }

        else
        {
            TextList[0] = "Welcome to Fight Virus.";
            TextList[1] = "Your objective is to prevent any viruses from leaving the cell.";
            TextList[2] = "If you look around there are four zones to protect.";
            TextList[3] = "Don't let any virus get to these zones.";
            TextList[4] = "If three of them meet up, they will create bigger viruses. Don't let any leave.";
            TextList[5] = "Click the button on the headset to destroy the viruses, press it again to stop.";
            TextList[6] = "Remember that viruses can spawn behind you.";
            TextList[7] = "Great now you're ready to play.";
            TextForTutorial();
        }
    }

    void Update()
    {
        SeeValue = PlayerPrefs.GetInt("VirusTotalCount");
        if (GlobalVariables.tutorial == false)
        {
            if (GlobalVariables.arcadeMode)
            {
                DisplayCountAndScore();

                //Checking if you completed a wave
                if (EnemyManger.GetComponent<VirusManager>().VirusList.Count == 0 && EnemyManger.GetComponent<VirusManager>().DoneSpawning == true)
                {
                    EnemyManger.GetComponent<VirusManager>().DoneSpawning = false;
                    TextForArcade();
                }

                //If you lose arcade bring up scorebaord
                if (VirusLeaveCount == 10)
                {
                    isGameOver = true;
                    ScoreBoard.SetActive(true);
                    Text.text = "You lose arcade mode";

                    if (CurrentScore > BestScoreForFightVirus)
                        BestScoreForFightVirus = CurrentScore;

                    if (BestScoreForFightVirus > PlayerPrefs.GetFloat("FightVirusScore"))
                        PlayerPrefs.SetFloat("FightVirusScore", BestScoreForFightVirus);
                    else
                        BestScoreForFightVirus = PlayerPrefs.GetFloat("FightVirusScore");

                    SetFacebook();

                    ScoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
                }

                //If you win arcade bring up score board
                if (EnemyManger.GetComponent<VirusManager>().WaveNumber >= 5 && EnemyManger.GetComponent<VirusManager>().VirusList.Count == 0 && VirusLeaveCount < 10)
                {
                    isGameOver = true;
                    ScoreBoard.SetActive(true);
                    Text.text = "You win arcade mode";

                    if (CurrentScore > BestScoreForFightVirus)
                        BestScoreForFightVirus = CurrentScore;

                    if (BestScoreForFightVirus > PlayerPrefs.GetFloat("FightVirusScore"))
                        PlayerPrefs.SetFloat("FightVirusScore", BestScoreForFightVirus);
                    else
                        BestScoreForFightVirus = PlayerPrefs.GetFloat("FightVirusScore");

                    SetFacebook();
                    ScoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);

                    if (VirusLeaveCount == 0 && BeatBoss == true)
                        BannerScript.UnlockTrophy("Virus Trophy");
                }

                //Check to see if player killed enough viruses
                if (IncrementKill > 0)
                {
                    VirusKillCount = IncrementKill;
                    IncrementKill = 0;
                }

                if (VirusKillCount == 50)
                    BannerScript.UnlockTrophy("Virus Capsid");
            }

            else
            {
                if (EnemyManger.GetComponent<VirusManager>().VirusList.Count == 0 && EnemyManger.GetComponent<VirusManager>().DoneSpawning == true)
                {
                    EnemyManger.GetComponent<VirusManager>().DoneSpawning = false;
                    TextForArcade();
                }

                //If you lose story mode keep making them play until they beat it
                if (VirusLeaveCount == 5)
                {
                    isGameOver = true;
                    Text.text = "You lose story mode";
                    SceneManager.LoadScene("FightVirus");
                }

                //If you win story mode continue to next scene and don't forget to fade out
                if (EnemyManger.GetComponent<VirusManager>().WaveNumber == 4 && EnemyManger.GetComponent<VirusManager>().VirusList.Count == 0)
                {
                    isGameOver = true;
                    Text.text = "You win story mode";
                    BeatGameTimer += Time.deltaTime;

                    //float a = BlackCurtain.GetComponent<Renderer>().material.color.a;
                    //if (a < 0)
                    //    a = 0;
                    //BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a + (Time.deltaTime * 1.5f));

                    if (BeatGameTimer >= 2.0f)
                    {
                        CellGameplayScript.loadCase = 4;
                        Set.SetAndEnterStatic(15);
                    }
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set up turtorial 
        else if (GlobalVariables.tutorial == true)
        {
            //Need to fade in
            //float a = BlackCurtain.GetComponent<Renderer>().material.color.a;
            //BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a - (Time.deltaTime * 1.5f));

            //Player input to skip text
            if (StopInput == false)
            {
                bool held = Input.GetButton("Fire1");
                if (held && !last)
                {
                    if (text)
                        finish = true;

                    else
                    {
                        PressToContinue.SetActive(false);
                        TextForTutorial();
                    }
                }
                last = held;
            }

            //Checking for events
            if (VirusLeaveCount == 1 && WhatToRead == 5)
            {
                StopInput = false;
                PressToContinue.SetActive(true);
                WhatToRead += 1;
            }

            if (EnemyManger.GetComponent<VirusManager>().VirusList.Count == 0 && EnemyManger.GetComponent<VirusManager>().WaveNumber == 3 && WhatToRead == 9)
            {
                StopInput = false;
                PressToContinue.SetActive(true);
                WhatToRead++;
            }
        }
    }

    void TextForArcade()
    {
        switch (WhatToRead)
        {
            case 0:
                StartCoroutine(DisplayText("Prevent the Virus from leaving the cell", 2.0f));
                break;

            case 1:
                StartCoroutine(DisplayText("Wave 1", 2.0f));
                break;

            case 2:
                StartCoroutine(DisplayText("Wave 2", 2.0f));
                break;

            case 3:
                StartCoroutine(DisplayText("Wave 3", 2.0f));
                break;

            case 4:
                StartCoroutine(DisplayText("Boss", 2.0f));
                break;

            default:
                break;
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
                BulletSpawn.CanIShoot = true;
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
                SoundManager.stopSFX("Fight Virus Tutorial/Medical_VR_Fight_Virus_Tutorial_Line-007");
                SoundManager.PlaySFX("Fight Virus Tutorial/Medical_VR_VO_Great_Now_Youre_Ready_to_Play");
                break;

            case 11:
                StoryMode();
                break;

            default:
                Text.text = " ";
                break;
        }
    }

    void DisplayCountAndScore()
    {
        if (isGameOver == false)
            VirusCount.GetComponent<TextMeshPro>().text = "VirusCount: " + VirusLeaveCount.ToString();

        ScoreObj.GetComponent<TextMeshPro>().text = "Score: " + CurrentScore.ToString();
    }

    IEnumerator DisplayText(string message, float duration)
    {
        Text.enabled = true;
        Text.text = message;
        yield return new WaitForSeconds(duration);
        Text.enabled = false;

        if (WhatToRead == 0)
        {
            WhatToRead++;
            TextForArcade();
        }

        else
        {
            EnemyManger.GetComponent<VirusManager>().CheckCount = true;
            WhatToRead++;
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
        PressToContinue.SetActive(true);
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
        GlobalVariables.arcadeMode = false;
        SceneManager.LoadScene("FightVirus");
    }

    public static int VirusKillCount
    {
        get
        {
            return PlayerPrefs.GetInt("VirusTotalCount");
        }

        set
        {
            PlayerPrefs.SetInt("VirusTotalCount", 0);
        }
    }
}
