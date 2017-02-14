using UnityEngine;
using System.Collections;

public class TimeInputObject : MonoBehaviour, TimedInputHandler {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HandleTimeInput()
    {

        if (GetComponent<Renderer>().material.color == Color.red)
            GetComponent<Renderer>().material.color = Color.green;
        else if (GetComponent<Renderer>().material.color == Color.green)
            GetComponent<Renderer>().material.color = Color.blue;
        else
            GetComponent<Renderer>().material.color = Color.red;
    }
}
