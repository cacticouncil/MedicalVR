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
    int EnemyCount;
    bool CheckCount;
    void Start()
    {
        Wave1 = true;
        Wave2 = false;
        Wave3 = false;
        Wave4 = false;
        CheckCount = false;
        EnemyCount = 20;
        Invoke("CreateWave", 5);
    }

    void Update()
    {
        if (CheckCount == true)
        {
            if (VirusList.Count == 0 && Wave1 == true)
            {
                Wave1 = false;
                Wave2 = true;
                CheckCount = false;
                EnemyCount = 25;
                Invoke("CreateWave", 5);
            }

            else if (VirusList.Count == 0 && Wave2 == true)
            {
                Wave2 = false;
                Wave3 = true;
                CheckCount = false;
                EnemyCount = 35;
                Invoke("CreateWave", 5);
            }

            else if (VirusList.Count == 0 && Wave3 == true)
            {
                Wave3 = false;
                Wave4 = true;
                CheckCount = false;
                Invoke("CreateWave", 5);
            }
        }

        for (int i = 0; i < VirusList.Count; i++)
        {
            if (VirusList[i].gameObject == null)
            {
                VirusList.Remove(VirusList[i]);
            }
        }
    }

    void CreateWave()
    {
        VirusList = new List<GameObject>();

        if (Wave1 == true || Wave2 == true || Wave3 == true)
        {
            for (int i = 0; i < EnemyCount; i++)
            {
                SpawnRandomVirus = Random.onUnitSphere * 7.0f;
                VirusList.Add(Instantiate(VirusCube, SpawnRandomVirus, Quaternion.identity, transform) as GameObject);
            }
            CheckCount = true;
        }

        else if (Wave4 == true)
        {
            VirusList.Add(Instantiate(BossCube, BossSpawn.transform.position, Quaternion.identity, transform) as GameObject);
            CheckCount = false;
        }
    }

    public void CreateBigVirus(GameObject pos)
    {
        VirusList.Add(Instantiate(BigVirusCube, pos.transform.position, Quaternion.identity, transform) as GameObject);
    }

    public void CreateSmallVirus(GameObject pos)
    {
        VirusList.Add(Instantiate(VirusCube, pos.transform.position, Quaternion.identity, transform) as GameObject);
        VirusList[VirusList.Count - 1].GetComponent<Virus>().BossSpawnSmallVirus = true;
    }
}
