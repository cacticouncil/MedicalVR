using UnityEngine;
using System.Collections;
using System;

public class MovingCamera : MonoBehaviour, TimedInputHandler
{

    public GameObject redCell;
    public float speed;
    public GameObject theScore, theLives, scoreBoard, UI;
    // Use this for initialization
    float score = 0;
    Vector3 originPos, redOrPos;
    int lives = 3;
    float orgSpeed; 
   public void LoseresetPos()
    {
        lives--;
        theLives.GetComponent<TextMesh>().text = "LIVES: " + lives;
        transform.position = originPos;
        
    }
    public void WinresetPos()
    {
        transform.position = originPos;
    }
    void ShowScore()
    {
        UI.SetActive(false);
        transform.position = originPos;
        speed = 0;
        scoreBoard.SetActive(true);
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 35);
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
    }
	
	// Update is called once per frame
	void Update ()
    {
      
    }
    void FixedUpdate()
    {
        if(lives < 1)
        {
            ShowScore();
        }
        else
        {
            transform.position += transform.forward * speed;
            score += Time.smoothDeltaTime;
            int tmp = (int)score;
            theScore.GetComponent<TextMesh>().text = "SCORE: " + tmp.ToString();
        }
       
    }

    void AvoidBack()
    {
 
        if (transform.rotation.eulerAngles.y > 90)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90, transform.rotation.eulerAngles.z);
      
        if (transform.rotation.eulerAngles.y < -90)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -90, transform.rotation.eulerAngles.z);
     
    }

    public void HandleTimeInput()
    {
        RestartGame();
    }
}
