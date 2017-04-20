using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class MemoryUI : MonoBehaviour {

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
