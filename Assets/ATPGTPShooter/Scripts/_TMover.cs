using UnityEngine;
using System.Collections;

public class _TMover : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public bool keepVelocity;
    public bool isRandomUp;
    public Boundary spawnValuesDirection;

    [HideInInspector]
    public bool DataIsSet = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!isRandomUp)
        {
            rb.velocity = transform.forward * speed;
        }
        else
        {
            rb.velocity = new Vector3(
                Random.Range(spawnValuesDirection.xMin, spawnValuesDirection.xMax),
                Random.Range(spawnValuesDirection.yMin, spawnValuesDirection.yMax),
                Random.Range(spawnValuesDirection.zMin, spawnValuesDirection.zMax)) * speed;
        }
        DataIsSet = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (keepVelocity && rb)
            rb.velocity = rb.velocity.normalized * speed;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (keepVelocity && rb)
            rb.velocity = rb.velocity.normalized * speed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (keepVelocity && rb)
            rb.velocity = rb.velocity.normalized * speed;
    }
}