using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    public GameObject VirusCube;
    public GameObject BigVirusCube;
    public List<GameObject> VirusList = new List<GameObject>();
    int VirusCount;
    Vector3 SpawnRandomVirus;

    void Start()
    {
        InvokeRepeating("Spawn", 2.5f, 2.5f);
        VirusCount = 10;

        for (int i = 0; i < VirusCount; i++)
        {
            SpawnRandomVirus = Random.onUnitSphere * 16.0f;
            VirusList.Add(Instantiate(VirusCube, SpawnRandomVirus, Quaternion.identity, transform) as GameObject);
        }
    }

    void Spawn()
    {
        SpawnRandomVirus = Random.onUnitSphere * 16.0f;
        VirusList.Add(Instantiate(VirusCube, SpawnRandomVirus, Quaternion.identity, transform) as GameObject);
    }
}
