using UnityEngine;
using System.Collections;
using System;

public class VirusPlayer : MonoBehaviour
{
    public GameObject TimerText;
    public float TimeLeft;
    public float Speed;
	void Start ()
    {
        TimeLeft = 60.0f;
        Speed = .01f;
	}
	

	void Update ()
    {
        TimeLeft -= Time.deltaTime;
        TimerText.GetComponent<TextMesh>().text = "Timer: " + TimeLeft.ToString("f0");

        if (TimeLeft <= 0.0f)
        {
            TimeLeft = 0.0f;
        }
	}

    void FixedUpdate()
    {
        transform.position += transform.forward * Speed;
        GetComponent<Rigidbody>().velocity *= Speed;
    }
}

