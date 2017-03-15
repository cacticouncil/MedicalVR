using UnityEngine;
using System.Collections;

enum Movements { XLeft = 0, XRight = 1, YUp = 2, YDown = 3, ZForward = 4, ZBackward = 5}
public class Antibody : MonoBehaviour
{
    GameObject AntibodyManager;
    GameObject Player;
    Vector3 Temp = Vector3.zero;
    Movements M;
    bool AlwaysChasePlayer;
    bool FlashScreen; 
    float MovementTimer;
    float Speed;
    void Start ()
    {
        AntibodyManager = gameObject.transform.parent.gameObject;
        Player = AntibodyManager.GetComponent<AntibodyManager>().Player;
        AlwaysChasePlayer = false;
        FlashScreen = false;
        M = (Movements)Random.Range(0, 6);
        //M = (Movements)4;
        MovementTimer = 0.0f;
        Speed = 2.0f;
    }
	
	void Update ()
    {
        if (AlwaysChasePlayer == false)
        {
            MovementTimer += Time.deltaTime;
            if (MovementTimer >= 4.5f)
            {
                M = (Movements)Random.Range(0, 6);
                //M = (Movements)4;
                MovementTimer = 0.0f;
            }

            //Give it random movement behavior
            switch (M)
            {
                case Movements.XLeft:
                    transform.position = new Vector3(transform.position.x - .01f, transform.position.y, transform.position.z);
                    break;

                case Movements.XRight:
                    transform.position = new Vector3(transform.position.x + .01f, transform.position.y, transform.position.z);
                    break;

                case Movements.YUp:
                    transform.position = new Vector3(transform.position.x, transform.position.y + .01f, transform.position.z);
                    break;

                case Movements.YDown:
                    transform.position = new Vector3(transform.position.x, transform.position.y - .01f, transform.position.z);
                    break;

                case Movements.ZForward:
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + .01f);
                    break;

                case Movements.ZBackward:
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - .01f);
                    break;
                default:
                    break;
            }

            //GetComponent<Rigidbody>().velocity *= Speed;
        }

        else if (AlwaysChasePlayer == true)
        {
            //Chase the player
            transform.LookAt(Player.GetComponent<VirusPlayer>().transform.position);
            transform.position = Vector3.SmoothDamp(transform.position, Player.GetComponent<VirusPlayer>().transform.position, ref Temp, Speed);
            //GetComponent<Rigidbody>().velocity *= Speed;
        }
    } 

    public bool CheckFOV()
    {
        if (Vector3.Angle(Player.transform.position - transform.position, transform.forward) <= 45 && FlashScreen == false)
        {
            Debug.Log("Can See Player");
            AlwaysChasePlayer = true;
            FlashScreen = true;
            return true;
        }
        return false;
    }
}
