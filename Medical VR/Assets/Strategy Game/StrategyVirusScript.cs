using UnityEngine;
using System.Collections;

public class StrategyVirusScript : MonoBehaviour
{
    public GameObject target;
    public float speed = 1.0f;
    public int attackValue = 1;
    public int attackDuration = 0;
    public float percentTraveled = 0.0f;
    public bool standby = false;

    public Vector3 startingPosition, prevPosition, nextPosition;
    private float distance = .1f;
    private float startTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        if (target)
        {
            transform.LookAt(target.transform);
            startingPosition = prevPosition = nextPosition = transform.position;
            distance = Vector3.Distance(startingPosition, target.transform.position);
            Mathf.Clamp(distance, .001f, float.MaxValue);
        }
        else
        {
            startingPosition = prevPosition = nextPosition = transform.position;
            distance = .001f;
            standby = true;
        }
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float disCovered = (Time.time - startTime) * speed;
        float fracJourney = disCovered / distance;
        if (fracJourney >= 1.0f || !target)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.LookAt(target.transform);
        }
        transform.position = Vector3.Lerp(prevPosition, nextPosition, fracJourney);
    }

    public void TurnUpdate()
    {
        if (target && target.GetComponent<StrategyCellScript>().immunity >= 10)
        {
            target.GetComponent<StrategyCellScript>().hosted = false;
            target.GetComponent<StrategyCellScript>().targeted = false;
            standby = true;
            target = null;
        }

        if (standby)
        {
            target = GetComponentInParent<StrategyCellManagerScript>().FindVirusNewTarget(gameObject);
            if (standby)
            {
                if (percentTraveled < .75f)
                {
                    percentTraveled += .25f;
                }
                else if (percentTraveled > .75f)
                {
                    percentTraveled = .75f;
                }

                nextPosition = Vector3.Lerp(prevPosition, GetComponentInParent<StrategyCellManagerScript>().RandomPositionAboveHex(), 1);
                prevPosition = transform.position;
                startTime = Time.time;
                return;
            }
            startingPosition = transform.position;
        }

        //Should not be called anymore
        if (target == null)
        {
            Debug.Log("Target Lost");
            target = GetComponentInParent<StrategyCellManagerScript>().FindVirusNewTarget(gameObject);
            transform.LookAt(target.transform);
            if (percentTraveled >= 1.0f)
            {
                startingPosition = transform.position;
                distance = Vector3.Distance(startingPosition, target.transform.position);
                Mathf.Max(distance, .001f);
                percentTraveled = 1.0f;
                attackDuration = 0;
            }
            else
            {
                distance = Vector3.Distance(startingPosition, target.transform.position);
            }
        }


        percentTraveled += .25f;
        nextPosition = Vector3.Lerp(startingPosition, target.transform.position, percentTraveled);
        prevPosition = transform.position;
        startTime = Time.time;

        if (percentTraveled >= 1.0f)
            target.GetComponent<StrategyCellScript>().hosted = true;

        if (percentTraveled > 1.0f)
        {
            attackDuration += attackValue;
            if (attackDuration >= target.GetComponent<StrategyCellScript>().defense)
            {
                Attack();
            }
        }
    }

    //This virus destroys the cell it is attacking, changes to a new cell, and spawns a virus
    public virtual void Attack()
    {
        GetComponentInParent<StrategyCellManagerScript>().SpawnVirusSingleAdjacent(target.GetComponent<StrategyCellScript>().key);
        GetComponentInParent<StrategyCellManagerScript>().KillCell(target.GetComponent<StrategyCellScript>().key);
        target = GetComponentInParent<StrategyCellManagerScript>().FindVirusNewTarget(gameObject);
        startingPosition = transform.position;
    }

    public void OnDestroy()
    {
        if (target)
        {
            target.GetComponent<StrategyCellScript>().hosted = false;
            target.GetComponent<StrategyCellScript>().targeted = false;
        }
    }
}
