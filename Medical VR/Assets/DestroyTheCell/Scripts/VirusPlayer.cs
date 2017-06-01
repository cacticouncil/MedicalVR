using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;


public class VirusPlayer : MonoBehaviour
{
    public GameObject PressToContinue;
    public ProteinCollectorScript PCS;
    public GameObject Spawn;
    public GameObject WaveManager;
    public GameObject BlackCurtain;

    public GameObject ScoreBoard;
    public FacebookStuff FB;

    public GameObject VirusAttack;

    [System.NonSerialized]
    public float baseSpeed = 1;
    public float currSpeed;

    public int Lives;
    public bool isGameover = false;

    bool InstructionsDone = false;

    public bool CanIMove = false;
    public bool WaveStarted = false;
    public bool IsCellDoneIdling = false;
    public bool IsCellDoneMoving = false;
    bool DelaySpawn = false;
    float DelayTimer = 0.0f;
    float BeatGameTimer = 0.0f;

    public int CurrentScore = 0;
    public static float FinalScore;
    public static float BestScoreForDestroyCell;

    //Variables for Text
    public bool TutorialModeCompleted = false;
    bool StopInput = false;
    public int WhatToRead = 0;
    public TextMeshPro ScoreGameOjbect;
    public TextMeshPro LivesGameObject;
    public TextMeshPro Text;
    private string[] TextList = new string[9];
    private bool last = false, text = false, finish = false;


    public int IncrementKill;
    public int SeeValue;

    void Awake()
    {
        PCS.StartHazards();
    }

    void Start()
    {
        //BannerScript.LockTrophy("Protein");
        //BannerScript.LockTrophy("Receptor");
        CellReceptorKillCount = 0;

        currSpeed = baseSpeed;
        Lives = 3;

        if (GlobalVariables.tutorial == false)
        {
            StartCoroutine(DisplayText("Target the Cell Receptors" + "\n" + "Evade the Anti Viral Proteins", 3.0f));
            IsCellDoneIdling = true;
            BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
            PressToContinue.GetComponent<TextMeshPro>().text = null;
        }

        else
        {
            TextList[0] = "Welcome to Destroy the Cell.";
            TextList[1] = "Your objective is to find the cell receptors.";
            TextList[2] = "You can search for the cell receptors near the cell membrane.";
            TextList[3] = "Use your cursor to automatically fire viruses at them.";
            TextList[4] = "Good job!";
            TextList[5] = "You need to avoid anti viral proteins or they will set you back to the spawn area.";
            TextList[6] = "Now practice targeting these cell receptors.";
            TextList[7] = "Great! now you're ready to play.";
            TextForTutorial();
        }
    }

    void Update()
    {
        SeeValue = PlayerPrefs.GetInt("ReceptorTotalCount");

        //For arcade and story mode
        if (GlobalVariables.tutorial == false)
        {
            if (GlobalVariables.arcadeMode)
            {
                ScoreGameOjbect.text = "Score: " + CurrentScore.ToString();
                LivesGameObject.text = "Lives: " + Lives.ToString();
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
                        Respawn();

                        if (CurrentScore > FinalScore)
                            FinalScore = CurrentScore;

                        if (FinalScore > PlayerPrefs.GetFloat("DestroyCellScore"))
                            PlayerPrefs.SetFloat("DestroyCellScore", FinalScore);
                        else
                            FinalScore = PlayerPrefs.GetFloat("DestroyCellScore");

                        SetFacebook();

                        ScoreBoard.SetActive(true);
                        ScoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);

                        if (Lives == 3)
                            BannerScript.UnlockTrophy("Protein");
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
                        Respawn();
                        SetFacebook();
                        ScoreBoard.SetActive(true);

                        if (CurrentScore > BestScoreForDestroyCell)
                            BestScoreForDestroyCell = CurrentScore;

                        ScoreBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
                    }
                }
            }

            if (IncrementKill > 0)
            {
                CellReceptorKillCount = IncrementKill;
                IncrementKill = 0;
            }

            if (CellReceptorKillCount == 50)
                BannerScript.UnlockTrophy("Receptor");
        }

        else if (GlobalVariables.tutorial == true)
        {
            //Need to fade in 
            float a = BlackCurtain.GetComponent<Renderer>().material.color.a;
            BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a - (Time.deltaTime * 1.5f));

            //Player input to skip text
            if (StopInput == false)
            {
                bool held = Input.GetButton("Fire1");
                if (held && !last)
                {
                    if (text)
                        finish = true;

                    else
                    {
                        PressToContinue.SetActive(false);
                        TextForTutorial();
                    }
                }
                last = held;
            }

            //Checking for events
            if (WhatToRead == 2 && IsCellDoneIdling == true)
            {
                StopInput = false;
                PressToContinue.SetActive(true);
            }

            if (WhatToRead == 4 && IsCellDoneMoving == true)
            {
                StopInput = false;
                PressToContinue.SetActive(true);
            }

            if (WhatToRead == 5 && WaveManager.GetComponent<WaveManager>().CellReceptorsList.Count == 0)
            {
                StopInput = false;
                PressToContinue.SetActive(true);
            }

            if (WhatToRead == 7 && WaveManager.GetComponent<WaveManager>().AntiViralProteinList.Count == 0)
            {
                StopInput = false;
                PressToContinue.SetActive(true);
            }

            if (WhatToRead == 9 && WaveManager.GetComponent<WaveManager>().CellReceptorsList.Count == 0 && WaveManager.GetComponent<WaveManager>().DoneSpawningCellReceptors == true)
            {
                StopInput = false;
                PressToContinue.SetActive(true);
            }
        }
    }

    void TextForTutorial()
    {
        switch (WhatToRead)
        {
            case 0:
                StartCoroutine(TurnTextOn(0));
                SoundManager.PlaySFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-001");
                break;

            case 1:
                StartCoroutine(TurnTextOn(1));
                SoundManager.stopSFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-001");
                SoundManager.PlaySFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-002");
                WaveManager.GetComponent<WaveManager>().CanISpawnCellReceptor = true;
                StopInput = true;
                break;

            case 2:
                StartCoroutine(TurnTextOn(2));
                SoundManager.stopSFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-002");
                SoundManager.PlaySFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-003");
                break;

            case 3:
                StartCoroutine(TurnTextOn(3));
                SoundManager.stopSFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-003");
                SoundManager.PlaySFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-004");
                StopInput = true;
                break;

            case 4:
                Text.text = " ";
                CanIMove = true;
                currSpeed = baseSpeed;
                WhatToRead++;
                StopInput = true;
                break;

            case 5:
                StartCoroutine(TurnTextOn(4));
                SoundManager.stopSFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-004");
                SoundManager.PlaySFX("Destroy the Cell Tutorial/Medical_VR_VO_Good_Job-001");
                CanIMove = false;
                Respawn();
                break;

            case 6:
                StartCoroutine(TurnTextOn(5));
                SoundManager.stopSFX("Destroy the Cell Tutorial/Medical_VR_VO_Good_Job-001");
                SoundManager.PlaySFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-005");
                WaveManager.GetComponent<WaveManager>().CanISpawnAntiViralProtein = true;
                StopInput = true;
                break;

            case 7:
                StartCoroutine(TurnTextOn(6));
                SoundManager.stopSFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-005");
                SoundManager.PlaySFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-006");
                break;

            case 8:
              //  PressToContinue.GetComponent<TextMeshPro>().text = "";
                Text.text = " ";
                CanIMove = true;
                currSpeed = baseSpeed;
                WaveManager.GetComponent<WaveManager>().DoneSpawningCellReceptors = false;
                WaveManager.GetComponent<WaveManager>().WaveNumber = 2;
                WaveManager.GetComponent<WaveManager>().CanISpawnCellReceptor = true;
                WaveManager.GetComponent<WaveManager>().CanISpawnAntiViralProtein = true;
                WhatToRead++;
                StopInput = true;
                break;

            case 9:
               // PressToContinue.GetComponent<TextMeshPro>().text = "Press To Continue";
                StartCoroutine(TurnTextOn(7));
                SoundManager.stopSFX("Destroy the Cell Tutorial/Medical_VR_Destroy_the_Cell_Tutorial_Lines-006");
                SoundManager.PlaySFX("Fight Virus Tutorial/Medical_VR_VO_Great_Now_Youre_Ready_to_Play");
                break;

            case 10:
                PlayStory();
                break;

            default:
                break;
        }
    }

    void FixedUpdate()
    {
        if (GlobalVariables.tutorial == false)
        {
            if (DelaySpawn == true)
            {
                transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
                DelayTimer += Time.deltaTime;
                if (DelayTimer >= 2.5f)
                {
                    DelayTimer = 0.0f;
                    DelaySpawn = false;
                }
            }

            else if (DelaySpawn == false)
            {
                transform.parent.GetComponent<Rigidbody>().velocity = transform.forward * currSpeed;
            }
        }

        if (GlobalVariables.tutorial == true)
        {
            if (CanIMove == true)
            {
                transform.parent.GetComponent<Rigidbody>().velocity = transform.forward * currSpeed;
            }
            else
            {
                transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    IEnumerator DisplayText(string message, float duration)
    {
        Text.enabled = true;
        Text.text = message;
        yield return new WaitForSeconds(duration);
        Text.enabled = false;

        if (InstructionsDone == false)
            InstructionsDone = true;
    }

    IEnumerator TurnTextOn(int index)
    {
        while (text)
            yield return 0;

        text = true;
        Text.text = " ";

        while (Text.text != TextList[index] && !finish)
        {
            yield return new WaitForSeconds(GlobalVariables.textDelay);

            if (Text.text.Length == TextList[index].Length)
                Text.text = TextList[index];

            else
                Text.text = Text.text.Insert(Text.text.Length - 1, TextList[index][Text.text.Length - 1].ToString());
        }

        Text.text = TextList[index];
        finish = false;
        text = false;
        PressToContinue.SetActive(true);
        WhatToRead++;
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
        Set.SetAndEnterStatic(14);
    }

    public static int CellReceptorKillCount
    {
        get
        {
            return PlayerPrefs.GetInt("ReceptorTotalCount");
        }

        //set
        //{
        //    PlayerPrefs.SetInt("ReceptorTotalCount", PlayerPrefs.GetInt("ReceptorTotalCount") + value);
        //}

        set
        {
            PlayerPrefs.SetInt("ReceptorTotalCount", 0);
        }
    }
}

