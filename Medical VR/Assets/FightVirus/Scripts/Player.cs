using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Player : MonoBehaviour, TimedInputHandler
{
    public float Score;
    public int VirusLeaveCount;
    float RuleTimer;
    float Wave1Timer;
    float Wave2Timer;
    float Wave3Timer;
    float Wave4Timer;
    bool DisplayRules;
    public bool isGameOver;
    public GameObject ScoreObj;
    public GameObject VirusCount;
    public GameObject CenterScreenObj;
    public GameObject EnemyManger;
    public GameObject ScoreBoard;


    public bool DisplayWaveNumber = false;
    void Start()
    {
        Score = 0.0f;
        VirusLeaveCount = 0;
        RuleTimer = 0.0f;
        Wave1Timer = 0.0f;
        Wave2Timer = 0.0f;
        Wave3Timer = 0.0f;
        Wave4Timer = 0.0f;
        DisplayRules = true;
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver == false)
        {
            ScoreObj.GetComponent<TextMeshPro>().text = "Score: " + Score.ToString();
            VirusCount.GetComponent<TextMeshPro>().text = "VirusCount: " + VirusLeaveCount.ToString();


            if (RuleTimer >= 4.0f && RuleTimer <= 5.0f)
            {
                RuleTimer = 10.0f;
                DisplayRules = false;
                EnemyManger.GetComponent<VirusManager>().CheckCount = true;
            }

            if (DisplayRules)
                CenterScreenObj.GetComponent<TextMeshPro>().text = "  Defeat the Virus " + "\n" + "from leaving the cell";
            else
                CenterScreenObj.GetComponent<TextMeshPro>().text = "";

            if (DisplayWaveNumber == true)
            {
                switch (EnemyManger.GetComponent<VirusManager>().WaveNumber)
                {
                    case 1:
                        if (Wave1Timer <= 2.0f)
                            CenterScreenObj.GetComponent<TextMeshPro>().text = "    Wave 1";

                        else
                            DisplayWaveNumber = false;
                        
                        Wave1Timer += Time.deltaTime;
                        break;

                    case 2:
                        if (Wave2Timer <= 2.0f)
                            CenterScreenObj.GetComponent<TextMeshPro>().text = "    Wave 2";

                        else
                            DisplayWaveNumber = false;

                        Wave2Timer += Time.deltaTime;
                        break;

                    case 3:
                        if (Wave3Timer <= 2.0f)
                            CenterScreenObj.GetComponent<TextMeshPro>().text = "    Wave 3";

                        else
                            DisplayWaveNumber = false;

                        Wave3Timer += Time.deltaTime;

                        break;

                    case 4:
                        if (Wave4Timer <= 2.0f)
                            CenterScreenObj.GetComponent<TextMeshPro>().text = "    Boss";

                        else
                            DisplayWaveNumber = false;

                        Wave4Timer += Time.deltaTime;

                        break;

                    default:
                        break;
                }
            }

            RuleTimer += Time.deltaTime;
        }

        if (VirusLeaveCount == 10)
        {
            isGameOver = true;
            //ScoreBoard.SetActive(true);
            //ScoreBoard.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z + 35);
            //ScoreBoard.GetComponent<ScoreBoardScript>().GenerateScore();
        }
    }

    public void HandleTimeInput()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
