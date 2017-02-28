using UnityEngine;
using System.Collections;

public class RedCellScript : MonoBehaviour {

    // Use this for initialization
    public float speed;
    Vector3 oringPos;
    public GameObject virus, spawner;
	void Start ()
    {
        oringPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "virus")
        {
            virus.GetComponent<MovingCamera>().resetPos();
            virus.GetComponent<MovingCamera>().speed++;
            spawner.GetComponent<AnitbodySpawnerScript>().GenerateObstacles();
        }
    }
}
