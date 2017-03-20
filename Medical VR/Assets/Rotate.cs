using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public bool x, y, z;
    public Vector3 rotation;

    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * rotation.x, Time.deltaTime * rotation.y, Time.deltaTime * rotation.z));
		//GetComponent<Renderer> ().material.color = col;
    }
}
