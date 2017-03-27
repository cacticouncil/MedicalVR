using UnityEngine;
using System.Collections;

public class StrategyMigratingWhiteBloodCell : MonoBehaviour
{
    public StrategyVirusScript target;
    public float movementSpeed = 10.0f, turnSpeed = .1f;
    public Vector3 startingPosition, prevPosition, nextPosition;
    public StrategyCellManagerScript parent;

    private float percentTraveled = 0.0f;
    private float distance = .1f;
    private float startTime = 0.0f;
    private bool trav = false;
    private bool kill = false;

    // Use this for initialization
    void Start()
    {
        parent = transform.parent.GetComponent<StrategyCellManagerScript>();
        //target.enabled = false;
        //transform.parent.GetComponent<StrategyCellManagerScript>()
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float disCovered = (Time.time - startTime) * movementSpeed;
        float fracJourney = disCovered / distance;
        //fracJourney = 0.0f;
        if (fracJourney >= 1.0f && (!target || (target && percentTraveled >= 1.0f)))
        {
            if (trav)
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
                if (Vector3.Distance(transform.position, nextPosition) < .1f)
                {
                    trav = true;
                }
                else
                {
                    GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(prevPosition, nextPosition, 1));
                }
            }
        }
        else
        {
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(prevPosition, nextPosition, fracJourney));
        }
    }

    void TurnUpdate()
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
        nextPosition = Vector3.Lerp(startingPosition, target.transform.position, percentTraveled);
        prevPosition = transform.position;
        startTime = Time.time;

        if (percentTraveled >= 1.0f)
        {
            target.enabled = false;
        }
    }
}