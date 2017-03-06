using UnityEngine;
using System.Collections;

public class TimeInputObject : MonoBehaviour, TimedInputHandler {

    public GameObject memoryui;
    public GameObject sphere;
    private Vector3 pos;
    public bool IsCorrect;
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

    public void HandleTimeInput()
    {

        if ((sphere.GetComponent<Randomsphere>().clonei.tag == "A" && gameObject.tag == "U") || (sphere.GetComponent<Randomsphere>().clonei.tag == "G" && gameObject.tag == "C") || (sphere.GetComponent<Randomsphere>().clonei.tag == "C" && gameObject.tag == "G") || (sphere.GetComponent<Randomsphere>().clonei.tag == "T" && gameObject.tag == "A") /*|| (sphere.GetComponent<Randomsphere>().clonei.tag == "T" && gameObject.tag == "A2")*/)
        {
            transform.position = new Vector3(-20, -10, 30);
            memoryui.GetComponent<MemoryUI>().score += 100;
            GetComponent<Renderer>().material.color = Color.green;
            IsCorrecti = true;
        }
        else
            IsCorrecti = false;

        if ((sphere.GetComponent<Randomsphere>().clonej.tag == "A" && gameObject.tag == "U") || (sphere.GetComponent<Randomsphere>().clonej.tag == "G" && gameObject.tag == "C") || (sphere.GetComponent<Randomsphere>().clonej.tag == "C" && gameObject.tag == "G"))
        {
            transform.position = new Vector3(-5, -10, 30);
            memoryui.GetComponent<MemoryUI>().score += 100;
            GetComponent<Renderer>().material.color = Color.green;
            IsCorrectj = true;
        }
        else
            IsCorrectj= false;

        if ((sphere.GetComponent<Randomsphere>().clonek.tag == "C" && gameObject.tag == "G") || (sphere.GetComponent<Randomsphere>().clonek.tag == "T2" && gameObject.tag == "A2") /*|| (sphere.GetComponent<Randomsphere>().clonek.tag == "T2" && gameObject.tag == "A")*/)
        {
                transform.position = new Vector3(10, -10, 30);

            memoryui.GetComponent<MemoryUI>().score += 100;
            GetComponent<Renderer>().material.color = Color.green;
            IsCorrectk = true;
        }
        else
            IsCorrectk= false;

        if (IsCorrecti == true || IsCorrectj == true || IsCorrectk == true)
            IsCorrect = true;
        else
            IsCorrect = false;

        if (IsCorrect)
        {
            memoryui.GetComponent<MemoryUI>().Level += 1;
            memoryui.GetComponent<MemoryUI>().score += 150;
        }
        else
        {
            memoryui.GetComponent<MemoryUI>().score -= 200;
            GetComponent<Renderer>().material.color = Color.red;
            memoryui.GetComponent<MemoryUI>().LoseresetPos();
            transform.position = pos;
        }
    }
}
