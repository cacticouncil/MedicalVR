using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Storebullets : MonoBehaviour {

    public static int bulletamount;
    public static int  numberofstingsdone;
    public static int neededstings = 5;
    public GameObject theScore, scoreBoard, UI, BulletAmount, TheLevel;
    public GameObject theLives;
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
            lives--;
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
            if (numberofstingsdone >= 30)
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

