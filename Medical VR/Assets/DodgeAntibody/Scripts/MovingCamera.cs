using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovingCamera : MonoBehaviour, TimedInputHandler
{
    public GameObject subtitles;
    public float speed;
    public GameObject theScore, theLives, scoreBoard, UI, MenuButton, Username, ProfilePic;
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
        //MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100);

    }
    public void WinresetPos()
    {
        transform.position = originPos;
        //MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100);
    }
    void ShowScore()
    {
        UI.SetActive(false);
        transform.position = originPos;
        //MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100);
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
        if(GlobalVariables.arcadeMode == false)
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
            transform.position += transform.forward * speed;
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
