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
      
    }
    void FixedUpdate()
    {
        transform.position += transform.forward * speed;
        //AvoidBack();
    }

    void AvoidBack()
    {
 

        
        if (transform.rotation.eulerAngles.y > 90)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90, transform.rotation.eulerAngles.z);

      
        if (transform.rotation.eulerAngles.y < -90)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -90, transform.rotation.eulerAngles.z);
     
    }
}
