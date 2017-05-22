using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TRotator : MonoBehaviour
{
    public float timeMultiplier;

    private void FixedUpdate()
    {
        transform.Rotate(0, Time.deltaTime * timeMultiplier, 0);
    }
}