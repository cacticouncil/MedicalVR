using UnityEngine;
using System.Collections;

public class MovingCamera : MonoBehaviour {

    public float speed;
    // Use this for initialization
    Vector3 originPos;
   public void resetPos()
    {
        transform.position = originPos;
    }
	void Start ()
    {
        originPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        float x = (transform.position.x + transform.forward.x); 
        float y = (transform.position.y + transform.forward.y); 
        float z = (transform.position.z + transform.forward.z);
        //GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        Vector3 newPos = new Vector3(x,y,z);
        GetComponent<Rigidbody>().MovePosition(newPos);

    }
}
