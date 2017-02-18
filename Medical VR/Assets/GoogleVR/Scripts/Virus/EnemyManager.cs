using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject VirusCube;
    public Transform[] enemies;

	void Start ()
    {
        InvokeRepeating("Spawn", 3.5f, 3.5f);
	}
	
    void Spawn()
    {
        Instantiate(VirusCube, Camera.main.GetComponent<Camera>().transform.position, Quaternion.identity);
    }
}
