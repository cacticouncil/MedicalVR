using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    public float speed;
    private float startTime;
    private float distance;
    private Vector3 position;

	// Use this for initialization
	void Start () {
        position = transform.position;
        distance = Vector3.Distance(transform.position, position);
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float disCovered = (Time.time - startTime) * speed;
        float fracJourney = disCovered / distance;
        transform.position = Vector3.Lerp(transform.position, position, fracJourney);
	}

    public void SetDestination(Vector3 endpoint)
    {
        position = new Vector3(endpoint.x, transform.position.y, endpoint.z);
        distance = Vector3.Distance(transform.position, position);
        startTime = Time.time;
    }
}