using UnityEngine;
using System.Collections;

public class ProteinScript : MonoBehaviour 
{
    float Speed;

	void Start ()
    {
        Speed = .1f;
	}
	

	void Update ()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);
    }
}
