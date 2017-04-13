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
    public bool selected = false;

    public Vector3 startingPosition, prevPosition, nextPosition;

    public StrategyCellManagerScript parent;
    public TMPro.TextMeshPro attack, speed, immunity;

    private float distance = .1f;
    private float startTime = 0.0f;
    private bool trav = false;
    private Vector2 key = new Vector2(-500, 500);

    private MoveCamera mainCamera;
    private float camOffset = 5.0f;
    private float scaledDistance = 1.3f;

    // Use this for initialization
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.GetComponent<MoveCamera>();
        }

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

        percentTraveled += turnSpeed;
        nextPosition = Vector3.Lerp(startingPosition, target.transform.position, percentTraveled);
        prevPosition = transform.position;
        startTime = Time.time;

        if (percentTraveled >= 1.0f && !target.hosted)
        {
            if (target.protein == Proteins.CH25H)
            {
                if (Random.Range(0.0f, 100.0f) < 90)
                {
                    target.targeted = false;
                    target = parent.FindVirusNewTarget(gameObject);
                    return;
                }
            }
            target.hosted = true;
            target.virus = gameObject;
        }

        if (percentTraveled > 1.0f)
        {
            if (target.protein != Proteins.Mx1 || (target.protein == Proteins.Mx1 && Random.Range(0.0f, 100.0f) <= 50))
            {
                attackDuration += attackValue;
                if (target.immunity > health)
                {
                    target.immunity -= health;
                    Destroy(gameObject);
                }
                if (attackDuration >= Mathf.Sqrt(target.defense * 5))
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
        if (target.protein == Proteins.None || target.protein == Proteins.CH25H || target.protein == Proteins.Mx1)
        {
            spawned = true;
            parent.SpawnVirusSingleAdjacent(target.key, transform.position);
            parent.SpawnVirusSingleAdjacent(target.key, transform.position);
        }
        if (spawned ||
            target.protein == Proteins.RNase_L ||
            target.protein == Proteins.PKR ||
            target.protein == Proteins.TRIM22 ||
            (target.protein == Proteins.IFIT && Random.Range(0.0f, 100.0f) > 90))
            parent.KillCell(target.key);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (target && collision.transform.parent && collision.transform.parent.transform == target.transform)
        {
            nextPosition = transform.position;
            collision.transform.GetComponent<Rotate>().enabled = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void MoveTo()
    {
        if (!selected)
        {
            //Get the direction of the player from the cell
            Vector3 heading = mainCamera.transform.position - transform.position;
            //Don't change y value
            heading.y = 0;
            //Find normalized direction
            float distance = Mathf.Max(heading.magnitude, .001f);
            Vector3 direction = heading / distance;
            if (direction.magnitude < 0.01f)
            {
                direction = new Vector3(0.0f, 0.0f, 1.0f);
            }
            //Scale it to 1.5
            direction *= scaledDistance;

            Vector3 finalPos = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);

            transform.GetChild(0).transform.LookAt(finalPos);

            //This is the new target position
            mainCamera.SetDestination(finalPos);
            parent.SetSelected(key);
            ToggleUI(true);
            parent.viewingStats = true;
        }
    }

    public void ToggleUI(bool b)
    {
        if (b)
        {
            if (!attack || !speed || !immunity)
            {
                TMPro.TextMeshPro[] arr = GetComponentsInChildren<TMPro.TextMeshPro>(true);
                attack = arr[0];
                speed = arr[1];
                immunity = arr[2];

                Debug.Log("Virus TextMesh Set");
            }
            attack.text = "Attack: " + (int)(attackValue * 100);
            speed.text = "Speed: " + (int)(turnSpeed * 100);
            immunity.text = "Immunity To Kill: " + Mathf.CeilToInt(health);
        }
        selected = b;
        transform.GetChild(0).gameObject.SetActive(b);
    }

    public void OnDestroy()
    {
        if (target)
        {
            target.hosted = false;
            target.targeted = false;
            target.transform.GetChild(1).GetComponent<Rotate>().enabled = true;
        }
        parent.virusKills++;
        parent.viruses.Remove(this);
    }
}