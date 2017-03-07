using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject VirusCube;
    public GameObject BigVirusCube;
    public GameObject BossCube;
    public GameObject BossSpawn;
    public GameObject Player;
    public GameObject VirusLocations;
    public List<GameObject> VirusList;
    Vector3 SpawnRandomVirus;

    public bool Wave1;
    public bool Wave2;
    public bool Wave3;
    public bool Wave4;

    bool doWave;
    void Start()
    {
        //InvokeRepeating("Spawn", 2.5f, 2.5f);
        Wave1 = false;
        Wave2 = false;
        Wave3 = false;
        Wave4 = true;
        doWave = false;

        CreateWave(1);
    }

    void Update()
    {
        if (VirusList.Count == 0 && Wave1 == true)
        {
            Wave1 = false;
            Wave2 = true;
            doWave = true;
        }

        if (Wave2 == true && doWave == true)
        {
            CreateWave(1);
            doWave = false;
        }

        if (VirusList.Count == 0 && Wave2 == true)
        {
            Wave2 = false;
            Wave3 = true;
            doWave = true;
        }

        if (Wave3 == true && doWave == true)
        {
            CreateWave(1);
            doWave = false;
        }

        if (VirusList.Count == 0 && Wave3 == true)
        {
            Wave3 = false;
            Wave4 = true;
            doWave = true;
        }

        if (Wave4 == true && doWave == true)
        {
            CreateWave(1);
            doWave = false;
        }
    }

    void CreateWave(int Count)
    {
        VirusList = new List<GameObject>();


        if (Wave1 == true || Wave2 == true || Wave3 == true)
        {
            for (int i = 0; i < Count; i++)
            {
                SpawnRandomVirus = Random.onUnitSphere * 7.0f;
                VirusList.Add(Instantiate(VirusCube, SpawnRandomVirus, Quaternion.identity, transform) as GameObject);
            }
        }

        else if (Wave4 == true)
        {
            VirusList.Add(Instantiate(BossCube, BossSpawn.transform.position, Quaternion.identity, transform) as GameObject);
        }
    }

    //void Spawn()
    //{
    //    SpawnRandomVirus = Random.onUnitSphere * 13.0f;
    //    VirusList.Add(Instantiate(VirusCube, SpawnRandomVirus, Quaternion.identity, transform) as GameObject);
    //}
}
