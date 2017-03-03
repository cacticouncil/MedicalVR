using UnityEngine;
using System.Collections;
using System;

public class MovingCamera : MonoBehaviour, TimedInputHandler
{

    public GameObject redCell;
    public float speed;
    public GameObject theScore, theLives, scoreBoard, UI, MenuButton;
    // Use this for initialization
    public float score = 0;
    public Color fogColor;
    Vector3 originPos, redOrPos;
    int lives = 3;
    float orgSpeed;
    bool stopMoving = false;
    
   public void LoseresetPos()
    {
        lives--;
        theLives.GetComponent<TextMesh>().text = "LIVES: " + lives;
        transform.position = originPos;
        MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100);

    }
    public void WinresetPos()
    {
        transform.position = originPos;
        MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100);
    }
    void ShowScore()
    {
        UI.SetActive(false);
        transform.position = originPos;
        MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100);
        speed = 0;
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
        speed = orgSpeed;
        theLives.GetComponent<TextMesh>().text = "LIVES: " + lives;
        //theScore.GetComponent<TextMesh>().text = "SCORE: " + tmp.ToString();
    }
    void Start ()
    {
        theLives.GetComponent<TextMesh>().text = "LIVES: " + lives;
        originPos = transform.position;
        redOrPos = redCell.transform.position;
        orgSpeed = speed;
        RenderSettings.fog = true;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = 0.001f;
    }
	
	// Update is called once per frame
	void Update ()
    {
      
    }
    void FixedUpdate()
    {
        AvoidBack();
        if(lives < 1)
        {
            ShowScore();
        }
        else if(stopMoving == false)
        {
            transform.position += transform.forward * speed;
            score += Time.smoothDeltaTime;
            int tmp = (int)score;
            theScore.GetComponent<TextMesh>().text = "SCORE: " + tmp.ToString();
        }
       
    }

    void AvoidBack()
    {
        if (transform.rotation.eulerAngles.y > 90 && transform.rotation.eulerAngles.y < 270)
        {
            stopMoving = true;
            MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z -100);
        }

        else
            stopMoving = false; 
    }

    public void HandleTimeInput()
    {
        RestartGame();
    }
}
