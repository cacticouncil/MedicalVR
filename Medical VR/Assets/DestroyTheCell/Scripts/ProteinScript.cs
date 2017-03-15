using UnityEngine;
using System.Collections;
using System;

public class ProteinScript : MonoBehaviour, TimedInputHandler
{
    GameObject ProteinManager;
    GameObject Player;
    void Start()
    {
        ProteinManager = gameObject.transform.parent.gameObject;
        Player = ProteinManager.GetComponent<ProteinSpawn>().Player;
    }

    public void OnGazeEnter(bool enter)
    {
        if (enter)
            Player.GetComponent<VirusPlayer>().Speed = 0;
        
        else
            Player.GetComponent<VirusPlayer>().Speed = .01f;
    }

    public void HandleTimeInput()
    {
        Destroy(transform.gameObject);
    }

    void OnDestroy()
    {
        Player.GetComponent<VirusPlayer>().Speed = .01f;
        ProteinManager.GetComponent<ProteinSpawn>().ProteinList.Remove(transform.gameObject);
    }
}
