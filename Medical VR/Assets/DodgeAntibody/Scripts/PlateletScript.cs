using UnityEngine;
using System.Collections;

public class PlateletScript : MonoBehaviour {

    public GameObject virus;
    public bool startTimer = false;
    float timer = 0;
    float orgSpeed = 0;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(startTimer == true)
        {
            timer += Time.deltaTime;
            virus.GetComponent<MovingCamera>().speed += Time.deltaTime;
            virus.GetComponent<MovingCamera>().score-= Time.smoothDeltaTime;
            if (timer >= 3)
            {
                virus.GetComponent<MovingCamera>().speed = orgSpeed;
                startTimer = false;
                timer = 0;
            }
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "virus")
        {
            virus.GetComponent<MovingCamera>().score -= 10;
            if (virus.GetComponent<MovingCamera>().score <= 0)
                virus.GetComponent<MovingCamera>().score = 0;
            orgSpeed = virus.GetComponent<MovingCamera>().speed;
            virus.GetComponent<MovingCamera>().speed = -3;
            startTimer = true;
        }
    }
}
