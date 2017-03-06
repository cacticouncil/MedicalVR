using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellMembrane : MonoBehaviour
{
    public GameObject Protein;
    public List<GameObject> ProteinList = new List<GameObject>();
    Vector3 SpawnProtein;

    void Start()
    {
        InvokeRepeating("SpawnProteins", 2.5f, 2.5f);
    }

    void SpawnProteins()
    {
        SpawnProtein.x = Random.Range(-5, 5);
        SpawnProtein.y = -2;
        SpawnProtein.z = Random.Range(-5, 5);

        ProteinList.Add(Instantiate(Protein, SpawnProtein, Quaternion.identity) as GameObject);
    }
}
