using UnityEngine;
using System.Collections;
using System.Collections.Generic;
enum VirusBehavior { LeaveCell = 0, AttackProtein = 1, AdvancedAI = 2 }

public class Virus : MonoBehaviour
{
    VirusBehavior VB;
    Vector3 RandomPos;

    GameObject VirusManager;
    GameObject CellManager;
    GameObject Cell;

    public float speed = .5f;
    int RandomCell;

    void Start()
    {
        RandomPos = Random.onUnitSphere;
        RandomPos *= 50.0f;

        int RandomBehavior = Random.Range(0, 2);
        VB = (VirusBehavior)RandomBehavior;

        VirusManager = GameObject.Find("VirusSpawn");
        CellManager = GameObject.Find("CellSpawn");

        if (VB == VirusBehavior.AttackProtein)
        {
            if (CellManager.GetComponent<CellManager>().CellList.Count != 0)
            {
                RandomCell = Random.Range(0, CellManager.GetComponent<CellManager>().CellList.Count);
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

            case VirusBehavior.AdvancedAI:
                //transform.position = Vector3.MoveTowards(transform.position, , speed);
                break;

            default:
                transform.position = Vector3.MoveTowards(transform.position, RandomPos, speed);
                break;
        }
    }

    void Duplicate()
    {
        VirusManager.GetComponent<EnemyManager>().VirusList.Add(Instantiate(VirusManager.GetComponent<EnemyManager>().VirusCube, transform.position, Quaternion.identity, transform) as GameObject);
        //transform.position = -Vector3.MoveTowards(transform.position, RandomPos, speed);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Bullet")
        {
            Destroy(gameObject);
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
        if (Cell && !Cell.GetComponent<Cell>().isDead)
        {
            CellManager.GetComponent<CellManager>().CellList.Add(Cell);
        }
    }
}
