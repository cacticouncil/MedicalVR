using UnityEngine;
using System.Collections;

public class CollectiblePoints : MonoBehaviour {

    public int score;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
