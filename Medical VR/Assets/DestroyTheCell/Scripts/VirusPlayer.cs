using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;

public class VirusPlayer : MonoBehaviour
{
    public static bool isArcade = true;
    public GameObject TimerText;
    public GameObject Spawn;
    public GameObject WaveManager;

    public GameObject VirusAttack;
    public List<GameObject> VirusAttackList = new List<GameObject>();
    Vector3 SpawnVirusAttack;

    public float TimeLeft;
    public float Speed;
    public int Lives;
    public bool isGameover = false;

    //public Image Screen;
    //Color OriginalColor;
    //Color FlashColor;

    public GameObject Instructions;
    bool InstructionsDone = false;

    public bool WaveStarted = false;
    //public GameObject FadeThisObject;
    //public Material Trans;
    //float FadeSpeed = 0.2f;
    //float Alpha = 0.0f;
    //float FadeDir = -1;
    void Start()
    {
        TimeLeft = 60.0f;
        Speed = 0.0f;
        Lives = 100;

        //OriginalColor = Screen.color;
        //FlashColor = new Color(1, 1, 1, .2f);

        StartCoroutine(DisplayText("Target the Proteins" + "\n" + "Evade the Anti Viral Proteins", 3.0f));
    }

    void Update()
    {
        if (isArcade == true)
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
                        StartCoroutine(DisplayText("Wave1", 2.5f));
                        break;

                    case 2:
                        StartCoroutine(DisplayText("Wave2", 2.5f));
                        break;

                    case 3:
                        StartCoroutine(DisplayText("Wave3", 2.5f));
                        break;

                    case 4:
                        StartCoroutine(DisplayText("Wave4", 2.5f));
                        break;

                    default:
                        break;
                }

                WaveManager.GetComponent<WaveManager>().WaveNumber += 1;
                WaveStarted = false;
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
                    //if (enemy.GetComponent<Antibody>().CheckFOV() == true)
                        //FlashScreen();
                        //StartCoroutine(FlashScreen());
                }
            }
        }

        if (isArcade == true)
        {
            //Gameover
            if (Lives == 0)
                isGameover = true;
        }

        if (isGameover == true)
            Speed = 0.0f;
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * Speed;
        GetComponent<Rigidbody>().velocity *= Speed;
    }

    void FlashScreen()
    {
        //Screen.color = FlashColor;
        //yield return new WaitForSeconds(.5f);
        //Screen.color = OriginalColor;

        //Alpha -= FadeDir * FadeSpeed * Time.deltaTime;
        //Alpha = Mathf.Clamp01(Alpha);
        //FadeThisObject.
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

    public void SpawnAttackViruses()
    {
        SpawnVirusAttack = transform.position;
        GameObject V = Instantiate(VirusAttack, SpawnVirusAttack, Quaternion.identity) as GameObject;
        V.GetComponent<LeaveCell>().MainCamera = this.gameObject;
        VirusAttackList.Add(V);
        V.GetComponent<LeaveCell>().enabled = true;
    }
}

