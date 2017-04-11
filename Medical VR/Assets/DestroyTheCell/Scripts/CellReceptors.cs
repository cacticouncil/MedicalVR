using UnityEngine;
using System.Collections;
using System;

public class CellReceptors : MonoBehaviour, TimedInputHandler
{
    GameObject WaveManager;
    GameObject Player;
    public bool AttackMe = false;
    bool SpawnFiveAttackVirus = false;
    float SpawnTimer;
    int Count = 5;
    void Start()
    {
        SpawnTimer = 0.0f;
        WaveManager = gameObject.transform.parent.gameObject;
        Player = WaveManager.GetComponent<WaveManager>().Player;
    }

    void Update()
    {
        if (SpawnFiveAttackVirus == true && Count > 0)
        {
            SpawnTimer += Time.deltaTime;

            if (SpawnTimer >= .2f)
            {
                SpawnTimer = 0.0f;
                Player.GetComponent<VirusPlayer>().SpawnAttackViruses();
                Count -= 1;
            }
        }

        if (Player.GetComponent<VirusPlayer>().isGameover)
            Destroy(this.gameObject);
    }

    public void OnGazeEnter()
    {
        Player.GetComponent<VirusPlayer>().PlayerSpeed = 0;
    }

    public void OnGazeExit()
    {
        Player.GetComponent<VirusPlayer>().PlayerSpeed = .02f;
    }

    public void HandleTimeInput()
    {
        if (WaveManager.GetComponent<WaveManager>().CanDestroyProteins == true)
        {
            SpawnFiveAttackVirus = true;
            AttackMe = true;
        }
    }

    void OnDestroy()
    {
        WaveManager.GetComponent<WaveManager>().CellReceptorCount -= 1;
        WaveManager.GetComponent<WaveManager>().CellReceptorsList.Remove(transform.gameObject);
    }
}
