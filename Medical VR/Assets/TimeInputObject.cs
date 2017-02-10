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
        GetComponent<Renderer>().material.color = Color.red;
    }
}
