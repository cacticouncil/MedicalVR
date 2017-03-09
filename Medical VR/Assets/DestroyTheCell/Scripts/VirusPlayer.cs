using UnityEngine;
using System.Collections;

public class VirusPlayer : MonoBehaviour
{
    public GameObject TimerText;
    public float TimeLeft;

	void Start ()
    {
        TimeLeft = 60.0f;
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
}
