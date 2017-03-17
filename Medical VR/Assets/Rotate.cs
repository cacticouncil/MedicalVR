using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	public bool x, y, z;
    public Vector3 rotation;

    void Update()
	{
		if (x)
			transform.Rotate(new Vector3(Time.deltaTime * rotation.x, 0, 0));
		if (y)
			transform.Rotate(new Vector3(0, Time.deltaTime * rotation.y, 0));
		if (z)
			transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotation.z));
    }
}
