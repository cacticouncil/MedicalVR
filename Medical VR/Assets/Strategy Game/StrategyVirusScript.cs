using UnityEngine;
using System.Collections;

public class StrategyVirusScript : MonoBehaviour
{
    public StrategyCellScript target;
    public Renderer render;
    public ParticleSystem deathParticles;
    public GameObject itemWhiteBloodCellPrefab;
    public TMPro.TextMeshPro attack, speed, immunity, powerup;

    [System.NonSerialized]
    public StrategyCellManagerScript parent;
    [System.NonSerialized]
    public float turnSpeed = .1f, attackValue = .5f, attackDuration = 0, immunityToKill = 15.0f, percentTraveled = 0.0f;
    [System.NonSerialized]
    public bool standby = false, targeted = false, selected = false;

    private float movementSpeed = 70.0f;
    private float distance = .1f;
    private float startTime = 0.0f;
    private bool trav = false;
    private Vector3 startingPosition, prevPosition, nextPosition;
    private Vector2 key = new Vector2(-500, 500);
    private float scaledDistance = 1.3f;

    // Use this for initialization
    void Start()
    {
        startingPosition = prevPosition = nextPosition = transform.position;
        if (target)
        {
            target.targeted = true;
            distance = Mathf.Max(Vector3.Distance(startingPosition, target.transform.position), .001f);
        }
        else
        {
            distance = .001f;
            standby = true;
        }
        parent.viruses.Add(this);
        startTime = Time.time;
    }

    private IEnumerator Move()
    {
        while (!trav)
        {
            float disCovered = (Time.time - startTime) * movementSpeed;
            float fracJourney = disCovered / distance;

            if (fracJourney >= 1.0f && (!target || (target && percentTraveled >= 1.0f)))
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
            else
            {
                GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(prevPosition, nextPosition, fracJourney));
            }
            yield return new WaitForFixedUpdate();
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
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
                distance = Mathf.Max(Vector3.Distance(prevPosition, nextPosition), .001f);
                startTime = Time.time;
                StopCoroutine(Move());
                StartCoroutine(Move());
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
                if (target.immunity > immunityToKill)
                {
                    target.immunity -= immunityToKill;
                    StartCoroutine(Die());
                }
                if (attackDuration >= target.defense)
                {
                    Attack();
                }
            }
        }
        StopCoroutine(Move());
        StartCoroutine(Move());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (target && collision.transform.parent && collision.transform.parent.transform.GetComponent<StrategyCellScript>() && collision.transform.parent.transform.GetComponent<StrategyCellScript>().key == target.key)
        {
            collision.transform.GetComponent<Rotate>().enabled = false;
            transform.GetChild(1).GetComponent<Rotate>().enabled = false;
            nextPosition = transform.position;
            transform.parent = collision.transform;
            StopCoroutine(Move());
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void MoveTo()
    {
        if (!selected)
        {
            //Get the direction of the player from the cell
            Vector3 heading = Camera.main.transform.position - transform.position;
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
            Camera.main.transform.parent.GetComponent<MoveCamera>().SetDestination(finalPos);
            parent.SetSelected(key);
            ToggleUI(true);
            parent.viewingStats = true;
        }
    }

    public void UseV()
    {
        //check for item
        if (parent.inventory[6].count > 0)
        {
            parent.inventory[6].count--;
            ToggleUI(false);
            Vector3 camPos = Camera.main.GetComponent<Transform>().position;
            Vector3 dir = camPos - transform.position;
            dir.Normalize();
            Camera.main.transform.parent.GetComponent<MoveCamera>().SetDestination(camPos + dir);
            Invoke("SpawnW", .5f);
        }
    }

    void SpawnW()
    {
        GameObject w = Instantiate(itemWhiteBloodCellPrefab, Camera.main.GetComponent<Transform>().position, Quaternion.identity, transform.parent) as GameObject;
        w.GetComponent<StrategyItemWhiteBloodCell>().target = this;
        w.GetComponent<StrategyItemWhiteBloodCell>().enabled = true;
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
            attack.text = "Attack: " + (int)(attackValue * 10) * .1f;
            speed.text = "Speed: " + (int)(turnSpeed * 10) * .1f;
            immunity.text = "Immunity To Kill: " + Mathf.CeilToInt(immunityToKill);
            powerup.text = parent.inventory[6].count.ToString();
            if (parent.inventory[6].count < 1)
                powerup.color = Color.red;
            else
                powerup.color = Color.white;
        }
        GetComponent<Collider>().enabled = b;
        selected = b;
        transform.GetChild(0).gameObject.SetActive(b);
    }

    //This virus kills the cell it is attacking and itself, then spawns 2 viruses
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
            (target.protein == Proteins.IFIT && Random.Range(0.0f, 100.0f) > 75))
            parent.KillCell(target.key);
        StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        if (target)
        {
            target.hosted = false;
            target.targeted = false;
            target.transform.GetChild(1).GetComponent<Rotate>().enabled = true;
            target = null;
        }
        if (parent)
        {
            parent.virusKills++;
            parent.viruses.Remove(this);
            parent = null;
        }

        float startTime = Time.time;
        Color c = render.material.color;
        Color o = render.material.GetColor("_OutlineColor");

        deathParticles.Play();
        float t = 0;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.a = Mathf.Lerp(1, 0, t);
            render.material.color = c;
            o.a = Mathf.Lerp(1, 0, t);
            render.material.SetColor("_OutlineColor", o);
            yield return 0;
        }
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        if (target)
        {
            target.hosted = false;
            target.targeted = false;
            target.transform.GetChild(1).GetComponent<Rotate>().enabled = true;
        }
        if (parent)
        {
            parent.virusKills++;
            parent.viruses.Remove(this);
        }
    }
}