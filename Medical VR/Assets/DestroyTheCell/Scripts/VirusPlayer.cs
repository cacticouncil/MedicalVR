using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class VirusPlayer : MonoBehaviour
{
    //Variables for Tutorial
    public static bool TutorialMode = true;
    int WhatToRead = 1;
    bool CanIRead = true;
    public bool TutorialModeCompleted = false;

    //Variable for Arcade
    public static bool ArcadeMode = false;

    //Variables for Game
    public GameObject TimerText;
    public GameObject Spawn;
    public GameObject WaveManager;
    public GameObject BlackCurtain;

    public GameObject VirusAttack;
    public List<GameObject> VirusAttackList = new List<GameObject>();
    Vector3 SpawnVirusAttack;

    public float TimeLeft;
    public float PlayerSpeed;
    public int Lives;
    public bool isGameover = false;

    public GameObject Instructions;
    bool InstructionsDone = false;

    public bool WaveStarted = false;
    bool DelaySpawn = false;
    float DelayTimer = 0.0f;
    public bool CanIMove = false;
    float BeatGameTimer = 0.0f;
    void Start()
    {
        TimeLeft = 60.0f;
        //PlayerSpeed = 0.0f;
        PlayerSpeed = .02f;
        Lives = 3;

        if (TutorialMode == false)
        {
            StartCoroutine(DisplayText("Target the Cell Receptors" + "\n" + "Evade the Anti Viral Proteins", 3.0f));
            BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
        }
    }

    void Update()
    {
        //For arcade and story mode
        if (TutorialMode == false)
        {
            if (ArcadeMode == true)
            {
                TimeLeft -= Time.deltaTime;
                TimerText.GetComponent<TextMeshPro>().text = "Timer: " + TimeLeft.ToString("f0") + "           Lives: " + Lives.ToString();

                //Temporarily have the countdown stay at 0
                if (TimeLeft <= 0.0f)
                    TimeLeft = 0.0f;
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
                            StartCoroutine(DisplayText("Wave4", 2.3f));
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
                            FlashScreen();
                    }
                }
            }

            //Gameover
            if (ArcadeMode == true)
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

                    if (ArcadeMode == false)
                    {
                        float a = BlackCurtain.GetComponent<Renderer>().material.color.a;
                        if (a < 0)
                            a = 0;
                        BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a + (Time.deltaTime * 1.5f));
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
                    if (ArcadeMode == false)
                    {
                        TutorialModeCompleted = false;
                        VirusGameplayScript.loadCase = 3;
                        SceneManager.LoadScene("Virus Gameplay Scene");
                    }

                    //For arcade mode once you beat it will bring up scoreboard 
                    //else if (ArcadeMode == true)
                    //{
                    //
                    //}
                }
            }
        }

        else if (TutorialMode == true)
        {
            float a = BlackCurtain.GetComponent<Renderer>().material.color.a;
            BlackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a - (Time.deltaTime * 1.5f));

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
                        StartCoroutine(DisplayText("The objective is to kill the cell receptors", 3.5f));
                        break;

                    case 3:
                        StartCoroutine(DisplayText("Do this by finding them near the cell membrane", 3.5f));
                        break;

                    case 4:
                        StartCoroutine(DisplayText("And using your reticle to destroy them", 3.5f));
                        CanIMove = true;
                        PlayerSpeed = .02f;
                        WaveManager.GetComponent<WaveManager>().CanISpawnCellReceptor = true;
                        break;

                    case 5:
                        StartCoroutine(DisplayText("You will shoot attack viruses", 3.5f));
                        WaveManager.GetComponent<WaveManager>().CanDestroyProteins = true;
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
                        StartCoroutine(DisplayText("Or they will set you back to the spawn area", 3.5f));
                        WaveManager.GetComponent<WaveManager>().CanISpawnAntiViralProtein = true;
                        break;

                    case 9:
                        StartCoroutine(DisplayText("Now pratice targeting these cell receptors", 3.5f));
                        CanIMove = true;
                        PlayerSpeed = .02f;
                        WaveManager.GetComponent<WaveManager>().WaveNumber = 2;
                        WaveManager.GetComponent<WaveManager>().CanISpawnCellReceptor = true;
                        WaveManager.GetComponent<WaveManager>().CanISpawnAntiViralProtein = true;
                        break;

                    case 10:
                        StartCoroutine(DisplayText("Great now your ready to play", 3.5f));
                        TutorialModeCompleted = true;
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

        //For both tutorial and gameplay delete null virus attack
        for (int i = 0; i < VirusAttackList.Count; i++)
        {
            if (VirusAttackList[i].gameObject == null)
                VirusAttackList.Remove(VirusAttackList[i]);
        }

        //After you beat tutorial if story mode bool is active transtion to story mode for this game
        if (TutorialModeCompleted == true)
        {
            TutorialMode = false;
            ArcadeMode = false;
            SceneManager.LoadScene("DestroyTheCell");
        }

        //Otherwise just play tutorial once and leave to  main menu
        //if (TutorialModeCompleted == true)
        //{

        //}
    }

    void FixedUpdate()
    {
        if (TutorialMode == false)
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
                transform.position += transform.forward * PlayerSpeed;
                GetComponent<Rigidbody>().velocity *= PlayerSpeed;
            }
        }

        if (TutorialMode == true)
        {
            if (CanIMove == true)
            {
                transform.position += transform.forward * PlayerSpeed;
                GetComponent<Rigidbody>().velocity *= PlayerSpeed;
            }
        }
    }

    void FlashScreen()
    {

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
        if (WhatToRead == 6 && WaveManager.GetComponent<WaveManager>().CellReceptorsList.Count != 0)
            CanIRead = false;

        if (WhatToRead == 9 && WaveManager.GetComponent<WaveManager>().AntiViralProteinList.Count != 0)
            CanIRead = false;

        if (WhatToRead == 10 && WaveManager.GetComponent<WaveManager>().CellReceptorsList.Count != 0)
            CanIRead = false;
    }

    public void Respawn()
    {
        transform.position = Spawn.transform.position;
        DelaySpawn = true;
    }

    public void SpawnAttackViruses()
    {
        SpawnVirusAttack = transform.position;
        GameObject V = Instantiate(VirusAttack, SpawnVirusAttack, Quaternion.identity) as GameObject;
        V.GetComponent<AttackVirus>().MainCamera = this.gameObject;
        VirusAttackList.Add(V);
        V.GetComponent<AttackVirus>().enabled = true;
    }
}

