using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;


public class VirusPlayer : MonoBehaviour
{
    //Variables for Tutorial
    public int WhatToRead = 1;
    public bool CanIRead = true;
    public bool TutorialModeCompleted = false;

    //Variables for Game
    public GameObject LivesGameobject;
    public GameObject Spawn;
    public GameObject WaveManager;
    public GameObject BlackCurtain;

    public GameObject ScoreBoard;
    public FacebookStuff FB;

    public GameObject VirusAttack;

    [System.NonSerialized]
    public float baseSpeed = .03f;
    public float currSpeed;

    public int Lives;
    public bool isGameover = false;

    public GameObject Instructions;
    bool InstructionsDone = false;

    public bool CanIMove = false;
    public bool WaveStarted = false;
    public bool IsCellDoneIdling = false;
    public bool IsCellDoneMoving = false;
    bool DelaySpawn = false;
    float DelayTimer = 0.0f;
    float BeatGameTimer = 0.0f;

    float CurrentScore = 0.0f;
    public static float FinalScore;
    public static float BestScoreForDestroyCell;
    void Start()
    {
        currSpeed = baseSpeed;
        Lives = 3;
        SetFacebook();

        if (GlobalVariables.tutorial == false)
        {
            StartCoroutine(DisplayText("Target the Cell Receptors" + "\n" + "Evade the Anti Viral Proteins", 3.0f));
            BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
        }
    }

    void Update()
    {
        //For arcade and story mode
        if (GlobalVariables.tutorial == false)
        {
            if (GlobalVariables.arcadeMode)
            {
                LivesGameobject.GetComponent<TextMeshPro>().text = "           Lives: " + Lives.ToString();
            }

            //Display wave text
            if (InstructionsDone == true)
            {
                if (WaveStarted == true)
                {
                    switch (WaveManager.GetComponent<WaveManager>().WaveNumber)
                    {
                        case 1:
                            StartCoroutine(DisplayText("Wave1", 2.3f));
                            break;

                        case 2:
                            StartCoroutine(DisplayText("Wave2", 2.3f));
                            break;

                        case 3:
                            StartCoroutine(DisplayText("Wave3", 2.3f));
                            break;

                        case 4:
                            StartCoroutine(DisplayText("Final Wave", 2.3f));
                            break;

                        default:
                            break;
                    }

                    WaveManager.GetComponent<WaveManager>().WaveNumber += 1;
                    WaveStarted = false;
                }
            }

            //Check which enemies are nearby
            Collider[] AntibodiesCloseBy = Physics.OverlapSphere(transform.position, 5.0f);

            if (AntibodiesCloseBy.Length != 0)
            {
                foreach (Collider enemy in AntibodiesCloseBy)
                {
                    //If there is an enemy close then check if player is in the field of view
                    if (enemy.GetComponent<AntiViralProtein>())
                    {
                        if (enemy.GetComponent<AntiViralProtein>().CheckFOV() == true)
                        {
                            //Alert the player
                        }
                    }
                }
            }

            //Gameover
            if (GlobalVariables.arcadeMode)
            {
                if (Lives == 0)
                    isGameover = true;
            }

            if (isGameover == true)
            {
                if (Lives != 0)
                {
                    BeatGameTimer += Time.deltaTime;
                    StartCoroutine(DisplayText("You Win", 2.0f));

                    if (!GlobalVariables.arcadeMode)
                    {
                        float a = BlackCurtain.GetComponent<Renderer>().material.color.a;
                        if (a < 0)
                            a = 0;
                        BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a + (Time.deltaTime * 1.5f));
                    }
                    else
                    {
                        if (CurrentScore > FinalScore)
                            FinalScore = CurrentScore;

                        if( FinalScore > PlayerPrefs.GetFloat("DestroyCellScore"))
                        PlayerPrefs.SetFloat("DestroyCellScore", FinalScore);
                        else
                            FinalScore = PlayerPrefs.GetFloat("DestroyCellScore");
                        

                        ScoreBoard.SetActive(true);
                        ScoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
                    }
                }

                else if (Lives == 0)
                {
                    BeatGameTimer += Time.deltaTime;
                    StartCoroutine(DisplayText("You Lose", 2.0f));
                }

                if (BeatGameTimer >= 2.0f)
                {
                    //For story mode once you beat it proceed to the next story mode
                    if (!GlobalVariables.arcadeMode)
                    {
                        TutorialModeCompleted = false;
                        ContinuePlayThrough();
                    }

                    //For arcade mode once you beat it will bring up scoreboard 
                    else
                    {
                        ScoreBoard.SetActive(true);

                        if (CurrentScore > BestScoreForDestroyCell)
                            BestScoreForDestroyCell = CurrentScore;
                        
                        ScoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
                    }
                }
            }
        }

        else if (GlobalVariables.tutorial == true)
        {
            float a = BlackCurtain.GetComponent<Renderer>().material.color.a;
            BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a - (Time.deltaTime * 1.5f));

            if (WhatToRead == 3 && IsCellDoneIdling == true)
                CanIRead = true;

            if (WhatToRead == 4 && IsCellDoneMoving == true)
                CanIRead = true;

            if (WhatToRead == 6 && WaveManager.GetComponent<WaveManager>().CellReceptorsList.Count == 0)
                CanIRead = true;

            if (WhatToRead == 9 && WaveManager.GetComponent<WaveManager>().AntiViralProteinList.Count == 0)
                CanIRead = true;

            if (WhatToRead == 10 && WaveManager.GetComponent<WaveManager>().CellReceptorsList.Count == 0)
                CanIRead = true;

            if (CanIRead == true)
            {
                switch (WhatToRead)
                {
                    case 1:
                        StartCoroutine(DisplayText("Welcome to Destroy the Cell", 3.5f));
                        break;

                    case 2:
                        StartCoroutine(DisplayText("Your objective is to find the cell receptors", 3.5f));
                        WaveManager.GetComponent<WaveManager>().CanISpawnCellReceptor = true;
                        break;

                    case 3:
                        StartCoroutine(DisplayText("You can search for the cell receptors near the cell membrane", 3.5f));
                        break;

                    case 4:
                        StartCoroutine(DisplayText("Use the button to automatically fire viruses at them", 3.5f));
                        CanIMove = true;
                        currSpeed = baseSpeed;
                        break;

                    case 5:
                        StartCoroutine(DisplayText(" ", 3.5f));
                        break;

                    case 6:
                        StartCoroutine(DisplayText("Good job", 3.0f));
                        CanIMove = false;
                        Respawn();
                        break;

                    case 7:
                        StartCoroutine(DisplayText("You need to avoid anti viral proteins", 3.5f));
                        break;

                    case 8:
                        StartCoroutine(DisplayText("or they will set you back to the spawn area", 3.5f));
                        WaveManager.GetComponent<WaveManager>().CanISpawnAntiViralProtein = true;
                        break;

                    case 9:
                        StartCoroutine(DisplayText("Now practice targeting these cell receptors", 3.5f));
                        CanIMove = true;
                        currSpeed = baseSpeed;
                        WaveManager.GetComponent<WaveManager>().WaveNumber = 2;
                        WaveManager.GetComponent<WaveManager>().CanISpawnCellReceptor = true;
                        WaveManager.GetComponent<WaveManager>().CanISpawnAntiViralProtein = true;
                        break;

                    case 10:
                        StartCoroutine(DisplayText("Great! now you're ready to play", 3.5f));
                        break;

                    default:
                        Instructions.GetComponent<TextMeshPro>().enabled = true;
                        Instructions.GetComponent<TextMeshPro>().text = " ";
                        break;
                }

                CanIRead = false;
                WhatToRead += 1;
            }
        }

        //After you beat tutorial play the story mode
        if (TutorialModeCompleted == true)
            PlayStory();
    }

    void FixedUpdate()
    {
        if (GlobalVariables.tutorial == false)
        {
            if (DelaySpawn == true)
            {
                DelayTimer += Time.deltaTime;
                if (DelayTimer >= 2.5f)
                {
                    DelayTimer = 0.0f;
                    DelaySpawn = false;
                }
            }

            else if (DelaySpawn == false)
            {
                transform.parent.position += transform.forward * currSpeed;
                transform.parent.GetComponent<Rigidbody>().velocity *= currSpeed;
            }
        }

        if (GlobalVariables.tutorial == true)
        {
            if (CanIMove == true)
            {
                transform.parent.position += transform.forward * currSpeed;
                GetComponent<Rigidbody>().velocity *= currSpeed;
            }
        }
    }

    IEnumerator DisplayText(string message, float duration)
    {
        Instructions.GetComponent<TextMeshPro>().enabled = true;
        Instructions.GetComponent<TextMeshPro>().text = message;
        yield return new WaitForSeconds(duration);
        Instructions.GetComponent<TextMeshPro>().enabled = false;

        if (InstructionsDone == false)
            InstructionsDone = true;

        //Events for tutorial
        CanIRead = true;
        if (WhatToRead == 3 && IsCellDoneIdling == false)
            CanIRead = false;

        if (WhatToRead == 4 && IsCellDoneMoving == false)
            CanIRead = false;

        if (WhatToRead == 6 && WaveManager.GetComponent<WaveManager>().CellReceptorsList.Count != 0)
            CanIRead = false;

        if (WhatToRead == 9 && WaveManager.GetComponent<WaveManager>().AntiViralProteinList.Count != 0)
            CanIRead = false;

        if (WhatToRead == 10 && WaveManager.GetComponent<WaveManager>().CellReceptorsList.Count != 0)
            CanIRead = false;

        if (WhatToRead == 11)
            TutorialModeCompleted = true;
    }

    public void Respawn()
    {
        transform.parent.position = Spawn.transform.position;
        DelaySpawn = true;
    }

    void SetFacebook()
    {
        FB.userName.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + CurrentScore.ToString(); /// + FacebookManager.Instance.GlobalScore /;
        if (FacebookManager.Instance.ProfilePic != null)
            FB.facebookPic.GetComponent<Image>().sprite = FacebookManager.Instance.ProfilePic;
    }

    void PlayStory()
    {
        GlobalVariables.tutorial = false;
        SceneManager.LoadScene("DestroyTheCell");
    }

    public void PlayArcade()
    {
        GlobalVariables.tutorial = false;
        GlobalVariables.arcadeMode = true;
        SceneManager.LoadScene("DestroyTheCell");
    }

    void ContinuePlayThrough()
    {
        VirusGameplayScript.loadCase = 3;
        SceneManager.LoadScene("VirusGameplay");
    }
}

