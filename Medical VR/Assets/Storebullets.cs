using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class Storebullets : MonoBehaviour
{
    float TutorialTimer = 0.0f;
    int WhatToRead = 0;
    float BeatGameTimer = 0.0f;
    public static int amount;
    public static int stingamount;


    public GameObject EventSystem;
    public GameObject CGAMPspawnSystem;
    public GameObject StingspawnSystem;

    public static int bulletamount;
    public static int numberofstingsdone;
    public static int neededstings = 5;
    public GameObject theScore, scoreBoard, UI, BulletAmount, TheLevel;
    public GameObject Username, ProfilePic;

    public GameObject theLives;
    public GameObject CenterScreenObj;
    public static float finalScore = 0;
    public float score = 0;
    public float ReturnScore() { return score; }
    public void AddToScore(float _score) { score += _score; }
    public static int lives = 3;
    int level = 1;

    public GameObject shotSpawn;

    public float tumble = 5;
    public float fireRate;
    public GameObject bullet;

    public bool finish;

    private float nextFire;
    public static void LoseresetPos()
    {
        if (GlobalVariables.arcadeMode == true)
        {
            lives--;
        }
        else
        {

        }
    }
    void ShowScore()
    {
        finish = true;
        if (score > finalScore)
            finalScore = score;
        if (finalScore > PlayerPrefs.GetFloat("CGampScore"))
            PlayerPrefs.SetFloat("CGampScore", finalScore);
        else
            finalScore = PlayerPrefs.GetFloat("CGampScore");

        UI.SetActive(false);
        scoreBoard.SetActive(true);
        CGAMPspawnSystem.GetComponent<SpawnCGamp>().enabled = false;
        StingspawnSystem.GetComponent<SpawnSting>().enabled = false;
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        lives = 3;
        Username.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + score.ToString();
        if (FacebookManager.Instance.ProfilePic != null)
            ProfilePic.GetComponent<Image>().sprite = FacebookManager.Instance.ProfilePic;
    }
    public void RestartGame()
    {
        UI.SetActive(true);
        scoreBoard.SetActive(false);
        lives = 3;
        level = 1;
        score = 0;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
    }
    void Start()
    {
       // BannerScript.LockTrophy("Endoplasmic reticulum");

        amount = 0;
        stingamount = 0;
        finish = false;
        score = 0;
        bulletamount = 0;
        BulletAmount.GetComponent<TMPro.TextMeshPro>().text = "CGamp: " + bulletamount;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        TheLevel.GetComponent<TMPro.TextMeshPro>().text = "LEVEL: " + level;
        if (GlobalVariables.arcadeMode == false)
        {
            TheLevel.SetActive(false);
            theScore.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                    if (TutorialTimer <= 2.18f)
                    {
                        if (SoundManager.IsCellVoicePlaying("Medical_VR_CGAMP_Snatcher_Tutorial_Line-001") == false)
                            SoundManager.PlayCellVoice("Medical_VR_CGAMP_Snatcher_Tutorial_Line-001");
                        
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Welcome to cGAMP Snatcher";
                    }
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 1:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 6.01f)
                    {
                        if (SoundManager.IsCellVoicePlaying("Medical_VR_CGAMP_Snatcher_Tutorial_Line-002") == false)
                            SoundManager.PlayCellVoice("Medical_VR_CGAMP_Snatcher_Tutorial_Line-002");
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Your objective is to grab cGAMP and direct them towards the STING molecules on the Endoplasmic Recticulum.";
                    }
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;
                case 2:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 3.0f)
                    {
                        if (SoundManager.IsCellVoicePlaying("Medical_VR_CGAMP_Snatcher_Tutorial_Line-003") == false)
                            SoundManager.PlayCellVoice("Medical_VR_CGAMP_Snatcher_Tutorial_Line-003");
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "This is how your cells communicate that a virus has been detected!";
                    }
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 3:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 5.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "If you look around there are cGAMP molecules in the area.";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 4:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Just look at them and to pick them up. Try to get 10";
                    else
                    {
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "";
                    }
                    EventSystem.SetActive(true);


                    if (bulletamount >= 10)
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 5:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                    {
                        if (SoundManager.IsCellVoicePlaying("Medical_VR_CGAMP_Snatcher_Tutorial_Line-004") == false)
                            SoundManager.PlayCellVoice("Medical_VR_CGAMP_Snatcher_Tutorial_Line-004");
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Perfect!" + "\n" + "Now help them reach the STING molecules by shooting them pressing the button.";
                    }
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 6:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Make sure they don't collide with other objects.";
                    else
                    {
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "";
                    }

                    if (numberofstingsdone >= 10)
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 7:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                    {
                        if (SoundManager.IsCellVoicePlaying("Medical_VR_CGAMP_Snatcher_Tutorial_Line-005") == false)
                            SoundManager.PlayCellVoice("Medical_VR_CGAMP_Snatcher_Tutorial_Line-005");
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Awesome!" + "\n" + "Remember that cGAMP can spawn behind you";
                    }

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
        if (WhatToRead >= 8)
        {
            BeatGameTimer += Time.deltaTime;
            if (SoundManager.IsCellVoicePlaying("Medical_VR_DNA_Minigame_Tutorial_Line-007") == false)
                SoundManager.PlayCellVoice("Medical_VR_DNA_Minigame_Tutorial_Line-007");
            CenterScreenObj.GetComponent<TextMeshPro>().text = "Awesome! Now your ready to play.";

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
                    SceneManager.LoadScene("CGampSnatcher");

                }
                else
                {
                    GlobalVariables.arcadeMode = false;
                    SceneManager.LoadScene("CGampSnatcher");
                }

                //}

                //    else if ()
                //    {
                //        //Just play tutorial once and go back to main menu
                //    }
            }
        }

        BulletAmount.GetComponent<TMPro.TextMeshPro>().text = "CGamp: " + Storebullets.bulletamount;
        bool bPressed = Input.GetButtonDown("Fire1");
        //     bool bHeld = Input.GetButton("Fire1");
        //     bool bUp = Input.GetButtonUp("Fire1");



        if (bPressed && Time.time > nextFire)
        {
            if (SoundManager.IsJordanPlaying("28860__junggle__btn050") == false)
                    SoundManager.PlayJordanVoice("28860__junggle__btn050");
            shootCGamp();
        }
    }
    void FixedUpdate()
    {

        if(score >= 1000 )
        {
            BannerScript.UnlockTrophy("Endoplasmic reticulum");
        }

        if (GlobalVariables.arcadeMode == false)
        {
            if (numberofstingsdone >= 20)
            {
                CellGameplayScript.loadCase = 2;
                SceneManager.LoadScene("CellGameplay");
            }
        }

        if (GlobalVariables.arcadeMode == true)
        {
            theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
            if (lives < 1)
            {
                ShowScore();
            }
            if (numberofstingsdone >= neededstings)
            {
                TheLevel.GetComponent<TMPro.TextMeshPro>().text = "Level: " + level;
                neededstings += 5;
            }
            int tmp = (int)score;
            theScore.GetComponent<TMPro.TextMeshPro>().text = "SCORE: " + tmp.ToString();
        }
        else
        {
            theScore.GetComponent<TMPro.TextMeshPro>().text = "";
            TheLevel.GetComponent<TMPro.TextMeshPro>().text = "";
            theLives.GetComponent<TMPro.TextMeshPro>().text = "";

        }
    }

    private void shootCGamp()
    {
        if (bullet)
        {
            if (bulletamount > 0)
            {
                nextFire = Time.time + fireRate;
                GameObject obj = Instantiate(bullet, shotSpawn.transform.position, shotSpawn.transform.rotation) as GameObject;
                obj.GetComponent<Rigidbody>().angularVelocity = UnityEngine.Random.insideUnitSphere * tumble;
                
                bulletamount -= 1;
                BulletAmount.GetComponent<TMPro.TextMeshPro>().text = "CGamp: " + bulletamount;
            }
        }
    }

}

