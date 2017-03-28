using UnityEngine;
using System.Collections;

public class StrategyVirusScript : MonoBehaviour
{
    public StrategyCellScript target;
    public float movementSpeed = 10.0f;
    public float turnSpeed = .1f;
    public float attackValue = .5f;
    public float attackDuration = 0;
    public float health = 15.0f;
    public float percentTraveled = 0.0f;
    public bool standby = false;
    public bool targeted = false;

    public Vector3 startingPosition, prevPosition, nextPosition;

    public StrategyCellManagerScript parent;

    private float distance = .1f;
    private float startTime = 0.0f;
    private bool trav = false;

    // Use this for initialization
    void Start()
    {
        if (target)
        {
            target.targeted = true;
            Vector3 lookRotation = target.transform.position - transform.position;
            if (lookRotation != Vector3.zero)
            {
                GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(target.transform.position - transform.position));
            }
            else
            {
                GetComponent<Rigidbody>().MoveRotation(Quaternion.identity);
            }
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
        parent.viruses.Add(this);
        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float disCovered = (Time.time - startTime) * movementSpeed;
        float fracJourney = disCovered / distance;
        //fracJourney = 0.0f;
        if (fracJourney >= 1.0f && (!target || (target && percentTraveled >= 1.0f)))
        {
            GetComponent<Rigidbody>().MoveRotation(Quaternion.identity);
            if (trav)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
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
            if (target)
            {
                Vector3 lookRotation = target.transform.position - transform.position;
                if (lookRotation != Vector3.zero)
                {
                    GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(lookRotation));
                }
                else
                {
                    GetComponent<Rigidbody>().MoveRotation(Quaternion.identity);
                }
            }
            else
            {
                Vector3 lookRotation = nextPosition - transform.position;
                if (lookRotation != Vector3.zero)
                {
                    GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(lookRotation));
                }
                else
                {
                    GetComponent<Rigidbody>().MoveRotation(Quaternion.identity);
                }
            }
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(prevPosition, nextPosition, fracJourney));
        }
    }

    public void TurnUpdate()
    {
        trav = false;
        if (standby)
        {
            target = parent.FindVirusNewTarget(gameObject);
            if (standby)
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
                GetComponent<Rigidbody>().MoveRotation(Quaternion.identity);
                distance = Vector3.Distance(prevPosition, nextPosition);
                startTime = Time.time;
                return;
            }
            startingPosition = transform.position;
        }

        //Should not be called anymore
        #region Null
        if (target == null)
        {
            Debug.Log("Target Lost AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            target = parent.FindVirusNewTarget(gameObject);
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
        #endregion

        percentTraveled += turnSpeed;
        nextPosition = Vector3.Lerp(startingPosition, target.transform.position, percentTraveled);
        prevPosition = transform.position;
        startTime = Time.time;

        if (percentTraveled >= 1.0f && !target.hosted)
        {
            if (target.protein == StrategyCellScript.Proteins.CH25H)
            {
                if (Random.Range(0.0f, 100.0f) < 90)
                {
                    target.targeted = false;
                    target = parent.FindVirusNewTarget(gameObject);
                    return;
                }
            }

            if (GetComponent<Collider>().enabled)
            {
                GetComponent<Collider>().enabled = false;
            }
            target.hosted = true;
            target.virus = gameObject;
        }

        if (percentTraveled > 1.0f)
        {
            if (target.protein != StrategyCellScript.Proteins.Mx1 || (target.protein == StrategyCellScript.Proteins.Mx1 && Random.Range(0.0f, 100.0f) <= 50))
            {
                attackDuration += attackValue;
                if (attackDuration >= target.defense)
                {
                    float t = health;
                    health -= target.immunity;
                    target.immunity -= t;
                    target.immunity = Mathf.Max(0.0f, target.immunity);
                    if (health <= 0)
                    {
                        Destroy(gameObject);
                        return;
                    }
                    Attack();
                }
            }
        }
    }

    //This virus destroys the cell it is attacking, changes to a new cell, and spawns a virus
    public virtual void Attack()
    {
        bool spawned = false;
        if (target.protein == StrategyCellScript.Proteins.None || target.protein == StrategyCellScript.Proteins.CH25H || target.protein == StrategyCellScript.Proteins.Mx1)
        {
            spawned = true;
            parent.SpawnVirusSingleAdjacent(target.key, transform.position);
            parent.SpawnVirusSingleAdjacent(target.key, transform.position);
        }
        if (spawned ||
            target.protein == StrategyCellScript.Proteins.RNase_L ||
            target.protein == StrategyCellScript.Proteins.PKR ||
            target.protein == StrategyCellScript.Proteins.TRIM22 ||
            (target.protein == StrategyCellScript.Proteins.IFIT && Random.Range(0.0f, 100.0f) > 90))
            parent.KillCell(target.key);
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        if (target)
        {
            target.hosted = false;
            target.targeted = false;
            target.RefreshUI();
        }
        parent.viruses.Remove(this);
    }
}
