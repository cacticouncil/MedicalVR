using UnityEngine;
using System.Collections;

enum Movements { XAxis = 0, YAxis = 1, XYAxis = 2, NXAxis = 3, NYAxis = 4, NXNYAxis = 5, XNYAxis = 6, NXYAxis = 7 }

public class AntiViralProtein : MonoBehaviour
{
    Movements M;
    GameObject WaveManager;
    GameObject Player;
    bool AlwaysChasePlayer;
    bool isRotating;
    float RotateTimer;
    float MoveTimer;

    [System.NonSerialized]
    public float patrolSpeed = .4f;

    [System.NonSerialized]
    public float chaceSpeed = .6f;

    void Start()
    {
        WaveManager = gameObject.transform.parent.gameObject;
        Player = WaveManager.GetComponent<WaveManager>().Player;
        M = (Movements)Random.Range(0, 7);
        AlwaysChasePlayer = false;
        isRotating = false;
        RotateTimer = 0.0f;
        MoveTimer = 0.0f;
    }

    void Update()
    {
        if (GlobalVariables.tutorial == true && WaveManager.GetComponent<WaveManager>().WaveNumber == 1)
        {
            AlwaysChasePlayer = true;
            GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(Player.transform.position - transform.position));
            GetComponent<Rigidbody>().MovePosition(transform.position + (Player.GetComponent<VirusPlayer>().transform.position - transform.position) * patrolSpeed * Time.deltaTime);
        }

        else
        {
            if (AlwaysChasePlayer == false)
            {
                if (isRotating == true)
                {
                    RotateTimer += Time.deltaTime * .5f;

                    //Give it random rotate behavior
                    switch (M)
                    {
                        case Movements.XAxis:
                            GetComponent<Rigidbody>().AddRelativeTorque(.05f, 0, 0);
                            break;

                        case Movements.YAxis:
                            GetComponent<Rigidbody>().AddRelativeTorque(0, .05f, 0);
                            break;

                        case Movements.XYAxis:
                            GetComponent<Rigidbody>().AddRelativeTorque(.05f, .05f, 0);
                            break;

                        case Movements.NXAxis:
                            GetComponent<Rigidbody>().AddRelativeTorque(-.05f, 0, 0);
                            break;

                        case Movements.NYAxis:
                            GetComponent<Rigidbody>().AddRelativeTorque(0, -.05f, 0);
                            break;

                        case Movements.NXNYAxis:
                            GetComponent<Rigidbody>().AddRelativeTorque(-.05f, -.05f, 0);
                            break;

                        case Movements.XNYAxis:
                            GetComponent<Rigidbody>().AddRelativeTorque(.05f, -.05f, 0);
                            break;

                        case Movements.NXYAxis:
                            GetComponent<Rigidbody>().AddRelativeTorque(-.05f, .05f, 0);
                            break;
                        default:
                            break;
                    }

                    if (RotateTimer >= 4.0f)
                    {
                        M = (Movements)Random.Range(0, 7);
                        RotateTimer = 0.0f;
                        isRotating = false;
                    }
                }

                //After rotate timer then move in that direction
                else if (isRotating == false)
                {
                    MoveTimer += Time.deltaTime;
                    
                    GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 1) * patrolSpeed);

                    if (MoveTimer >= 5.5f)
                    {
                        MoveTimer = 0.0f;
                        isRotating = true;
                    }
                }
            }

            else if (AlwaysChasePlayer == true)
            {
                //Chase the player
                GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(Player.transform.position - transform.position));
                GetComponent<Rigidbody>().MovePosition(transform.position + (Player.GetComponent<VirusPlayer>().transform.position - transform.position) * chaceSpeed * Time.deltaTime);
            }

        }

        if (Player.GetComponent<VirusPlayer>().isGameover || Player.GetComponent<VirusPlayer>().TutorialModeCompleted == true)
            Destroy(this.gameObject);
    }

    public void DestroyAntiBody()
    {
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        WaveManager.GetComponent<WaveManager>().AntiViralProteinList.Remove(transform.gameObject);
    }

    public bool CheckFOV()
    {
        if (Player != null && Vector3.Angle(Player.transform.position - transform.position, transform.forward) <= 35 && Vector3.Distance(transform.position, Player.transform.position) <= 5.0f)
        {
            AlwaysChasePlayer = true;
            return true;
        }

        return false;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player" && AlwaysChasePlayer == true)
        {
            Destroy(gameObject);
            Player.GetComponent<VirusPlayer>().Lives -= 1;
            Player.GetComponent<VirusPlayer>().Respawn();
            Player.GetComponent<VirusPlayer>().CurrentScore -= 300;

            if (Player.GetComponent<VirusPlayer>().CurrentScore <= 0)
                Player.GetComponent<VirusPlayer>().CurrentScore = 0;
        }
    }
}
