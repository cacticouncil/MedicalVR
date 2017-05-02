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
    public GameObject CenterScreenObj;
    public GameObject EventSystem;

    public GameObject theScore, theLives, theLevels, scoreBoard, UI, Spheres, Timer, Capsules;
    public GameObject Username, ProfilePic;

    public int GlobalScore;
    // Use this for initialization
    public float score = 0;
    int lives = 5;
    public int Level = 1;
    public float startTime = 60.0f;
    public bool finnished = false;
    public float timeRemaining = 60.0f;


    //public void Start()
    //{
    //    GlobalVariables.arcadeMode = false;
    //}
    public void LoseresetPos()
    {
            lives--;
            theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
    }

    void ShowScore()
    {
        lives = 5;
        UI.SetActive(false);
        Capsules.SetActive(false);
        Spheres.GetComponent<Randomsphere>().Getclonei().SetActive(false);
        Spheres.GetComponent<Randomsphere>().Getclonej().SetActive(false);
        Spheres.GetComponent<Randomsphere>().Getclonek().SetActive(false);
        scoreBoard.SetActive(true);
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 35);
        //scoreBoard.GetComponent<ScoreBoardScript>().GenerateScore();
        Username.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + FacebookManager.Instance.GlobalScore;
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

            switch (WhatToRead)
            {
                case 0:
                    TutorialTimer += Time.deltaTime;
                    EventSystem.SetActive(false);
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Welcome to the DNA Memory Game";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 1:
                    TutorialTimer += Time.deltaTime;
                    EventSystem.SetActive(false);
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Nucleic acids to comprise DNA pair in very specific ways";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 2:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 6.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Your objective is to select three capsules that are the correct pairing to the DNA code given above";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;
                case 3:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "The pairing is as follows:";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 4:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "A binds to U,";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 5:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "G binds to C,";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 6:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "C binds to G,";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 7:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "and T binds to A.";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 8:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Pair the correct ones and they will turn green,";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 9:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "pair a wrong one and it will turn red.";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 10:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Make sure you don't get 5 wrong or you will have to restart";
                    else
                    {
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "";
                        EventSystem.SetActive(true);
                    }
                    if (Randomsphere.correct == 1)
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 11:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = " Perfect " + "\n" + " Now get the others!";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 12:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f && GlobalVariables.arcadeMode == true)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Make sure you do it before the timer ends";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 13:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Awesome";
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
        if (WhatToRead >= 14 && (Randomsphere.correct == 3))
        {
            BeatGameTimer += Time.deltaTime;
            CenterScreenObj.GetComponent<TextMeshPro>().text = "Great now your ready to play";

            if (BeatGameTimer >= 3.5)
            {
                //if ()
                //{
                //Story mode verion will play after completing

                //FOR NOW IF YOU COMPLETE TUTORIAL PROCEED TO STORY MODE
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
            }
        }




        AvoidBack();

        if (finnished)
            return;

        if (GlobalVariables.arcadeMode == true && GlobalVariables.tutorial == false)
        {
            if(!(Randomsphere.correct == 3))
            timeRemaining -= Time.deltaTime;

            string minutes = ((int)timeRemaining / 60).ToString();
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

        if ((lives < 1 || timeRemaining < 0) && GlobalVariables.arcadeMode != false)
        {
            ShowScore();
        }
        else
        {
            if(lives < 1 || timeRemaining < 0)
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
}
