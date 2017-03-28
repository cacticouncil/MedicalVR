using UnityEngine;
using System.Collections;
using System;

public class ProteinScript : MonoBehaviour, TimedInputHandler
{
    GameObject WaveManager;
    GameObject Player;

    void Start()
    {
        WaveManager = gameObject.transform.parent.gameObject;
        Player = WaveManager.GetComponent<WaveManager>().Player;
    }

    void Update()
    {
        if (Player.GetComponent<VirusPlayer>().isGameover)
            Destroy(this.gameObject);
    }

    public void OnGazeEnter()
    {
        Player.GetComponent<VirusPlayer>().Speed = 0;
    }

    public void OnGazeExit()
    {
        Player.GetComponent<VirusPlayer>().Speed = .01f;
    }

    public void HandleTimeInput()
    {
        Destroy(transform.gameObject);
    }

    void OnDestroy()
    {
        Player.GetComponent<VirusPlayer>().Speed = .01f;
        WaveManager.GetComponent<WaveManager>().ProteinList.Remove(transform.gameObject);
    }
}
