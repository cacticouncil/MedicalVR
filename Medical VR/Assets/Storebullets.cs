using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class Storebullets : Tutorial {

    float TutorialTimer = 0.0f;
    int WhatToRead = 0;
    float BeatGameTimer = 0.0f;

    public GameObject EventSystem;
    public static int bulletamount;
    public static int  numberofstingsdone;
    public static int neededstings = 5;
    public GameObject theScore, scoreBoard, UI, BulletAmount, TheLevel;
    public GameObject theLives;
    public GameObject CenterScreenObj;
    public static float score = 0;
    public static bool arcadeMode = /*true;*/ false;////////////////////////////////////////////////////////////////////////////
   public static int lives = 3;
    int level = 1;

    public GameObject shotSpawn;

    public float fireRate;
    public GameObject bullet;

    private float nextFire;
    public static void LoseresetPos()
    {
        if (arcadeMode == true)
        {
            lives--;
        }
        else
        {
            
        }
    }
    void ShowScore()
    {
        UI.SetActive(false);
        scoreBoard.SetActive(true);
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
        lives = 3;
    }
    public void RestartGame()
    {
        UI.SetActive(true);
        scoreBoard.SetActive(false);
        lives = 3;
        level = 1;
        score = 0;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        //theScore.GetComponent<TextMesh>().text = "SCORE: " + tmp.ToString();
    }
    void Start()
    {
        bulletamount = 0;
        BulletAmount.GetComponent<TMPro.TextMeshPro>().text = "CGamp: " + bulletamount;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        TheLevel.GetComponent<TMPro.TextMeshPro>().text = "LEVEL: " + level;
        if (arcadeMode == false)
        {
            TheLevel.SetActive(false);
            theScore.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial == false)
        {
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set up how turtorial will show players basic gameplay
        else if (tutorial == true)
        {
            switch (WhatToRead)
            {
                case 0:
                    TutorialTimer += Time.deltaTime;
                    EventSystem.SetActive(false);
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Welcome to CGamp Snatcher";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 1:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Your objective is to " + "\n" + "Gramb Cgamp and shoot them towards the stings in the Endoplasmic Recticulum";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 2:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  If you look around " + "\n" + "there are CGamps around.";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 3:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = " Look at them to grab them. Try to get 10";
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

                case 4:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Perfect " + "\n" +" Now help them reach the stings by shooting them pressing the button ";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 5:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = " Make sure they don't collide with other objects";
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

                case 6:
                    TutorialTimer += Time.deltaTime;
                    //BulletSpawn.GetComponent<BulletManager>().CanIShoot = true;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = " Awesome ";

                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }

                    break;

                case 7:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "  Remember that CGamp can spawn behind you";

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
            CenterScreenObj.GetComponent<TextMeshPro>().text = "Great now your ready to play";

            if (BeatGameTimer >= 3.5)
            {
                //if ()
                //{
                //Story mode verion will play after completing

                //FOR NOW IF YOU COMPLETE TUTORIAL PROCEED TO STORY MODE
                tutorial = false;
                arcadeMode = false;
                SceneManager.LoadScene("CGampSnatcher");
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
            shootCGamp();
        }
    }
    void FixedUpdate()
    {

        if (arcadeMode == false)
        {
            if (numberofstingsdone >= 20)
            {
                CellGameplayScript.loadCase = 2;
                SceneManager.LoadScene("Cell Gameplay");
            }
        }

            if (arcadeMode == true)
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
                Instantiate(bullet, shotSpawn.transform.position, shotSpawn.transform.rotation);
                bulletamount -= 1;
                BulletAmount.GetComponent<TMPro.TextMeshPro>().text = "CGamp: " + bulletamount;
            }
        }
    }

}

