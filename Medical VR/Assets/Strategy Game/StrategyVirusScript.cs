using UnityEngine;
using System.Collections;

public class StrategyVirusScript : MonoBehaviour
{
    public GameObject target;
    public float movementSpeed = 1.0f;
    public float turnSpeed = .25f;
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
            Mathf.Max(distance, .001f);
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
        float disCovered = (Time.time - startTime) * movementSpeed;
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
        if (standby)
        {
            target = transform.parent.GetComponent<StrategyCellManagerScript>().FindVirusNewTarget(gameObject);
            if (standby)
            {
                if (percentTraveled < .75f)
                {
                    percentTraveled += turnSpeed;
                }
                else if (percentTraveled > .75f)
                {
                    percentTraveled = .75f;
                }

                nextPosition = Vector3.Lerp(prevPosition, transform.parent.GetComponent<StrategyCellManagerScript>().RandomPositionAboveHex(), 1);
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
            target = transform.parent.GetComponent<StrategyCellManagerScript>().FindVirusNewTarget(gameObject);
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


        percentTraveled += turnSpeed;
        nextPosition = Vector3.Lerp(startingPosition, target.transform.position, percentTraveled);
        prevPosition = transform.position;
        startTime = Time.time;

        if (percentTraveled >= 1.0f && !target.GetComponent<StrategyCellScript>().hosted)
        {
            if (target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.CH25H)
            {
                if (Random.Range(0.0f, 100.0f) < 90)
                {
                    target.GetComponent<StrategyCellScript>().targeted = false;
                    target = transform.parent.GetComponent<StrategyCellManagerScript>().FindVirusNewTarget(gameObject);
                    return;
                }
            }
            target.GetComponent<StrategyCellScript>().hosted = true;
            target.GetComponent<StrategyCellScript>().virus = gameObject;
        }

        if (percentTraveled > 1.0f)
        {
            if (target.GetComponent<StrategyCellScript>().protein != StrategyCellScript.Proteins.Mx1 || (target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.Mx1 && Random.Range(0.0f, 100.0f) <= 50))
            {
                attackDuration += attackValue;
                if (attackDuration >= target.GetComponent<StrategyCellScript>().defense)
                {
                    Attack();
                }
            }
        }
    }

    //This virus destroys the cell it is attacking, changes to a new cell, and spawns a virus
    public virtual void Attack()
    {
        bool spawned = false;
        if (target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.None || target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.CH25H || target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.Mx1)
        {
            spawned = true;
            transform.parent.GetComponent<StrategyCellManagerScript>().SpawnVirusSingleAdjacent(target.GetComponent<StrategyCellScript>().key, transform.position);
            transform.parent.GetComponent<StrategyCellManagerScript>().SpawnVirusSingleAdjacent(target.GetComponent<StrategyCellScript>().key, transform.position);
        }
        if (spawned ||
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.RNase_L ||
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.PKR || 
            target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.TRIM22 || 
            (target.GetComponent<StrategyCellScript>().protein == StrategyCellScript.Proteins.IFIT && Random.Range(0.0f, 100.0f) > 90))
            transform.parent.GetComponent<StrategyCellManagerScript>().KillCell(target.GetComponent<StrategyCellScript>().key);
        Destroy(gameObject);
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
