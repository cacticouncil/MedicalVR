using UnityEngine;
using System.Collections;

public class MovesCamera : MonoBehaviour
{
	void Start ()
    {
	
	}
	
	void Update ()
    {
        transform.position += transform.forward * .10f;
    }
}
