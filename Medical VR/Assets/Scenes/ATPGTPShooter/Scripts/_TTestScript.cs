using UnityEngine;
using System.Collections;

public class _TTestScript : MonoBehaviour
{
    public float timeMultiplier;

    private void FixedUpdate()
    {
        transform.Rotate(0, Time.deltaTime * timeMultiplier, 0);
        //transform.Rotate(Vector3.right * Time.deltaTime);
    }

}

//public class _TTestScript : MonoBehaviour
//{
//    Rigidbody rb;
//
//    public float timeMultiplier;
//    private void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        transform.rotation = Random.rotation;
//
//    }
//
//    private void FixedUpdate()
//    {
//
//        float min = 0.3f;
//        float max = 0.7f;
//        rb.angularVelocity = Random.insideUnitSphere * timeMultiplier;
//        // transform.Rotate(Random.Range(min, max) * timeMultiplier, Random.Range(min, max) * timeMultiplier, Random.Range(min, max) * timeMultiplier);
//        //transform.Rotate(Vector3.right * Time.deltaTime);
//    }
//
//}
