using UnityEngine;
using System.Collections;

public class RedCellScript : MonoBehaviour {

    // Use this for initialization
    public float speed;
    Vector3 oringPos;
	void Start () {
        oringPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
	}
}
