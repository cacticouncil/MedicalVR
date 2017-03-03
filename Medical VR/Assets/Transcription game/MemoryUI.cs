using UnityEngine;
using System.Collections;

public class MemoryUI : MonoBehaviour {

    public GameObject theScore, theLives, scoreBoard, UI;
    // Use this for initialization
    public float score = 0;
    int lives = 3;
    public void LoseresetPos()
    {
       
        lives--;
        theLives.GetComponent<TextMesh>().text = "LIVES: " + lives;

    }
    public void WinresetPos()
    {
    }
    void ShowScore()
    {
        UI.SetActive(false);
        scoreBoard.SetActive(true);
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 35);
    }
    public void RestartGame()
    {
        UI.SetActive(true);
        scoreBoard.SetActive(false);
        lives = 3;
        score = 0;
        theLives.GetComponent<TextMesh>().text = "LIVES: " + lives;
        //theScore.GetComponent<TextMesh>().text = "SCORE: " + tmp.ToString();
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
}
