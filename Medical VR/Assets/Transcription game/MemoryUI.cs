using UnityEngine;
using System.Collections;

public class MemoryUI : MonoBehaviour {

    public GameObject theScore, theLives, theLevels, scoreBoard, UI, Spheres;
    // Use this for initialization
    public float score = 0;
    int lives = 3;
    public int Level = 1;
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
        UI.SetActive(true);
        scoreBoard.SetActive(false);
        lives = 3;
        score = 0;
        Level = 0;
        theLives.GetComponent<TextMesh>().text = "LIVES: " + lives;
        Spheres.GetComponent<Randomsphere>().Reset();
    }
    void Start()
    {
        theLives.GetComponent<TextMesh>().text = "LIVES: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        AvoidBack();
    }
    void FixedUpdate()
    {
        if (lives < 1)
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
