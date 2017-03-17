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
    bool StartCheckingWaves;
    void Start()
    {
        Wave1 = true;
        Wave2 = false;
        Wave3 = false;
        Wave4 = false;
        doWave = false;
        StartCheckingWaves = false;
        StartCoroutine(CreateWave(6));
    }

    void Update()
    {
        if (StartCheckingWaves == true)
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
                StartCheckingWaves = false;
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
                StartCheckingWaves = false;
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
                StartCheckingWaves = false;
                doWave = false;
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


    IEnumerator CreateWave(int Count)
    {
        VirusList = new List<GameObject>();

        if (Wave1 == true || Wave2 == true || Wave3 == true)
        {
            yield return new WaitForSeconds(5.0f);
            for (int i = 0; i < Count; i++)
            {
                SpawnRandomVirus = Random.onUnitSphere * 7.0f;
                VirusList.Add(Instantiate(VirusCube, SpawnRandomVirus, Quaternion.identity, transform) as GameObject);
            }
        }

        else if (Wave4 == true)
        {
            yield return new WaitForSeconds(5.0f);
            VirusList.Add(Instantiate(BossCube, BossSpawn.transform.position, Quaternion.identity, transform) as GameObject);
        }

        StartCheckingWaves = true;
    }

    public void CreateBigVirus(GameObject pos)
    {
        VirusList.Add(Instantiate(BigVirusCube, pos.transform.position, Quaternion.identity, transform) as GameObject);
    }
}
