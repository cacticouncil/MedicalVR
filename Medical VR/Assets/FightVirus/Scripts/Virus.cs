using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Virus : MonoBehaviour
{
    GameObject VirusManager;
    GameObject Player;
    GameObject GoTo;

    public float Speed;
    public int Health;
    bool BossCanTakeDamage;
    bool EnteredZone;
    int RandomVirusLocation;

    void Start()
    {
        VirusManager = gameObject.transform.parent.gameObject;
        Player = VirusManager.GetComponent<VirusManager>().Player;
        GoTo = VirusManager.GetComponent<VirusManager>().VirusLocations;
        EnteredZone = false;

        RandomVirusLocation = UnityEngine.Random.Range(0, 0);

        if (VirusManager.GetComponent<VirusManager>().Wave1 == true)
        {
            Speed = 0.005f;
            Health = 10;
        }

        if (VirusManager.GetComponent<VirusManager>().Wave2 == true)
        {
            Speed = 0.009f;
            Health = 30;
        }

        if (VirusManager.GetComponent<VirusManager>().Wave3 == true)
        {
            Speed = 0.01f;
            Health = 50;
        }

        if (VirusManager.GetComponent<VirusManager>().Wave4 == true)
        {
            Speed = 0.08f;
            Health = 100;
        }
    }

    void Update()
    {
        if (transform.tag == "Virus")
        {
            //Virus form up at special postion
            transform.position = Vector3.MoveTowards(transform.position, GoTo.GetComponent<VirusLocations>().VirusLocationList[RandomVirusLocation].Pos.transform.position, Speed);

            //Check List Count
            for (int i = 0; i < GoTo.GetComponent<VirusLocations>().VirusLocationList.Count; i++)
            {
                if (GoTo.GetComponent<VirusLocations>().VirusLocationList[i].VirusList.Count == 5)
                {
                    CreateBigVirus(GoTo.GetComponent<VirusLocations>().VirusLocationList[i].Pos);

                    //Delete from original VirusList
                    //if (GoTo.GetComponent<VirusLocations>().VirusLocationList[i].Pos.transform.position == transform.position)
                    //{
                    //    VirusManager.GetComponent<EnemyManager>().VirusList.Remove(transform.gameObject);
                    //}

                    GoTo.GetComponent<VirusLocations>().VirusLocationList[i].VirusList.Clear();
                }
            }
        }

        else if (transform.tag == "BigVirus")
        {

        }

        //For Boss 
        else if (transform.tag == "Boss")
        {
            float Distance1 = Vector3.Distance(transform.position, Player.transform.position);
            if (Distance1 >= 4.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed);
                float Distance2 = Vector3.Distance(transform.position, Player.transform.position);

                //So the boss follows the player
                Vector3 forward;
                forward = Player.transform.forward;
                transform.position = forward * Distance2;

                BossCanTakeDamage = false;
            }
            else
            {
                BossCanTakeDamage = true;
            }
        }
    }

    void CreateBigVirus(GameObject pos)
    {
        VirusManager.GetComponent<VirusManager>().VirusList.Add(Instantiate(VirusManager.GetComponent<VirusManager>().BigVirusCube, pos.transform.position, Quaternion.identity) as GameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Bullet")
        {
            if (transform.tag != "Boss")
            {
                Health -= col.GetComponent<BulletScript>().Damage;

                if (Health == 0)
                {
                    VirusManager.GetComponent<VirusManager>().VirusList.Remove(gameObject);
                    Destroy(gameObject);
                    Player.GetComponent<Player>().Score += 100;
                }
            }

            else if (transform.tag == "Boss")
            {
                if (BossCanTakeDamage == true)
                {
                    Health -= col.GetComponent<BulletScript>().Damage;

                    if (Health == 0)
                    {
                        VirusManager.GetComponent<VirusManager>().VirusList.Remove(gameObject);
                        Destroy(gameObject);
                        Player.GetComponent<Player>().Score += 100;
                    }
                }
            }
        }

        else
        {
            if (col.name == "1VirusGoTo")
                GoTo.GetComponent<VirusLocations>().VirusLocationList[0].VirusList.Add(transform.gameObject);

            else if (col.name == "2VirusGoTo")
                GoTo.GetComponent<VirusLocations>().VirusLocationList[1].VirusList.Add(transform.gameObject);

            else if (col.name == "3VirusGoTo")
                GoTo.GetComponent<VirusLocations>().VirusLocationList[2].VirusList.Add(transform.gameObject);

            else if (col.name == "4VirusGoTo")
                GoTo.GetComponent<VirusLocations>().VirusLocationList[3].VirusList.Add(transform.gameObject);

            EnteredZone = true;
        }
    }

    void OnDestroy()
    {
        if (EnteredZone == true)
        {
            if (GoTo.GetComponent<VirusLocations>().VirusLocationList[0].VirusList.Contains(transform.gameObject))
                GoTo.GetComponent<VirusLocations>().VirusLocationList[0].VirusList.Remove(transform.gameObject);

            else if (GoTo.GetComponent<VirusLocations>().VirusLocationList[1].VirusList.Contains(transform.gameObject))
                GoTo.GetComponent<VirusLocations>().VirusLocationList[1].VirusList.Remove(transform.gameObject);

            else if (GoTo.GetComponent<VirusLocations>().VirusLocationList[2].VirusList.Contains(transform.gameObject))
                GoTo.GetComponent<VirusLocations>().VirusLocationList[2].VirusList.Remove(transform.gameObject);

            else if (GoTo.GetComponent<VirusLocations>().VirusLocationList[3].VirusList.Contains(transform.gameObject))
                GoTo.GetComponent<VirusLocations>().VirusLocationList[3].VirusList.Remove(transform.gameObject);
        }
    }
}
