using UnityEngine;
using System.Collections;

public class VirusScript : MonoBehaviour
{
    public GameObject target;
    public float speed = 1.0f;
    public int attackValue = 1;
    public int attackDuration = 0;
    public float percentTraveled = 0.0f;

    public Vector3 startingPosition, prevPosition, nextPosition;
    private float distance = .1f;
    private float startTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        transform.LookAt(target.transform);
        startingPosition = prevPosition = nextPosition = transform.position;
        distance = Vector3.Distance(startingPosition, target.transform.position);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Debug.Log("Target Lost");
            target = GetComponentInParent<CellManagerScript>().FindVirusNewTarget();
            transform.LookAt(target.transform);
            if (percentTraveled >= 1.0f)
            {
                startingPosition = transform.position;
                percentTraveled = .75f;
                attackDuration = 0;
            }
            else
            {
                percentTraveled -= .25f;
            }
            TurnUpdate();
        }

        float disCovered = (Time.time - startTime) * speed;
        float fracJourney = disCovered / distance;
        transform.position = Vector3.Lerp(prevPosition, nextPosition, fracJourney);
    }

    public void TurnUpdate()
    {
        percentTraveled += .25f;
        nextPosition = Vector3.Lerp(startingPosition, target.transform.position, percentTraveled);
        prevPosition = transform.position;
        startTime = Time.time;

        if (percentTraveled > 1.0f)
        {
            attackDuration += attackValue;
            if (attackDuration >= target.GetComponent<CellScript>().defense)
            {
                Attack();
            }
        }
    }

    //This virus destroys the cell it is attacking, changes to a new cell, and spawns a virus
    public virtual void Attack()
    {
        GetComponentInParent<CellManagerScript>().SpawnVirusSingleAdjacent(target.GetComponent<CellScript>().key);
        GetComponentInParent<CellManagerScript>().KillCell(target.GetComponent<CellScript>().key);
    }
}
