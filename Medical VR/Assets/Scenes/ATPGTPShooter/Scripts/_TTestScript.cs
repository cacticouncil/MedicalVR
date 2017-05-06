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
