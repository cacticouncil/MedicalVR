using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VirusManager : MonoBehaviour
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
        Wave1 = true;
        Wave2 = false;
        Wave3 = false;
        Wave4 = false;
        doWave = false;

        CreateWave(6);
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

        for (int i = 0; i < VirusList.Count; i++)
        {
            if (VirusList[i].gameObject == null)
            {
                VirusList.Remove(VirusList[i]);
            }
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

    public void CreateBigVirus(GameObject pos)
    {
        VirusList.Add(Instantiate(BigVirusCube, pos.transform.position, Quaternion.identity, transform) as GameObject);
    }
}
