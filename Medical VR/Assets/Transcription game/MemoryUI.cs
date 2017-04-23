using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using TMPro;


public class MemoryUI : MonoBehaviour {

    public static bool TutorialMode = true;
    float TutorialTimer = 0.0f;
    int WhatToRead = 0;
    float BeatGameTimer = 0.0f;
    public GameObject CenterScreenObj;


    public GameObject theScore, theLives, theLevels, scoreBoard, UI, Spheres, Timer, Capsules;
    public GameObject Username, ProfilePic;

    public int GlobalScore;
    // Use this for initialization
    public float score = 0;
    int lives = 3;
    public int Level = 1;
    public float startTime = 60.0f;
    public bool finnished = false;
    public float timeRemaining = 60.0f;
    public static bool arcadeMode = false;
    public FBscript hi;
    public void LoseresetPos()
    {
            lives--;
            theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
    }

    void ShowScore()
    {
        lives = 3;
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
        lives = 3;
        score = 0;
        Level = 0;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        Spheres.GetComponent<Randomsphere>().Reset();
        startTime = 60.0f;
        timeRemaining = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialMode == false)
        {
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set up how turtorial will show players basic gameplay
        else if (TutorialMode == true)
        {
            switch (WhatToRead)
            {
                case 0:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Welcome to the Transcription Memory Game";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 1:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Your objective is to " + "\n" + " Select three capsules that are the correct transcription to the code given above";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;
                case 2:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 6.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = " A Transcripts to U, G to C, C to G, and T to A." /*+ "\n" + " Select three capsules that are the correct transcription to the code given above"*/;
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 3:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Select the correct ones and they will turn green, " + "\n" + "select a wrong one and it will turn red.";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 4:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = " Make sure you don't get 5 wrong or you will have to restart";
                    else
                    {
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "";
                    }
                    if (Randomsphere.correct == 1)
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 5:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Perfect " + "\n" + " Now get the others!";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 6:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f && arcadeMode == true)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = " Make sure you do it before the timer ends";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 7:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = " Awesome ";
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
        if (WhatToRead >= 8 && (Randomsphere.correct == 3))
        {
            BeatGameTimer += Time.deltaTime;
            CenterScreenObj.GetComponent<TextMeshPro>().text = "Great now your ready to play";

            if (BeatGameTimer >= 3.5)
            {
                //if ()
                //{
                //Story mode verion will play after completing

                //FOR NOW IF YOU COMPLETE TUTORIAL PROCEED TO STORY MODE
                TutorialMode = false;
                arcadeMode = false;
                SceneManager.LoadScene("MemoryGame");
                //}

                //    else if ()
                //    {
                //        //Just play tutorial once and go back to main menu
                //    }
            }
        }




        AvoidBack();

        if (finnished)
            return;

        if (arcadeMode == true)
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

        if (lives < 1 || timeRemaining < 0)
        {
            ShowScore();
        }
        else
        {
            if (arcadeMode == true)
            {
                int tmp = (int)score;
                theScore.GetComponent<TMPro.TextMeshPro>().text = "SCORE: " + tmp.ToString();
            }
            else
            {
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
