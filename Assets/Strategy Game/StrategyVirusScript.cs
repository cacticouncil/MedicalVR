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
            target = StrategyCellManagerScript.instance.FindVirusNewTarget(this);
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

                nextPosition = Vector3.Lerp(prevPosition, StrategyCellManagerScript.instance.RandomPositionAboveHex(), percentTraveled);
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
                    target = StrategyCellManagerScript.instance.FindVirusNewTarget(this);
                    return;
                }
            }
            GetComponent<Collider>().isTrigger = true;
            target.hosted = true;
            target.virus = this;
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
                    return;
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

    void CheckCollision(Transform collision)
    {
        if (target && collision.transform.GetInstanceID() == target.render.transform.GetInstanceID())
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

    void OnCollisionEnter(Collision collision)
    {
        CheckCollision(collision.transform);
    }

    void OnTriggerEnter(Collider collision)
    {
        CheckCollision(collision.transform);
    }

    public void MoveTo()
    {
        if (!selected)
        {
            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0;
            direction.Normalize();
            direction *= scaledDistance;
            if (direction.magnitude == 0)
            {
                direction = new Vector3(1.3f, 0, 0);
            }
            Vector3 finalPos = transform.position - direction;
            transform.GetChild(0).transform.LookAt(finalPos);

            //This is the new target position
            MoveCamera.instance.SetDestination(finalPos);
            StrategyCellManagerScript.instance.SetSelected(key);
            ToggleUI(true);
            StrategyCellManagerScript.instance.viewingStats = true;
        }
    }

    public void UseV()
    {
        //check for item
        if (StrategyCellManagerScript.instance.inventory[6].count > 0)
        {
            StrategyCellManagerScript.instance.inventory[6].count--;
            ToggleUI(false);
            Vector3 camPos = Camera.main.GetComponent<Transform>().position;
            Vector3 dir = camPos - transform.position;
            dir.Normalize();
            MoveCamera.instance.SetDestination(camPos + dir);
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
            powerup.text = StrategyCellManagerScript.instance.inventory[6].count.ToString();
            if (StrategyCellManagerScript.instance.inventory[6].count < 1)
                powerup.color = Color.red;
            else
                powerup.color = Color.white;
        }
        selected = b;
        transform.GetChild(0).gameObject.SetActive(b);
    }

    //This virus kills the cell it is attacking and itself, then spawns 2 viruses
    public virtual void Attack()
    {
        bool spawned = false;
        if (target.protein != Proteins.TRIM22 && target.protein != Proteins.IFIT)
        {
            spawned = true;
            StrategyCellManagerScript.instance.SpawnVirusSingleAdjacent(target.key, transform.position);
            StrategyCellManagerScript.instance.SpawnVirusSingleAdjacent(target.key, transform.position);
        }
        if (spawned || target.protein == Proteins.TRIM22 || (target.protein == Proteins.IFIT && Random.Range(0.0f, 100.0f) < 20))
            StrategyCellManagerScript.instance.KillCell(target.key);
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
        if (StrategyCellManagerScript.instance)
        {
            StrategyCellManagerScript.instance.virusKills++;
            StrategyCellManagerScript.instance.viruses.Remove(this);
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
}