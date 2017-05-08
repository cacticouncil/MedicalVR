using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class Storebullets : MonoBehaviour
{
    float TutorialTimer = 0.0f;
    int WhatToRead = 0;
    float BeatGameTimer = 0.0f;

    public GameObject EventSystem;
    public GameObject CGAMPspawnSystem;
    public GameObject StingspawnSystem;

    public static int bulletamount;
    public static int numberofstingsdone;
    public static int neededstings = 5;
    public GameObject theScore, scoreBoard, UI, BulletAmount, TheLevel;
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
        if (score > finalScore)
            finalScore = score;
        
        UI.SetActive(false);
        scoreBoard.SetActive(true);
        CGAMPspawnSystem.GetComponent<SpawnCGamp>().enabled = false;
        StingspawnSystem.GetComponent<SpawnSting>().enabled = false;
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 4);
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
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Welcome to cGAMP Snatcher";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 1:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Your objective is to grab cGAMP and guide them towards the STING molecules on the Endoplasmic Recticulum.";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;
                case 2:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "This is how your cells communicate that a virus has been detected!";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 3:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "If you look around there are cGAMPs all around.";
                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }
                    break;

                case 4:
                    TutorialTimer += Time.deltaTime;
                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Look at them and to grab them. Try to get 10";
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
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Perfect!" + "\n" + "Now help them reach the STING molecules by shooting them pressing the button.";
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
                    //BulletSpawn.GetComponent<BulletManager>().CanIShoot = true;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Awesome!";

                    else
                    {
                        TutorialTimer = 0.0f;
                        WhatToRead += 1;
                    }

                    break;

                case 8:
                    TutorialTimer += Time.deltaTime;

                    if (TutorialTimer <= 4.0f)
                        CenterScreenObj.GetComponent<TextMeshPro>().text = "Remember that cGAMP can spawn behind you";

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
        if (WhatToRead >= 9)
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
            shootCGamp();
        }
    }
    void FixedUpdate()
    {

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

