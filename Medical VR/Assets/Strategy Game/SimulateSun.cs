using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class SimulateSun : MonoBehaviour
{
    bool x, y, z, w;
    public float degrees;
    public float transitionTime;

    private float startingTime;
    private Quaternion startingRotation, targetRotation;

    // Use this for initialization
    void Start()
    {
        startingRotation = targetRotation = transform.rotation;
    }

    void Update()
    {
        if (transform.rotation != targetRotation)
            transform.rotation = Quaternion.Lerp(startingRotation, targetRotation, Time.time - startingTime / transitionTime);
    }

    // Update is called once per frame
    public void TurnUpdate()
    {
        startingRotation = transform.rotation;
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
        if (w)
        {
            targetRotation.w += degrees;
        }
        startingTime = Time.time;
    }
}
