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
        transform.position = col.transform.position + new Vector3(20, 0, 0);
    }
}
