using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    public float speed;
    private float startTime;
    private float distance;
    private Vector3 startPosition;
    private Vector3 endPosition;

    // Use this for initialization
    void Start()
    {
        startPosition = endPosition = transform.position;
        distance = .001f;
        startTime = Time.time;
        GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Move(Vector3 pos)
    {
        //Heading towards position
        Vector3 heading = pos - transform.position;
        //Find normalized direction
        float distance = Mathf.Max(heading.magnitude, .001f);
        Vector3 direction = heading / distance;
        if (direction.magnitude < 0.01f)
        {
            direction = new Vector3(0.0f, 0.0f, 1.0f);
        }

        direction *= speed;

        Vector3 finalPos = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);

        transform.GetChild(0).transform.LookAt(finalPos);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float disCovered = (Time.time - startTime) * speed;
        float fracJourney = disCovered / distance;
        if (fracJourney <= 1.0f)
        {
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(startPosition, endPosition, fracJourney));
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    public void SetDestination(Vector3 endpoint)
    {
        endPosition = endpoint;
        startPosition = transform.position;
        distance = Mathf.Max(Vector3.Distance(startPosition, endPosition), .001f);
        startTime = Time.time;
    }
}