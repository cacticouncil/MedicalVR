using UnityEngine;
using System.Collections;
using System;

public class CellReceptors : MonoBehaviour
{
    GameObject Player;
    GameObject WaveManager;
    public GameObject VirusAttack;

    public bool AmITargeted = false;
    private bool NeverTargetAgain = false;

    public int Health = 4;
    private float SpeedForTutorial = .01f;
    public float TutorialCellReceptorTimer = 0.0f;
    bool DontCheckAgain = false;
    Vector3 SavedLocation;
    void Start()
    {
        WaveManager = gameObject.transform.parent.gameObject;
        Player = WaveManager.GetComponent<WaveManager>().Player;

        if (!GlobalVariables.tutorial == true && WaveManager.GetComponent<WaveManager>().WaveNumber != 1)
            GetComponent<Rigidbody>().AddForce(new Vector3(100, 100, 100));

        SavedLocation = WaveManager.GetComponent<WaveManager>().TutorialLocationEnd.transform.position;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && AmITargeted == true && NeverTargetAgain == false)
        {
            NeverTargetAgain = true;
            Player.GetComponent<VirusPlayer>().currSpeed = Player.GetComponent<VirusPlayer>().baseSpeed;
            StartCoroutine(FireAttackVirus(4, .3f));
        }

        if (Player.GetComponent<VirusPlayer>().isGameover)
            Destroy(this.gameObject);

        if (GlobalVariables.tutorial == true && WaveManager.GetComponent<WaveManager>().WaveNumber == 1 && DontCheckAgain == false)
        {
            AmITargeted = false;

            TutorialCellReceptorTimer += Time.deltaTime;

            if (Player.GetComponent<VirusPlayer>().IsCellDoneIdling == false)
            {
                if (TutorialCellReceptorTimer >= 3.5f)
                {
                    Player.GetComponent<VirusPlayer>().IsCellDoneIdling = true;
                    TutorialCellReceptorTimer = 0;
                }
            }

            else if (Player.GetComponent<VirusPlayer>().IsCellDoneIdling == true && Player.GetComponent<VirusPlayer>().WhatToRead == 4)
            {
                if (TutorialCellReceptorTimer <= 6.5f)
                {
                    transform.LookAt(Player.GetComponent<VirusPlayer>().transform.position);
                    transform.position -= transform.forward * SpeedForTutorial;
                }

                if (TutorialCellReceptorTimer >= 6.5f)
                {
                    Player.GetComponent<VirusPlayer>().IsCellDoneMoving = true;
                    DontCheckAgain = true;
                }
            }
        }
    }


    public void OnGazeEnter()
    {
        if (AmITargeted == false && NeverTargetAgain == false && Player.GetComponent<VirusPlayer>().IsCellDoneIdling == true)
        {
            Player.GetComponent<VirusPlayer>().currSpeed = 0;
            AmITargeted = true;
        }
    }

    public void OnGazeExit()
    {
        Player.GetComponent<VirusPlayer>().currSpeed = Player.GetComponent<VirusPlayer>().baseSpeed;
        AmITargeted = false;
    }

    public void SpawnAttackVirus()
    {
        GameObject V = Instantiate(VirusAttack, Player.transform.position, Quaternion.identity) as GameObject;
        V.GetComponent<AttackVirus>().MainCamera = Player;
        V.GetComponent<AttackVirus>().target = gameObject;
        V.GetComponent<AttackVirus>().enabled = true;
    }

    IEnumerator FireAttackVirus(int num, float duration)
    {
        for (int i = 0; i < num; i++)
        {
            SpawnAttackVirus();
            yield return new WaitForSeconds(duration);
        }
    }

    void OnDestroy()
    {
        WaveManager.GetComponent<WaveManager>().CellReceptorCount -= 1;
        WaveManager.GetComponent<WaveManager>().CellReceptorsList.Remove(transform.gameObject);
    }
}

