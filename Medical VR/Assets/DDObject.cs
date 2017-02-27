using UnityEngine;
using System.Collections;

public class DDObject : MonoBehaviour, DragDropHandler {

    private bool isHeld;
    private GameObject Recticle;
    private Color store;  
	// Use this for initialization
	void Start ()
    {
        isHeld = false;
        store = GetComponent<Renderer>().material.color;
        Recticle = GameObject.Find("DDReticle");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isHeld)
        {
            Ray ray = new Ray(Recticle.transform.position, Recticle.transform.forward);
            transform.position = ray.GetPoint(50);
        }
    }

    void DragDropHandler.HandleGazeTriggerStart()
    {
        isHeld = true;
        GetComponent<Renderer>().material.color = Color.blue;
    }

    void DragDropHandler.HandleGazeTriggerEnd()
    {
        isHeld = false;
        GetComponent<Renderer>().material.color = store;
    }
}
