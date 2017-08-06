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
    public float orgSpeed;
    public GameObject theScore, theLives, scoreBoard, UI, Username, ProfilePic, facingTarget;
    public float score = 0;
    public Color fogColor;
    public Vector3 orgPos;
    public int lives = 3;
    public bool stopMoving = false, startSpeed = true, resetting = false;

    private float vComparer = .01f;
    public DodgeAntiBodyTutorial DABT; 
    void Start()
    {
        orgPos = transform.position;
        theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
        orgSpeed = speed;
        if(GlobalVariables.tutorial == true)
        {
            gameObject.GetComponent<LookCamera>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<LookCamera>().target = facingTarget.transform;
        }
        gameObject.GetComponent<LookCamera>().enabled = true;
        if (GlobalVariables.arcadeMode == false && GlobalVariables.tutorial == true)
        {
            
            subtitles.SetActive(true);
            UI.transform.parent = null;
            UI.SetActive(false);
            UI.transform.position = new Vector3(10000, 10000, 10000);
            speed = 0;
        }

        if (GlobalVariables.arcadeMode == false)
        {
            UI.transform.parent = null;
            UI.SetActive(false);
            UI.transform.position = new Vector3(10000, 10000, 10000);
        }
    }

    public void LoseResetPos()
    {
        if (GlobalVariables.arcadeMode)
        {
            lives--;
            theLives.GetComponent<TMPro.TextMeshPro>().text = "LIVES: " + lives;
            GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(LoseReset(orgPos));
        }
        //transform.position = originPos;
    }

    public IEnumerator LoseReset(Vector3 pos)
    {
        while (!V3Equal(transform.position, pos))
        {
            speed = -10;
            yield return 0;
        }

        if (DABT.MoveText != 7)
        {
            speed = orgSpeed;
            GetComponent<SphereCollider>().enabled = true;
        }
    }

    public void WinResetPos()
    {
        transform.position = orgPos;
    }

    void ShowScore()
    {
        if ((int)score > finalScore)
        {
            finalScore = (int)score;
        }

        if (finalScore > PlayerPrefs.GetFloat("DodgeScore"))
            PlayerPrefs.SetFloat("DodgeScore", finalScore);
        else
            finalScore = PlayerPrefs.GetFloat("DodgeScore");

        UI.SetActive(false);
        transform.position = orgPos;
        speed = 0;
        scoreBoard.SetActive(true);
        scoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5f);
        int tmp = (int)score;
        Username.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + tmp.ToString();
        if (FacebookManager.Instance.ProfilePic != null)
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

    void FixedUpdate()
    {
        if (score >= 500)
        {
            BannerScript.UnlockTrophy("Platelet");
        }

        AvoidBack();

        if (lives < 1)
        {
            ShowScore();
        }

        else if (speed < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, orgPos, Time.fixedDeltaTime * -speed);
        }

        else if (!stopMoving)
        {
            transform.position += cam.transform.forward * speed * Time.fixedDeltaTime;
            if (GlobalVariables.arcadeMode == true)
            {
                score += Time.smoothDeltaTime;
                theScore.GetComponent<TMPro.TextMeshPro>().text = "SCORE: " + ((int)score).ToString();
            }
        }
    }

    void AvoidBack()
    {
        if (cam.transform.rotation.eulerAngles.y > 90 && cam.transform.rotation.eulerAngles.y < 270)
        {
            stopMoving = true;
        }

        else if (speed >= 0)
        {
            stopMoving = false;
        }
    }

    private bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < vComparer;
    }
}
