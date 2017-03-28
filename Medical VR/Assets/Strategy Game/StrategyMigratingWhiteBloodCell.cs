using UnityEngine;
using System.Collections;

public class StrategyMigratingWhiteBloodCell : MonoBehaviour
{
    public StrategyVirusScript target;
    public float movementSpeed = 10.0f, turnSpeed = .1f;
    public Vector3 startingPosition, prevPosition, nextPosition;
    public StrategyCellManagerScript parent;
    public float percentTraveled = 0.0f;

    private float distance = .1f;
    private float startTime = 0.0f;
    private bool trav = false, kill = false, leave = false;

    // Use this for initialization
    void Start()
    {
        startingPosition = prevPosition = nextPosition = transform.position;
        parent = transform.parent.GetComponent<StrategyCellManagerScript>();
        parent.whiteCells.Add(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 destination;
        if (target)
            destination = Vector3.Lerp(startingPosition, target.transform.position, percentTraveled);
        else
            destination = nextPosition;
        float disCovered = (Time.time - startTime) * movementSpeed;
        float fracJourney = disCovered / distance;

        if (fracJourney >= 1.0f && (!target || (target && percentTraveled >= 1.0f)))
        {
            if (leave)
            {
                if (Vector3.Distance(transform.position, destination) < .1f)
                {
                    Destroy(transform.gameObject);
                }
                GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(prevPosition, destination, 1));
            }
            else if (trav)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                if (target)
                {
                    Destroy(target.gameObject);
                    kill = true;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, destination) < .1f)
                {
                    trav = true;
                }
                else
                {
                    GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(prevPosition, destination, 1));
                }
            }
        }
        else
        {
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(prevPosition, destination, fracJourney));
        }
    }

    public void TurnUpdate()
    {
        if (!kill)
        {
            trav = false;
            if (target == null)
            {
                target = parent.FindWhiteCellNewTarget();
                if (target == null)
                {
                    if (percentTraveled < 1f - turnSpeed)
                    {
                        percentTraveled += turnSpeed;
                    }
                    else
                    {
                        percentTraveled = 1f - turnSpeed;
                    }

                    nextPosition = Vector3.Lerp(prevPosition, parent.RandomPositionAboveHex(), percentTraveled);
                    prevPosition = transform.position;
                    distance = Vector3.Distance(prevPosition, nextPosition);
                    startTime = Time.time;
                    return;
                }
                startingPosition = transform.position;
            }

            percentTraveled += turnSpeed;
            //nextPosition = Vector3.Lerp(startingPosition, target.transform.position, percentTraveled);
            //prevPosition = transform.position;
            startTime = Time.time;

            if (percentTraveled >= 1.0f)
            {
                target.enabled = false;
            }
        }
        else
        {
            leave = true;
            Vector3 direction = Random.onUnitSphere;
            direction.y = Mathf.Clamp(direction.y, 0.65f, 1f);
            nextPosition = direction * 100.0f;
            startingPosition = prevPosition = transform.position;
            distance = Vector3.Distance(startingPosition, nextPosition);
            startTime = Time.time;
        }
    }

    void OnDestroy()
    {
        parent.whiteCells.Remove(this);
    }
}