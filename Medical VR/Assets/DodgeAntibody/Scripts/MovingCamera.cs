using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovingCamera : MonoBehaviour
{
    public static float finalScore;

    public GameObject subtitles, cam;
    public float speed;
    public GameObject theScore, theLives, scoreBoard, UI, Username, ProfilePic;
    public float score = 0;
    public Color fogColor;
    public Vector3 originPos;
    public int lives = 3;
    float orgSpeed;
    public bool stopMoving = false, startSpeed = true;
    
   public void LoseresetPos()
    {
        if(GlobalVariables.arcadeMode)
        {
            lives--;
            theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        }
        //transform.position = originPos;

    }
    public void WinresetPos()
    {
        transform.position = originPos;
    }
    void ShowScore()
    {
        if(score > finalScore)
        {
            finalScore = score;
        }
        UI.SetActive(false);
        transform.position = originPos;
        speed = 0;
        scoreBoard.SetActive(true);
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 35);
        int tmp = (int)score;
        Username.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + tmp.ToString();
        ProfilePic.GetComponent<Image>().sprite = FacebookManager.Instance.ProfilePic;
        
    }

   public void RestartGame()
    {
        UI.SetActive(true);
        scoreBoard.SetActive(false);
        lives = 3;
        score = 0;
        speed = orgSpeed;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        //theScore.GetComponent<TextMesh>().text = "SCORE: " + tmp.ToString();
    }
    void Start ()
    {
        originPos = transform.position;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        orgSpeed = speed;
        if(GlobalVariables.arcadeMode == false && GlobalVariables.tutorial == true)
        {
            subtitles.SetActive(true);
            UI.SetActive(false);
            speed = 0;
        }
        if(GlobalVariables.arcadeMode == false)
        {
            UI.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
      
    }

    void FixedUpdate()
    {
        if(score >= 500)
        BannerScript.UnlockTrophy("Platelet");

        if (GlobalVariables.arcadeMode == false)
        {
            if(startSpeed == true)
            {
                if(subtitles.GetComponent<SubstitlesScript>().IsDone() == true)
                {
                    speed = orgSpeed;
                    startSpeed = false;
                }
            }
        }

        AvoidBack();
        if(lives < 1)
        {
            ShowScore();
        }

        else if(stopMoving == false)
        {
            transform.position +=cam.transform.forward * speed;
            if(GlobalVariables.arcadeMode == true)
            {
                score += Time.smoothDeltaTime;
                int tmp = (int)score;
                theScore.GetComponent<TMPro.TextMeshPro>().text = "SCORE: " + tmp.ToString();
            }
        }
        else if(speed < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos, Time.deltaTime * 5000);
        }
    }

    void AvoidBack()
    {
        if (transform.rotation.eulerAngles.y > 90 && transform.rotation.eulerAngles.y < 270)
        {
            stopMoving = true;
            //MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z -100);
        }

        else if( speed >=0)
            stopMoving = false; 
    }

    public void HandleTimeInput()
    {
        RestartGame();
    }
}
