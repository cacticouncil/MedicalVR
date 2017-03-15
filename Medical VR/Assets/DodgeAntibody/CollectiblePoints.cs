using UnityEngine;
using System.Collections;

public class CollectiblePoints : MonoBehaviour {

    public int score;
    public GameObject Cam;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(transform.position, Cam.transform.position) < 3000)
        {
            GetComponent<Renderer>().enabled = true;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "virus")
        {
            other.GetComponent<MovingCamera>().score += score;
            Destroy(gameObject);
        }
    }
}
