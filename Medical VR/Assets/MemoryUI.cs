using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using TMPro;


public class MemoryUI : MonoBehaviour
{
    float TutorialTimer = 0.0f;
    int WhatToRead = 0;
    float BeatGameTimer = 0.0f;
    public TextMeshPro CenterScreenObj;
    public GameObject EventSystem;

    public GameObject theScore, theLives, theLevels, scoreBoard, UI, Spheres, Timer, Capsules;
    public GameObject Username, ProfilePic;

    // Use this for initialization
    public float score = 0;
    public static float finalScore = 0;
    int lives = 5;
    public int Level = 1;
    public float startTime = 60.0f;
    public bool finnished = false;
    public float timeRemaining = 60.0f;

    private void Start()
    {
        if( GlobalVariables.tutorial== true)
        {
            Click();
        }
    }

    private string[] texts =
    {
        "Welcome to the DNA Memory Game",
        "Nucleic acids construct DNA pairs in very specific ways. Your objective is to select three capsules that are the correct pairing to the DNA code given above.",
        "The pairing is as follows: G binds to C, C binds to G, A binds to U, and T binds to A.",
        "Pair the correct ones and they will turn green, pair a wrong one and it will turn red. Make sure you don't get 5 wrong or you will have to restart.",
        //(After CLICKING ONE CORRECT CAPSULE)
        " Perfect \n Now get the others!",
        //(IF Arcade Mode)
        "Make sure you do it before the timer ends.",
        //(If 3 correct Capsules)
        "Awesome! Now your ready to play."
        //(end)
        };
    private bool last = false, text = false, finish = false;

    public void LoseresetPos()
    {
        lives--;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
    }

    void ShowScore()
    {
        if (score > finalScore)
            finalScore = score;
        if (finalScore > PlayerPrefs.GetFloat("MemoryScore"))
            PlayerPrefs.SetFloat("MemoryScore", finalScore);
        else
            finalScore = PlayerPrefs.GetFloat("MemoryScore");

        lives = 5;
        UI.SetActive(false);
        Capsules.SetActive(false);
        Spheres.GetComponent<Randomsphere>().Getclonei().SetActive(false);
        Spheres.GetComponent<Randomsphere>().Getclonej().SetActive(false);
        Spheres.GetComponent<Randomsphere>().Getclonek().SetActive(false);
        scoreBoard.SetActive(true);
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 35);
        Username.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + score.ToString();
        if (FacebookManager.Instance.ProfilePic != null)
            ProfilePic.GetComponent<Image>().sprite = FacebookManager.Instance.ProfilePic;
    }
    public void RestartGame()
    {
        finnished = false;
        UI.SetActive(true);
        scoreBoard.SetActive(false);
        lives = 5;
        score = 0;
        Level = 1;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        Spheres.GetComponent<Randomsphere>().Reset();
        startTime = 60.0f;
        timeRemaining = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;

        if (GlobalVariables.tutorial == false)
        {
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set up how turtorial will show players basic gameplay
        else if (GlobalVariables.tutorial == true)
        {

            if ((WhatToRead == 5 && Randomsphere.correct == 1) ||  (WhatToRead == 8 && Randomsphere.correct == 3))
                Click();

            bool held = Input.GetButton("Fire1");
            if (held && !last)
            {
                if (text)
                {
                    finish = true;
                }
                else
                {
                    if ((WhatToRead != 5 && WhatToRead != 8))
                        Click();
                }
            }
            last = held;

            
        }


        //AvoidBack();

        if (finnished)
            return;

        if (GlobalVariables.arcadeMode == true && GlobalVariables.tutorial == false)
        {
            if (!(Randomsphere.correct == 3))
                timeRemaining -= Time.deltaTime;

            int minutesint = ((int)timeRemaining / 60);
            if (minutesint < 0)
                minutesint = 0;
            string minutes = minutesint.ToString();
            string seconds = (timeRemaining % 60).ToString("f2");
            Timer.GetComponent<TMPro.TextMeshPro>().text = "Timer: " + minutes + ":" + seconds;
        }
        else
        {
            Timer.GetComponent<TMPro.TextMeshPro>().text = "";
        }
    }

    public void Finish()
    {
        finnished = true;
    }

    void FixedUpdate()
    {
        if (Level >= 20)
        {
            BannerScript.UnlockTrophy("RNA");
        }
        if (timeRemaining < 0)
        {
            BannerScript.UnlockTrophy("Ribosome");
        }
        if ((lives < 1 || timeRemaining < 0) && GlobalVariables.arcadeMode != false)
        {
            ShowScore();
        }
        else
        {
            if (lives < 1 || timeRemaining < 0)
            {
                GlobalVariables.arcadeMode = false;
                SceneManager.LoadScene("MemoryGame");
            }
            if (GlobalVariables.arcadeMode == true)
            {
                int tmp = (int)score;
                theScore.GetComponent<TMPro.TextMeshPro>().text = "SCORE: " + tmp.ToString();
            }
            else
            {
                Timer.GetComponent<TMPro.TextMeshPro>().text = "";
                theScore.GetComponent<TMPro.TextMeshPro>().text = "";
            }
            theLevels.GetComponent<TMPro.TextMeshPro>().text = "LEVEL: " + Level.ToString();
        }
    }

    void AvoidBack()
    {
        if (transform.rotation.eulerAngles.y > 90)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90, transform.rotation.eulerAngles.z);

        if (transform.rotation.eulerAngles.y < -90)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -90, transform.rotation.eulerAngles.z);
    }

    private void Click()
    {
        switch (WhatToRead)
        {

            case 0:
                {
                    EventSystem.SetActive(false);
                    SoundManager.PlayCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-001");
                    StartCoroutine(TurnTextOn(0));
                }
                break;
            case 1:
                {
                    if (SoundManager.IsCellVoicePlaying("Medical_VR_DNA_Minigame_Tutorial_Line-001"))
                        SoundManager.StopCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-001");
                    SoundManager.PlayCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-002");
                    StartCoroutine(TurnTextOn(1));
                }
                break;         
            case 2:
                {
                    if (SoundManager.IsCellVoicePlaying("Medical_VR_DNA_Minigame_Tutorial_Line-002"))
                        SoundManager.StopCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-002");
                    SoundManager.PlayCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-003");
                    StartCoroutine(TurnTextOn(2));
                }
                break;
            case 3:
                {
                    if (SoundManager.IsCellVoicePlaying("Medical_VR_DNA_Minigame_Tutorial_Line-003"))
                        SoundManager.StopCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-003");
                    SoundManager.PlayCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-004");
                    StartCoroutine(TurnTextOn(3));
                }
                break;
            case 4:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_DNA_Minigame_Tutorial_Line-004"))
                    SoundManager.StopCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-004");
                EventSystem.SetActive(true);
                CenterScreenObj.text = " ";
                break;
            case 5:            
                SoundManager.PlayCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-005");
                StartCoroutine(TurnTextOn(4));
                break;
            case 6:
                if (GlobalVariables.arcadeMode == true)
                {
                    if (SoundManager.IsCellVoicePlaying("Medical_VR_DNA_Minigame_Tutorial_Line-005"))
                        SoundManager.StopCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-005");
                    SoundManager.PlayCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-006");
                    StartCoroutine(TurnTextOn(5));
                }
                else { WhatToRead++; }
                break;
            case 7:
                if (SoundManager.IsCellVoicePlaying("Medical_VR_DNA_Minigame_Tutorial_Line-006"))
                    SoundManager.StopCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-006");
                CenterScreenObj.text = " ";
                break;
            case 8:
                SoundManager.PlayCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-007");
                StartCoroutine(TurnTextOn(6));
                break;
            default:
                CenterScreenObj.text = "";
                GlobalVariables.tutorial = false;
                if (GlobalVariables.arcadeMode == true)
                {
                    GlobalVariables.arcadeMode = true;
                    SceneManager.LoadScene("MemoryGame");

                }
                else
                {
                    GlobalVariables.arcadeMode = false;
                    SceneManager.LoadScene("MemoryGame");
                }
                break;
        }
        WhatToRead++;
    }



    #region Text
    IEnumerator TurnTextOn(int index)
    {
        while (text)
            yield return 0;

        text = true;
        CenterScreenObj.text = "_";

        while (CenterScreenObj.text != texts[index] && !finish)
        {
            yield return new WaitForSeconds(GlobalVariables.textDelay);

            if (CenterScreenObj.text.Length == texts[index].Length)
            {
                CenterScreenObj.text = texts[index];
            }
            else
            {
                CenterScreenObj.text = CenterScreenObj.text.Insert(CenterScreenObj.text.Length - 1, texts[index][CenterScreenObj.text.Length - 1].ToString());
            }
        }
        CenterScreenObj.text = texts[index];
        finish = false;
        text = false;
    }
    #endregion
}
