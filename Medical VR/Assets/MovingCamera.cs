using UnityEngine;
using System.Collections;

public class MovingCamera : MonoBehaviour {

    public GameObject redCell;
    public float speed;
    // Use this for initialization
    Vector3 originPos;
    Vector3 redOrPos;
   public void resetPos()
    {
        transform.position = originPos;
        redCell.transform.position = redOrPos;
    }
	void Start ()
    {
        originPos = transform.position;
        redOrPos = redCell.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //float x = (transform.position.x + transform.forward.x) * speed; 
        //float y = (transform.position.y + transform.forward.y) * speed; 
        //float z = (transform.position.z + transform.forward.z) * speed;
        ////GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        //Vector3 newPos = new Vector3(x,y,z);
        //GetComponent<Rigidbody>().MovePosition(newPos);

    }
    void FixedUpdate()
    {
        transform.position += transform.forward * speed;
    }
}
