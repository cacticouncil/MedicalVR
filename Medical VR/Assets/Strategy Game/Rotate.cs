using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public Vector3 rotation = new Vector3(-15, 10, 5);

    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime, Space.World);
        //transform.Rotate(new Vector3(Time.deltaTime * rotation.x, Time.deltaTime * rotation.y, Time.deltaTime * rotation.z));
    }
}
