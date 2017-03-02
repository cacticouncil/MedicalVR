using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//enum VirusBehavior { LeaveCell = 0, AttackProtein = 1, LeaveCellFast = 2 }

public class Virus : MonoBehaviour, TimedInputHandler
{
    //VirusBehavior VB;
    //Vector3 RandomPos;
    //GameObject Cell;

    GameObject VirusManager;
    GameObject Player;
    GameObject GoTo;

    public float speed;
    int RandomVirusLocation;

    void Start()
    {
        VirusManager = GameObject.Find("VirusSpawn");
        Player = GameObject.Find("Main Camera");
        GoTo = GameObject.Find("VirusLocations");

        //RandomPos = UnityEngine.Random.onUnitSphere;
        //RandomPos *= 50.0f;


        //int RandomBehavior = UnityEngine.Random.Range(0, 3);
        //VB = (VirusBehavior)RandomBehavior;
        //VB = (VirusBehavior)0;

        //if (VB == VirusBehavior.AttackProtein)
        //{
        //    if (CellManager.GetComponent<CellManager>().CellList.Count != 0)
        //    {
        //        RandomCell = UnityEngine.Random.Range(0, CellManager.GetComponent<CellManager>().CellList.Count);
        //        Cell = CellManager.GetComponent<CellManager>().CellList[RandomCell];
        //        CellManager.GetComponent<CellManager>().CellList.Remove(Cell);
        //    }
        //    else
        //    {
        //        VB = VirusBehavior.LeaveCell;
        //    }
        //}

        RandomVirusLocation = UnityEngine.Random.Range(0, 0);
    }

    void Update()
    {
        //switch (VB)
        //{
        //    case VirusBehavior.LeaveCell:
        //        transform.position = Vector3.MoveTowards(transform.position, RandomPos, speed);
        //        break;

        //    case VirusBehavior.AttackProtein:
        //        transform.position = Vector3.MoveTowards(transform.position, Cell.transform.position, speed);
        //        break;

        //    case VirusBehavior.LeaveCellFast:
        //        transform.position = Vector3.MoveTowards(transform.position, RandomPos, speed + .02f);
        //        break;

        //    default:
        //        transform.position = Vector3.MoveTowards(transform.position, RandomPos, speed);
        //        break;
        //}

        //GameObject GoToVirus = null;
        //float ShortestDistance = float.MaxValue;
        //foreach (GameObject virus in VirusManager.GetComponent<EnemyManager>().VirusList)
        //{
        //    float Distance = Vector3.Distance(virus.transform.position, transform.position);
        //    if (Distance < ShortestDistance)
        //    {
        //        GoToVirus = virus;
        //        ShortestDistance = Distance;
        //    }
        //}

        //Virus form up at special postion
        transform.position = Vector3.MoveTowards(transform.position, GoTo.GetComponent<VirusLocations>().VirusLocationList[RandomVirusLocation].Pos.transform.position, speed);

        //Check List Count
        for (int i = 0; i < GoTo.GetComponent<VirusLocations>().VirusLocationList.Count; i++)
        {
            for (int j = 0; j < GoTo.GetComponent<VirusLocations>().VirusLocationList[i].VirusList.Count; j++)
            {
                if (GoTo.GetComponent<VirusLocations>().VirusLocationList[i].VirusList.Count == 5)
                {
                    CreateBigVirus(GoTo.GetComponent<VirusLocations>().VirusLocationList[i].Pos);
                    GoTo.GetComponent<VirusLocations>().VirusLocationList[i].VirusList.Clear();
                }
            }
        }
    }

    void CreateBigVirus(GameObject pos)
    {
        VirusManager.GetComponent<EnemyManager>().VirusList.Add(Instantiate(VirusManager.GetComponent<EnemyManager>().BigVirusCube, pos.transform.position, Quaternion.identity) as GameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Bullet")
        {
            Destroy(gameObject);
            Player.GetComponent<Player>().Score += 100;
        }

        else if (col.name == "1VirusGoTo")
        {
            GoTo.GetComponent<VirusLocations>().VirusLocationList[0].VirusList.Add(transform.gameObject);
        }

        else if (col.name == "2VirusGoTo")
        {
            GoTo.GetComponent<VirusLocations>().VirusLocationList[1].VirusList.Add(transform.gameObject);
        }
        else if (col.name == "3VirusGoTo")
        {
            GoTo.GetComponent<VirusLocations>().VirusLocationList[2].VirusList.Add(transform.gameObject);
        }
        else if (col.name == "4VirusGoTo")
        {
            GoTo.GetComponent<VirusLocations>().VirusLocationList[3].VirusList.Add(transform.gameObject);
        }
    }

    void OnDestroy()
    {
        //If cell is alive but virus dies then add it back to the list
        //if (Cell && !Cell.GetComponent<Cell>().isDead)
        //{
        //    CellManager.GetComponent<CellManager>().CellList.Add(Cell);
        //}
    }

    public void HandleTimeInput()
    {
        throw new NotImplementedException();
    }
}
