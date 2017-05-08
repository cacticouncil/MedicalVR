using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class CGampMoveCamera : MonoBehaviour
{

    public GameObject subtitles;
    public float speed;
    public GameObject theScore, theLives, scoreBoard, UI, MenuButton, CGamp;
    // Use this for initialization
    public float score = 0;
    public Color fogColor;
    public static bool arcadeMode = true;
    Vector3 originPos;
    int lives = 3;
    float orgSpeed;
    bool stopMoving = false, startSpeed = true;

    public void LoseresetPos()
    {
        if (arcadeMode == true)
        {
            lives--;
            theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        }
        transform.position = originPos;
        MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100);
        CGamp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 200);

    }
    public void WinresetPos()
    {
        transform.position = originPos;
        MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100);
        CGamp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 200);
    }
    void ShowScore()
    {
        UI.SetActive(false);
        transform.position = originPos;
        MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100);
        CGamp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 200);
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
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        //theScore.GetComponent<TextMesh>().text = "SCORE: " + tmp.ToString();
    }
    void Start()
    {
        originPos = transform.position;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        orgSpeed = speed;
        if (arcadeMode == false)
        {
            subtitles.SetActive(true);
            UI.SetActive(false);
            speed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (arcadeMode == false)
        {
            if (startSpeed == true)
            {
                if (subtitles.GetComponent<SubstitlesScript>().IsDone() == true)
                {
                    speed = orgSpeed;
                    startSpeed = false;
                }
            }
        }
        AvoidBack();
        if (lives < 1)
        {
            ShowScore();
        }
        else if (stopMoving == false)
        {
            transform.position += transform.forward * speed; //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (arcadeMode == true)
            {
                score += Time.smoothDeltaTime;
                int tmp = (int)score;
                theScore.GetComponent<TMPro.TextMeshPro>().text = "SCORE: " + tmp.ToString();
            }

        }

    }

    void AvoidBack()
    {
        if (transform.rotation.eulerAngles.y > 90 && transform.rotation.eulerAngles.y < 270)
        {
            stopMoving = true;
            MenuButton.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100);
            CGamp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 200);
        }

        else
            stopMoving = false;
    }

    public void HandleTimeInput()
    {
        RestartGame();
    }
}