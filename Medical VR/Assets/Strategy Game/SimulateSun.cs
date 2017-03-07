using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class SimulateSun : MonoBehaviour
{
    public bool x, y, z;
    public float degrees;
    public float transitionTime;

    private float startingTime;
    private Vector3 startingRotation, targetRotation;

    // Use this for initialization
    void Start()
    {
        startingRotation = targetRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {
        if (transform.rotation.eulerAngles != targetRotation)
            transform.eulerAngles = Vector3.Lerp(startingRotation, targetRotation, (Time.time - startingTime) / transitionTime);
    }

    // Update is called once per frame
    public void TurnUpdate()
    {
        startingRotation = transform.rotation.eulerAngles;
        if (x)
        {
            targetRotation.x += degrees;
        }
        if (y)
        {
            targetRotation.y += degrees;
        }
        if (z)
        {
            targetRotation.z += degrees;
        }
        startingTime = Time.time;
    }
}
