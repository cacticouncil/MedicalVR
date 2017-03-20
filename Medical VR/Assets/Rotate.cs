using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public Vector3 rotation;
	public Color col;

    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * rotation.x, Time.deltaTime * rotation.y, Time.deltaTime * rotation.z));
		//GetComponent<Renderer> ().material.color = col;
    }
}
