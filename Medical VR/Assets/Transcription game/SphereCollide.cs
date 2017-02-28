using UnityEngine;
using System.Collections;

public class SphereCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "A")
        {
            Destroy(gameObject);
        }
        if (col.tag == "C")
        {
            Destroy(gameObject);
        }
        if (col.tag == "G")
        {
            Destroy(gameObject);
        }
        if (col.tag == "T")
        {
            Destroy(gameObject);
        }
        if (col.tag == "U")
        {
            Destroy(gameObject);
        }
    }
}
