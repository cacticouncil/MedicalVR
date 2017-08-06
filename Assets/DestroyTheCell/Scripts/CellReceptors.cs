using UnityEngine;
using System.Collections;
using System;

public class CellReceptors : MonoBehaviour
{
    public GameObject VirusAttack;
    public bool AmITargeted = false;
    public int Health = 4;
    public float TutorialCellReceptorTimer = 0.0f;


    private VirusPlayer Player;
    private WaveManager WaveManager;
    private bool NeverTargetAgain = false;
    private float SpeedForTutorial = .01f;
    bool DontCheckAgain = false;

    void Start()
    {
        WaveManager = gameObject.transform.parent.GetComponent<WaveManager>();
        Player = WaveManager.Player.GetComponent<VirusPlayer>();

        if (!GlobalVariables.tutorial == true && WaveManager.WaveNumber != 1)
            GetComponent<Rigidbody>().AddForce(new Vector3(100, 100, 100));
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && AmITargeted == true && NeverTargetAgain == false)
        {
            NeverTargetAgain = true;
            Player.currSpeed = Player.baseSpeed;
            StartCoroutine(FireAttackVirus(4, .3f));
        }

        if (Player.isGameover)
        {
            Destroy(gameObject);
        }

        if (GlobalVariables.tutorial == true && WaveManager.WaveNumber == 1 && DontCheckAgain == false)
        {
            AmITargeted = false;

            TutorialCellReceptorTimer += Time.deltaTime;

            if (Player.IsCellDoneIdling == false)
            {
                if (TutorialCellReceptorTimer >= 3.5f)
                {
                    Player.IsCellDoneIdling = true;
                    TutorialCellReceptorTimer = 0;
                }
            }

            else if (Player.IsCellDoneIdling == true && Player.WhatToRead == 4)
            {
                if (TutorialCellReceptorTimer <= 6.5f)
                {
                    transform.LookAt(Player.transform.position);
                    transform.position -= transform.forward * SpeedForTutorial;
                }

                if (TutorialCellReceptorTimer >= 6.5f)
                {
                    Player.IsCellDoneMoving = true;
                    DontCheckAgain = true;
                }
            }
        }
    }


    public void OnGazeEnter()
    {
        if (AmITargeted == false && NeverTargetAgain == false && Player.IsCellDoneIdling == true)
        {
            Player.currSpeed = 0;
            AmITargeted = true;
        }
    }

    public void OnGazeExit()
    {
        Player.currSpeed = Player.baseSpeed;
        AmITargeted = false;
    }

    public void SpawnAttackVirus()
    {
        AttackVirus V = Instantiate(VirusAttack, Player.transform.position, Quaternion.identity).GetComponent<AttackVirus>();
        V.Player = Player;
        V.target = this;
        V.enabled = true;
    }

    IEnumerator FireAttackVirus(int num, float duration)
    {
        for (int i = 0; i < num; i++)
        {
            SpawnAttackVirus();
            SoundManager.PlaySFX("Fight Virus Tutorial/sfx_sounds_interaction16");
            yield return new WaitForSeconds(duration);
        }
    }

    void OnDestroy()
    {
        if (GlobalVariables.tutorial == false)
            Player.IncrementKill++;

        WaveManager.CellReceptorCount -= 1;
        WaveManager.CellReceptorsList.Remove(transform.gameObject);
    }
}

