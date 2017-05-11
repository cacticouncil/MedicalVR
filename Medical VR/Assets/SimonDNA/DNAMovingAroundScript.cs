using System.Collections;using System.Collections.Generic;using UnityEngine;public class DNAMovingAroundScript : MonoBehaviour{    private Rigidbody rb;
    public float speed = 1;
    Vector3 velocity;

    void Start ()    {
        rb = transform.GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
        velocity = rb.velocity;
    }	//	void FixedUpdate ()//    {//        //       // transform.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * Time.fixedDeltaTime);
//    }    private void OnCollisionEnter(Collision collision)
    {
            rb.velocity = rb.velocity.normalized * speed;
    }
    private void OnCollisionExit(Collision collision)
    {
            rb.velocity = rb.velocity.normalized * speed;
    }

    private void OnCollisionStay(Collision collision)
    {
            rb.velocity = rb.velocity.normalized * speed;
    }


    //public void Reflect()
    //{
    //    transform.GetComponent<Rigidbody>().velocity = Vector3.Reflect(this.GetComponent<Rigidbody>().velocity, Vector3.right);
    //}
}