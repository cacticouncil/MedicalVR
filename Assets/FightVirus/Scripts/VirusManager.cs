using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VirusManager : MonoBehaviour
{
    public GameObject VirusCube;
    public GameObject BigVirusCube;
    public GameObject BossCube;
    public GameObject BossSpawn;
    public GameObject FightVirusPlayer;
    public GameObject VirusLocations;
    public List<GameObject> VirusList;
    Vector3 SpawnRandomVirus;

    int EnemyCount;
    public bool CheckCount;

    public int WaveNumber;
    bool SpawnBoss = false;


    float SmallVirusSpeed;
    int SmallVirusHealth;

    float BigVirusSpeed;
    int BigVirusHealth;

    float BossVirusSpeed;
    public int BossVirusHealth;

    public bool CanISpawn = false;
    public bool DoneSpawning = false;

    void Start()
    {
        WaveNumber = 1;
        CheckCount = false;
    }

    void Update()
    {
        if (CheckCount == true && GlobalVariables.tutorial == false && VirusList.Count == 0)
        {
            switch (WaveNumber)
            {
                case 1:
                    if (GlobalVariables.arcadeMode == true)
                    {
                        EnemyCount = 10;
                        SmallVirusSpeed = 0.005f;
                        SmallVirusHealth = 20;
                        BigVirusSpeed = 0.002f;
                        BigVirusHealth = 50;
                    }
                    //Story Mode
                    else
                    {
                        EnemyCount = 5;
                        SmallVirusSpeed = 0.002f;
                        SmallVirusHealth = 20;
                        BigVirusSpeed = 0.001f;
                        BigVirusHealth = 40;
                    }
                    break;
                case 2:
                    if (GlobalVariables.arcadeMode == true)
                    {
                        EnemyCount = 15;
                        SmallVirusSpeed = 0.0075f;
                        SmallVirusHealth = 40;
                        BigVirusSpeed = 0.004f;
                        BigVirusHealth = 100;
                    }
                    //Story Mode
                    else
                    {
                        EnemyCount = 7;
                        SmallVirusSpeed = 0.003f;
                        SmallVirusHealth = 40;
                        BigVirusSpeed = 0.002f;
                        BigVirusHealth = 80;
                    }
                    break;
                case 3:
                    if (GlobalVariables.arcadeMode == true)
                    {
                        EnemyCount = 20;
                        SmallVirusSpeed = 0.01f;
                        SmallVirusHealth = 60;
                        BigVirusSpeed = 0.005f;
                        BigVirusHealth = 150;
                    }
                    //Story Mode
                    else
                    {
                        EnemyCount = 10;
                        SmallVirusSpeed = 0.004f;
                        SmallVirusHealth = 60;
                        BigVirusSpeed = 0.003f;
                        BigVirusHealth = 60;
                    }
                    break;
                case 4:
                    SpawnBoss = true;
                    SmallVirusSpeed = 0.0125f;
                    SmallVirusHealth = 20;
                    BossVirusSpeed = 0.3f;
                    BossVirusHealth = 500;
                    break;
                default:
                    break;
            }

            CheckCount = false;
            Invoke("CreateWave", 5);
        }

        if (GlobalVariables.tutorial == true)
        {
            if (CanISpawn == true)
            {
                switch (WaveNumber)
                {
                    case 1:
                        EnemyCount = 3;
                        SmallVirusSpeed = 0.006f;
                        SmallVirusHealth = 20;
                        BigVirusSpeed = 0.007f;
                        BigVirusHealth = 40;
                        Invoke("CreateWave", 2);
                        break;

                    case 2:
                        EnemyCount = 7;
                        SmallVirusSpeed = 0.002f;
                        SmallVirusHealth = 20;
                        BigVirusSpeed = 0.007f;
                        BigVirusHealth = 40;
                        Invoke("CreateWave", 2);
                        break;

                    default:
                        break;
                }
                CanISpawn = false;
            }
        }

        //This will clean up empty gameobjects
        for (int i = 0; i < VirusList.Count; i++)
        {
            if (VirusList[i].gameObject == null || VirusList[i].GetComponent<Virus>().Health <= 0)
                VirusList.Remove(VirusList[i]);
        }
    }

    void CreateWave()
    {
        VirusList = new List<GameObject>();

        if (SpawnBoss == false)
        {
            for (int i = 0; i < EnemyCount; i++)
            {
                SpawnRandomVirus.x = Random.Range(-2.2f, 2.2f);
                SpawnRandomVirus.y = Random.Range(0.0f, 1.0f);
                SpawnRandomVirus.z = Random.Range(-2.2f, 2.2f);
                GameObject VC = Instantiate(VirusCube, SpawnRandomVirus, Quaternion.identity, transform) as GameObject;
                VC.GetComponent<Virus>().Speed = SmallVirusSpeed;
                VC.GetComponent<Virus>().Health = SmallVirusHealth;
                VirusList.Add(VC);
            }
        }

        else if (SpawnBoss == true)
        {
            //Boss
            GameObject BC = Instantiate(BossCube, BossSpawn.transform.position, Quaternion.identity, transform) as GameObject;
            BC.GetComponent<Virus>().Speed = BossVirusSpeed;
            BC.GetComponent<Virus>().Health = BossVirusHealth;
            VirusList.Add(BC);
            CheckCount = false;
        }

        DoneSpawning = true;
        WaveNumber++;
    }

    public void CreateSmallVirus(GameObject pos)
    {
        GameObject SMC = Instantiate(VirusCube, pos.transform.position, Quaternion.identity, transform) as GameObject;
        SMC.GetComponent<Virus>().Speed = SmallVirusSpeed;
        SMC.GetComponent<Virus>().Health = SmallVirusHealth;
        VirusList.Add(SMC);
        VirusList[VirusList.Count - 1].GetComponent<Virus>().BossSpawnSmallVirus = true;
    }

    public void CreateBigVirus(GameObject pos)
    {
        GameObject BVC = Instantiate(BigVirusCube, pos.transform.position, Quaternion.identity, transform) as GameObject;
        BVC.GetComponent<Virus>().Speed = BigVirusSpeed;
        BVC.GetComponent<Virus>().Health = BigVirusHealth;
        VirusList.Add(BVC);
    }
}
