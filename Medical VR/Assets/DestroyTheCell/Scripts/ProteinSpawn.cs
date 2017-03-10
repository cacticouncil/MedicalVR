using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProteinSpawn : MonoBehaviour
{
    public GameObject Cell;
    public GameObject Protein;
    public GameObject Player;
    public List<GameObject> ProteinList = new List<GameObject>();
    public int ProteinCount;
    Vector3 ProteinLocation;

    void Start()
    {
        ProteinCount = 15;
        for (int i = 0; i < ProteinCount; i++)
        {
            ProteinLocation = Random.onUnitSphere * 6.3f;
            ProteinList.Add(Instantiate(Protein, ProteinLocation, Quaternion.identity, transform) as GameObject);
        }
    }

    void Update ()
    {

	}
}
