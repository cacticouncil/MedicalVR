using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    public GameObject VirusCube;
    public List<GameObject> VirusList = new List<GameObject>();
    Vector3 Pos;
    void Start ()
    {
        GameObject CellManager = GameObject.Find("CellSpawn");
        InvokeRepeating("Spawn", 2.5f, 2.5f);
        Pos = Camera.main.GetComponent<Camera>().transform.position;
	}
	
    void Spawn()
    {
        VirusList.Add(Instantiate(VirusCube, Pos = new Vector3(Pos.x, Pos.y + .8f, Pos.z), Quaternion.identity, transform) as GameObject);
    }
}
