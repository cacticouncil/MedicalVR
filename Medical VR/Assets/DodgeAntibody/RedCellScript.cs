using UnityEngine;
using System.Collections;

public class RedCellScript : MonoBehaviour {

    // Use this for initialization
    public float speed;
    public GameObject virus, spawner;
    Vector3 oringPos;
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
            virus.GetComponent<MovingCamera>().WinresetPos();
            virus.GetComponent<MovingCamera>().speed++;
            spawner.GetComponent<AnitbodySpawnerScript>().GenerateObstacles();
        }
    }
}
