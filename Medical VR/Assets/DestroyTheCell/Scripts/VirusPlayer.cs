using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class VirusPlayer : MonoBehaviour
{
    public GameObject TimerText;
    public GameObject Spawn;
    public GameObject WaveManager;

    public float TimeLeft;
    public float Speed;
    public int Lives;
    public bool isGameover = false;

    public Image Screen;
    Color OriginalColor;
    Color FlashColor;

    public GameObject Instructions;
    public float Timer = 0.0f;
    int WaveNum = 0;
    bool InstructionsDone = false;
    void Start()
    {
        TimeLeft = 60.0f;
        Speed = 0.0f;
        Lives = 100;

        OriginalColor = Screen.color;
        FlashColor = new Color(1, 1, 1, .2f);

        StartCoroutine(DisplayText("Target the Proteins" + "\n" + "Evade the Anti Viral Proteins", 3.0f));
    }


    void Update()
    {
        TimeLeft -= Time.deltaTime;
        TimerText.GetComponent<TextMeshPro>().text = "Timer: " + TimeLeft.ToString("f0");

        //Temporarily have the countdown stay at 0
        if (TimeLeft <= 0.0f)
            TimeLeft = 0.0f;

        //Display wave text
        if (InstructionsDone == true)
        {
            if (WaveNum != WaveManager.GetComponent<WaveManager>().WaveNumber)
            {
                switch (WaveManager.GetComponent<WaveManager>().WaveNumber)
                {
                    case 1:
                        StartCoroutine(DisplayText("Wave1", 3.0f));
                        break;
                    case 2:
                        StartCoroutine(DisplayText("Wave2", 3.0f));
                        break;
                    case 3:
                        StartCoroutine(DisplayText("Wave3", 3.0f));
                        break;
                    case 4:
                        StartCoroutine(DisplayText("Wave4", 3.0f));
                        break;
                    default:
                        break;
                }
                WaveNum = WaveManager.GetComponent<WaveManager>().WaveNumber;
            }
        }


        Collider[] AntibodiesCloseBy = Physics.OverlapSphere(transform.position, 5.0f);

        if (AntibodiesCloseBy.Length != 0)
        {
            foreach (Collider enemy in AntibodiesCloseBy)
            {
                //If there is an enemy close then check if player is in the field of view
                if (enemy.GetComponent<Antibody>())
                {
                    if (enemy.GetComponent<Antibody>().CheckFOV() == true)
                        StartCoroutine(FlashScreen());
                }
            }
        }

        if (Lives == 0)
        {
            //Gameover
            isGameover = true;
        }
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * Speed;
        GetComponent<Rigidbody>().velocity *= Speed;
    }

    IEnumerator FlashScreen()
    {
        Screen.color = FlashColor;
        yield return new WaitForSeconds(.5f);
        Screen.color = OriginalColor;
    }

    IEnumerator DisplayText(string message, float duration)
    {
        Instructions.GetComponent<TextMeshPro>().enabled = true;
        Instructions.GetComponent<TextMeshPro>().text = message;
        yield return new WaitForSeconds(duration);
        Instructions.GetComponent<TextMeshPro>().enabled = false;

        if (InstructionsDone == false)
            InstructionsDone = true;
    }

    public void Respawn()
    {
        transform.position = Spawn.transform.position;
    }
}

