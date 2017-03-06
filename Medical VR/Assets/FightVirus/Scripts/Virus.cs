using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class Virus : MonoBehaviour, TimedInputHandler
{
    GameObject VirusManager;
    GameObject Player;
    GameObject GoTo;

    public float Speed;
    public int Health;
    int RandomVirusLocation;

    void Start()
    {
        VirusManager = gameObject.transform.parent.gameObject;
        Player = VirusManager.GetComponent<EnemyManager>().Player;
        GoTo = VirusManager.GetComponent<EnemyManager>().VirusLocations;

        RandomVirusLocation = UnityEngine.Random.Range(0, 0);

        if (VirusManager.GetComponent<EnemyManager>().Wave1 == true)
        {
            Speed = 0.005f;
            Health = 10;
        }

        if (VirusManager.GetComponent<EnemyManager>().Wave2 == true)
        {
            Speed = 0.009f;
            Health = 30;
        }

        if (VirusManager.GetComponent<EnemyManager>().Wave3 == true)
        {
            Speed = 0.01f;
            Health = 50;
        }

        if (VirusManager.GetComponent<EnemyManager>().Wave4 == true)
        {
            Speed = 0.05f;
            Health = 100;
        }
    }

    void Update()
    {
        //Virus form up at special postion
        transform.position = Vector3.MoveTowards(transform.position, GoTo.GetComponent<VirusLocations>().VirusLocationList[RandomVirusLocation].Pos.transform.position, Speed);

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

        if (transform.name == "Boss")
        {
            if (Vector3.Distance(transform.position, Player.transform.position) != 5.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed);
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
            Health -= col.GetComponent<BulletScript>().Damage;

            if (Health == 0)
            {
                VirusManager.GetComponent<EnemyManager>().VirusList.Remove(gameObject);
                Destroy(gameObject);
                Player.GetComponent<Player>().Score += 100;
            }
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
        if (GoTo.GetComponent<VirusLocations>().VirusLocationList[0].VirusList.Contains(transform.gameObject))
        {
            GoTo.GetComponent<VirusLocations>().VirusLocationList[0].VirusList.Remove(transform.gameObject);
        }

        else if (GoTo.GetComponent<VirusLocations>().VirusLocationList[1].VirusList.Contains(transform.gameObject))
        {
            GoTo.GetComponent<VirusLocations>().VirusLocationList[1].VirusList.Remove(transform.gameObject);
        }

        else if (GoTo.GetComponent<VirusLocations>().VirusLocationList[2].VirusList.Contains(transform.gameObject))
        {
            GoTo.GetComponent<VirusLocations>().VirusLocationList[2].VirusList.Remove(transform.gameObject);
        }

        else if (GoTo.GetComponent<VirusLocations>().VirusLocationList[3].VirusList.Contains(transform.gameObject))
        {
            GoTo.GetComponent<VirusLocations>().VirusLocationList[3].VirusList.Remove(transform.gameObject);
        }
    }

    public void HandleTimeInput()
    {
        throw new NotImplementedException();
    }
}
