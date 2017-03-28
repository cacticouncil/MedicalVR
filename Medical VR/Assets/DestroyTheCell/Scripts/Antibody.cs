using UnityEngine;
using System.Collections;

enum Movements { XAxis = 0, YAxis = 1, XYAxis = 2, NXAxis = 3, NYAxis = 4, NXNYAxis = 5, XNYAxis = 6, NXYAxis = 7}
public class Antibody : MonoBehaviour
{
    GameObject WaveManager;
    GameObject Player;
    Movements M;
    bool AlwaysChasePlayer;
    bool FlashScreen;
    bool isRotating;
    float RotateTimer;
    float MoveTimer;
    public float Speed;

    void Start()
    {
        WaveManager = gameObject.transform.parent.gameObject;
        Player = WaveManager.GetComponent<WaveManager>().Player;
        M = (Movements)Random.Range(0, 7);
        AlwaysChasePlayer = false;
        FlashScreen = false;
        isRotating = false;
        RotateTimer = 0.0f;
        MoveTimer = 0.0f;
        Speed = .005f;
    }

    void Update()
    {
        if (AlwaysChasePlayer == false)
        {
            if (isRotating)
            {
                RotateTimer += Time.deltaTime;

                //Give it random rotate behavior
                switch (M)
                {
                    case Movements.XAxis:
                        transform.Rotate(.5f, 0, 0);
                        break;

                    case Movements.YAxis:
                        transform.Rotate(0, .5f, 0);
                        break;

                    case Movements.XYAxis:
                        transform.Rotate(.5f, .5f, 0);
                        break;

                    case Movements.NXAxis:
                        transform.Rotate(-.5f, 0, 0);
                        break;

                    case Movements.NYAxis:
                        transform.Rotate(0, -.5f, 0);
                        break;

                    case Movements.NXNYAxis:
                        transform.Rotate(-.5f, -.5f, 0);
                        break;

                    case Movements.XNYAxis:
                        transform.Rotate(.5f, -.5f, 0);
                        break;

                    case Movements.NXYAxis:
                        transform.Rotate(-.5f, .5f, 0);
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

                transform.position += transform.forward * Speed;
                GetComponent<Rigidbody>().velocity *= Speed;

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
            transform.LookAt(Player.GetComponent<VirusPlayer>().transform.position);
            transform.position += transform.forward * Speed;
            GetComponent<Rigidbody>().velocity *= Speed;
        }

        if (Player.GetComponent<VirusPlayer>().isGameover)
            Destroy(this.gameObject);
    }

    public bool CheckFOV()
    {
        if (Vector3.Angle(Player.transform.position - transform.position, transform.forward) <= 55 && FlashScreen == false)
        {
            Debug.Log("Can See Player");
            AlwaysChasePlayer = true;
            FlashScreen = true;
            return true;
        }
        return false;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "MainCamera" && AlwaysChasePlayer == true)
        {
            Destroy(gameObject);
            Player.GetComponent<VirusPlayer>().Lives -= 1;
            Player.GetComponent<VirusPlayer>().Respawn();
        }

        //Possibly colliding with the wall
        //Maybe make it head the opposite way
        else
        {

        }
    }
}
