using UnityEngine;
using System.Collections;

public class StrategyTransporter : MonoBehaviour
{
    public Vector3 destination;

    private Transform passenger;
    private Vector3 startPos, direction;
    private float t = 0.0f;

    void Start()
    {
        startPos = transform.position;
        passenger = transform.GetChild(0);
        direction = (startPos - destination) * .02f;
    }

    void FixedUpdate()
    {
        t += .02f;
        transform.position += direction;
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
        if (t >= 1.0f)
        {
            if (passenger)
            {
                if (passenger.GetComponent<StrategyVirusScript>())
                    passenger.GetComponent<Collider>().isTrigger = false;
                else if (passenger.GetComponent<Collider>())
                {
                    passenger.GetComponent<Collider>().enabled = true;
                }
                if (passenger.transform.childCount > 0)
                {
                    for (int i = 0; i < passenger.childCount; i++)
                    {
                        if (passenger.GetChild(i).GetComponent<Collider>())
                        {
                            passenger.GetChild(i).GetComponent<Collider>().enabled = true;
                        }
                    }
                }
                passenger.parent = transform.parent;
            }
            Destroy(gameObject);
        }
    }
}
