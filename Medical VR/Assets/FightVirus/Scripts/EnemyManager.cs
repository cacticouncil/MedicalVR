using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    public GameObject VirusCube;
    public List<GameObject> VirusList = new List<GameObject>();

    void Start ()
    {
        GameObject CellManager = GameObject.Find("CellSpawn");
        InvokeRepeating("Spawn", 4.0f, 4.0f);
	}
	
    void Spawn()
    {
        VirusList.Add(Instantiate(VirusCube, Camera.main.GetComponent<Camera>().transform.position, Quaternion.identity, transform) as GameObject);
    }
}
