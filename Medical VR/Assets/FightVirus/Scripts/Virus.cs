using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum VirusMovement { UP = 0, DOWN = 1, LEFT = 2, RIGHT = 3, UPLEFT = 4, UPRIGHT = 5, DOWNLEFT = 6, DOWNRIGHT = 7 }
public class Virus : MonoBehaviour
{
    GameObject VirusManager;
    GameObject Player;
    GameObject GoTo;

    VirusMovement VM;
    int RandomVirusLocation;
    public float Speed;
    public int Health;
    bool EnteredZone;

    bool BossCanTakeDamage;
    bool BossMadeLocation;
    float BossMovementTimer;

    Vector3 SavedLocation;
    float AngleSpeed;
    void Start()
    {
        VirusManager = gameObject.transform.parent.gameObject;
        Player = VirusManager.GetComponent<VirusManager>().Player;
        GoTo = VirusManager.GetComponent<VirusManager>().VirusLocations;
        EnteredZone = false;

        RandomVirusLocation = Random.Range(0, 4);
        VM = (VirusMovement)Random.Range(0, 8);

        BossCanTakeDamage = false;
        BossMadeLocation = false;
        BossMovementTimer = 0.0f;

        AngleSpeed = 20.0f;

        if (VirusManager.GetComponent<VirusManager>().Wave1 == true)
        {
            //Speed = 1.0f;
            Speed = 0.006f;
            Health = 20;
        }

        else if (VirusManager.GetComponent<VirusManager>().Wave2 == true)
        {
            Speed = 0.009f;
            Health = 40;
        }

        else if (VirusManager.GetComponent<VirusManager>().Wave3 == true)
        {
            Speed = 0.01f;
            Health = 60;
        }

        else if (VirusManager.GetComponent<VirusManager>().Wave4 == true)
        {
            Speed = 0.08f;
            Health = 1000;
        }
    }

    void Update()
    {
        if (transform.tag == "Virus")
        {
            //Virus form up at special postion
            transform.position = Vector3.MoveTowards(transform.position, GoTo.GetComponent<VirusLocations>().VirusLocationList[RandomVirusLocation].Pos.transform.position, Speed);
        }

        else if (transform.tag == "BigVirus")
        {
            //Temporaily Fixed
            Speed = 0.01f;
            transform.LookAt(Player.transform.position);
            transform.Rotate(0, 180, 0);
            transform.position += transform.forward * Speed;
        }

        //For Boss 
        else if (transform.tag == "Boss")
        {
            if (BossMadeLocation == false)
            {
                float Distance1 = Vector3.Distance(transform.position, Player.transform.position);
                if (Distance1 >= 4.5f)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), 20 * Time.deltaTime);
                    transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed);
                }

                else
                {
                    //Basically the Boss made it to its destination
                    SavedLocation = transform.position;
                    BossMadeLocation = true;
                    BossCanTakeDamage = true;
                }
            }

            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), 2.5f * Time.deltaTime);

                //The boss moves unpredicatble and rotate
                BossMovementTimer += Time.deltaTime;
                if (BossMovementTimer > 6.5f)
                {
                    BossMovementTimer = 0.0f;
                    VM = (VirusMovement)Random.Range(0, 8);
                }

                switch (VM)
                {
                    case VirusMovement.UP:
                        transform.RotateAround(Player.transform.position, Vector3.up, AngleSpeed * Time.deltaTime);
                        break;

                    case VirusMovement.DOWN:
                        transform.RotateAround(Player.transform.position, Vector3.down, AngleSpeed * Time.deltaTime);
                        break;

                    case VirusMovement.LEFT:
                        transform.RotateAround(Player.transform.position, Vector3.left, AngleSpeed * Time.deltaTime);
                        break;

                    case VirusMovement.RIGHT:
                        transform.RotateAround(Player.transform.position, Vector3.right, AngleSpeed * Time.deltaTime);
                        break;

                    case VirusMovement.UPLEFT:
                        transform.RotateAround(Player.transform.position, Vector3.up, AngleSpeed * Time.deltaTime);
                        transform.RotateAround(Player.transform.position, Vector3.left, AngleSpeed * Time.deltaTime);
                        break;

                    case VirusMovement.UPRIGHT:
                        transform.RotateAround(Player.transform.position, Vector3.up, AngleSpeed * Time.deltaTime);
                        transform.RotateAround(Player.transform.position, Vector3.right, AngleSpeed * Time.deltaTime);
                        break;

                    case VirusMovement.DOWNLEFT:
                        transform.RotateAround(Player.transform.position, Vector3.down, AngleSpeed * Time.deltaTime);
                        transform.RotateAround(Player.transform.position, Vector3.left, AngleSpeed * Time.deltaTime);
                        break;

                    case VirusMovement.DOWNRIGHT:
                        transform.RotateAround(Player.transform.position, Vector3.down, AngleSpeed * Time.deltaTime);
                        transform.RotateAround(Player.transform.position, Vector3.right, AngleSpeed * Time.deltaTime);
                        break;

                    default:
                        break;
                }
            }
        }

        if (Player.GetComponent<Player>().isGameOver == true)
        {
            Destroy(this.gameObject);
        }
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
                        Player.GetComponent<Player>().isGameOver = true;
                    }
                }
            }
        }

        if (transform.tag == "Virus")
        {
            if (col.name == "1VirusGoTo")
            {
                GoTo.GetComponent<VirusLocations>().VirusLocationList[0].VirusList.Add(transform.gameObject);
                GoTo.GetComponent<VirusLocations>().VirusLocationList[0].SmallVirusCount += 1;
            }

            else if (col.name == "2VirusGoTo")
            {
                GoTo.GetComponent<VirusLocations>().VirusLocationList[1].VirusList.Add(transform.gameObject);
                GoTo.GetComponent<VirusLocations>().VirusLocationList[1].SmallVirusCount += 1;
            }

            else if (col.name == "3VirusGoTo")
            {
                GoTo.GetComponent<VirusLocations>().VirusLocationList[2].VirusList.Add(transform.gameObject);
                GoTo.GetComponent<VirusLocations>().VirusLocationList[2].SmallVirusCount += 1;
            }

            else if (col.name == "4VirusGoTo")
            {
                GoTo.GetComponent<VirusLocations>().VirusLocationList[3].VirusList.Add(transform.gameObject);
                GoTo.GetComponent<VirusLocations>().VirusLocationList[3].SmallVirusCount += 1;
            }

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
