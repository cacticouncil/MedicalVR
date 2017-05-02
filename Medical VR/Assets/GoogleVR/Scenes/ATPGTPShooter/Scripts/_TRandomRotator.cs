using UnityEngine;
using System.Collections;

public class _TRandomRotator : MonoBehaviour
{
    public float tumble;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
        rb.maxAngularVelocity = 3;
    }
}
