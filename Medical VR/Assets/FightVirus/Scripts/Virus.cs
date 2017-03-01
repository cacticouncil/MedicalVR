using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

enum VirusBehavior { LeaveCell = 0, AttackProtein = 1, LeaveCellFast = 2 }

public class Virus : MonoBehaviour, TimedInputHandler
{
    VirusBehavior VB;
    Vector3 RandomPos;

    GameObject VirusManager;
    GameObject CellManager;
    GameObject Cell;
    GameObject Player;

    public float speed;
    int RandomCell;

    void Start()
    {
        RandomPos = UnityEngine.Random.onUnitSphere;
        RandomPos *= 50.0f;

        int RandomBehavior = UnityEngine.Random.Range(0, 3);
        VB = (VirusBehavior)RandomBehavior;
        //VB = (VirusBehavior)1;

        VirusManager = GameObject.Find("VirusSpawn");
        CellManager = GameObject.Find("CellSpawn");
        Player = GameObject.Find("Main Camera");

        if (VB == VirusBehavior.AttackProtein)
        {
            if (CellManager.GetComponent<CellManager>().CellList.Count != 0)
            {
                RandomCell = UnityEngine.Random.Range(0, CellManager.GetComponent<CellManager>().CellList.Count);
                Cell = CellManager.GetComponent<CellManager>().CellList[RandomCell];
                CellManager.GetComponent<CellManager>().CellList.Remove(Cell);
            }
            else
            {
                VB = VirusBehavior.LeaveCell;
            }
        }
    }

    void Update()
    {
        switch (VB)
        {
            case VirusBehavior.LeaveCell:
                transform.position = Vector3.MoveTowards(transform.position, RandomPos, speed);
                break;

            case VirusBehavior.AttackProtein:
                transform.position = Vector3.MoveTowards(transform.position, Cell.transform.position, speed);
                break;

            case VirusBehavior.LeaveCellFast:
                transform.position = Vector3.MoveTowards(transform.position, RandomPos, speed + .02f);
                break;

            default:
                transform.position = Vector3.MoveTowards(transform.position, RandomPos, speed);
                break;
        }
    }

    void Duplicate()
    {
        VirusManager.GetComponent<EnemyManager>().VirusList.Add(Instantiate(VirusManager.GetComponent<EnemyManager>().VirusCube, transform.position, Quaternion.identity) as GameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Bullet")
        {
            Destroy(gameObject);
            Player.GetComponent<Player>().Score += 100;
        }

        else if (col.tag == "Cell")
        {
            Duplicate();
            Destroy(Cell);
            VB = VirusBehavior.LeaveCell;
        }
    }

    void OnDestroy()
    {
        //If cell is alive but virus dies then add it back to the list
        if (Cell && !Cell.GetComponent<Cell>().isDead)
        {
            CellManager.GetComponent<CellManager>().CellList.Add(Cell);
        }
    }

    public void HandleTimeInput()
    {
        throw new NotImplementedException();
    }
}
