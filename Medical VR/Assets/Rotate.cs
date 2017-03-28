using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public bool x, y, z;
    public Vector3 rotation;
    private Rigidbody rig;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (x && y && z)
        {
            transform.Rotate(new Vector3(Time.deltaTime * rotation.x, Time.deltaTime * rotation.y, Time.deltaTime * rotation.z));
        }
        else
        {
            if (x)
                transform.Rotate(new Vector3(Time.deltaTime * rotation.x, 0, 0));
            if (y)
                transform.Rotate(new Vector3(0, Time.deltaTime * rotation.y, 0));
            if (z)
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotation.z));
        }
    }
}
