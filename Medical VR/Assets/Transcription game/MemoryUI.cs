using UnityEngine;
using System.Collections;

public class MemoryUI : MonoBehaviour {

    public GameObject theScore, theLives, theLevels, scoreBoard, UI, Spheres, Timer;
    // Use this for initialization
    public float score = 0;
    int lives = 3;
    public int Level = 1;
    public float startTime;
    public bool finnished = false;
    public float timeRemaining = 60.0f;
    public void LoseresetPos()
    {
       
        lives--;
        theLives.GetComponent<TextMesh>().text = "LIVES: " + lives;
    }

    void ShowScore()
    {
        lives = 3;
        UI.SetActive(false);
        scoreBoard.SetActive(true);
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 35);
        scoreBoard.GetComponent<ScoreBoardScript>().GenerateScore();

    }
    public void RestartGame()
    {
        finnished = false;
        UI.SetActive(true);
        scoreBoard.SetActive(false);
        lives = 3;
        score = 0;
        Level = 0;
        timeRemaining = 60.0f;
        theLives.GetComponent<TextMesh>().text = "LIVES: " + lives;
        Spheres.GetComponent<Randomsphere>().Reset();
    }

    // Update is called once per frame
    void Update()
    {
        AvoidBack();

        if (finnished)
            return;

         timeRemaining -= Time.deltaTime;
        string minutes = ((int)timeRemaining / 60).ToString();
        string seconds = (timeRemaining % 60).ToString("f2");
        Timer.GetComponent<TextMesh>().text = "Timer: " + minutes + ":" + seconds;

        
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
            //score += Time.smoothDeltaTime;
            int tmp = (int)score;
            theScore.GetComponent<TextMesh>().text = "SCORE: " + tmp.ToString();
            theLevels.GetComponent<TextMesh>().text = "LEVEL: " + Level.ToString();
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
