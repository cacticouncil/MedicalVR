using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DNAMovingAroundScript : MonoBehaviour
{
    private Rigidbody RB;
    public float speed = 1;

    void Start ()
    {
        RB = transform.GetComponent<Rigidbody>();
        RB.velocity = transform.forward * speed;
    }
	
    private void OnCollisionEnter(Collision collision)
    {
            RB.velocity = RB.velocity.normalized * speed;
    }
    private void OnCollisionExit(Collision collision)
    {
            RB.velocity = RB.velocity.normalized * speed;
    }

    private void OnCollisionStay(Collision collision)
    {
            RB.velocity = RB.velocity.normalized * speed;
    }

    //public void Reflect()
    //{
    //    transform.GetComponent<Rigidbody>().velocity = Vector3.Reflect(this.GetComponent<Rigidbody>().velocity, Vector3.right);
    //}
}
