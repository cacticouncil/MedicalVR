using UnityEngine;
using System.Collections;

public class TimeInputObject : MonoBehaviour, TimedInputHandler {

    public GameObject memoryui;
    public GameObject sphere;
    private Vector3 pos;
    private bool IsCorrect;
    private bool IsCorrecti;
    private bool IsCorrectj;
    private bool IsCorrectk;
    // Use this for initialization
    void Start () {
        pos = transform.position;
        IsCorrect = false;
        IsCorrecti = false;
        IsCorrectj = false;
        IsCorrectk = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider col)
    {
            //col.transform.position = transform.position + new Vector3(20, 0, 0);
    }

    public void HandleTimeInput()
    {

        //if(transform.position != new Vector3(-20, -10, 30))
        //transform.position = new Vector3(-20, -10, 30);
        //else if (transform.position == new Vector3(-20, 0, 30))
        //    transform.position = new Vector3(-5, -10, 30);
        //else if(transform.position == new Vector3(-5, -10, 30))
        //    transform.position = new Vector3(10, -10, 30);

        if ((sphere.GetComponent<Randomsphere>().clonei.tag == "A" && gameObject.tag == "U") || (sphere.GetComponent<Randomsphere>().clonei.tag == "G" && gameObject.tag == "C") || (sphere.GetComponent<Randomsphere>().clonei.tag == "C" && gameObject.tag == "G") || (sphere.GetComponent<Randomsphere>().clonei.tag == "T" && gameObject.tag == "A"))
        {
            transform.position = new Vector3(-20, -10, 30);
            memoryui.GetComponent<MemoryUI>().score += 10;
            IsCorrecti = true;
        }
        else
            IsCorrecti = false;

        if ((sphere.GetComponent<Randomsphere>().clonej.tag == "A" && gameObject.tag == "U") || (sphere.GetComponent<Randomsphere>().clonej.tag == "G" && gameObject.tag == "C") || (sphere.GetComponent<Randomsphere>().clonej.tag == "C" && gameObject.tag == "G"))
        {
            transform.position = new Vector3(-5, -10, 30);
            memoryui.GetComponent<MemoryUI>().score += 10;
            IsCorrectj = true;
        }
        else
            IsCorrectj= false;

        if ((sphere.GetComponent<Randomsphere>().clonek.tag == "C" && gameObject.tag == "G") || (sphere.GetComponent<Randomsphere>().clonek.tag == "T" && gameObject.tag == "A"))
        {
            transform.position = new Vector3(10, -10, 30);
            memoryui.GetComponent<MemoryUI>().score += 10;
            IsCorrectk = true;
        }
        else
            IsCorrectk= false;

        if (IsCorrecti == true || IsCorrectj == true || IsCorrectk)
            IsCorrect = true;
        else
            IsCorrect = false;

        if (IsCorrect)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
            memoryui.GetComponent<MemoryUI>().LoseresetPos();
            transform.position = pos;
        }
    }
}
