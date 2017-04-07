using UnityEngine;
using System.Collections;
using System;

public class Storebullets : MonoBehaviour {

    public static int bulletamount;
    public GameObject theScore, theLives, scoreBoard, UI;
    public float score = 0;
    public static bool arcadeMode = true;
    int lives = 3;

    public GameObject Camera;

    public float fireRate;
    public GameObject bullet;

    private float nextFire;
    public void LoseresetPos()
    {
        if (arcadeMode == true)
        {
            lives--;
            theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        }
    }
    void ShowScore()
    {
        UI.SetActive(false);
        scoreBoard.SetActive(true);
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 35);
        scoreBoard.GetComponent<ScoreBoardScript>().GenerateScore();
        lives = 3;
    }
    public void RestartGame()
    {
        UI.SetActive(true);
        scoreBoard.SetActive(false);
        lives = 3;
        score = 0;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        //theScore.GetComponent<TextMesh>().text = "SCORE: " + tmp.ToString();
    }
    void Start()
    {
        bulletamount = 0;

        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        if (arcadeMode == false)
        {
            UI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        gameObject.transform.rotation = Camera.transform.rotation;

        if (arcadeMode == false)
        {

        }

        if (lives < 1)
        {
            ShowScore();
        }

        if (arcadeMode == true)
        {
            score += Time.smoothDeltaTime;
            int tmp = (int)score;
            theScore.GetComponent<TMPro.TextMeshPro>().text = "SCORE: " + tmp.ToString();
        }

    }
   
    private void shootCGamp()
    {
        if (bullet)
        {
            if (bulletamount > 0)
            {
                nextFire = Time.time + fireRate;
                Instantiate(bullet, Camera.transform.position, Camera.transform.rotation);
                bulletamount -= 1;
            }
        }
    }

}

