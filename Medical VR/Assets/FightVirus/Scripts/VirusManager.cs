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
    void Start()
    {
        WaveNumber = 1;
        CheckCount = false;
    }

    void Update()
    {
        if (CheckCount == true && Player.TutorialMode == false)
        {
            if (VirusList.Count == 0)
            {
                switch (WaveNumber)
                {
                    case 1:
                        if (Player.ArcadeMode == true)
                        {
                            EnemyCount = 12;
                            SmallVirusSpeed = 0.003f;
                            SmallVirusHealth = 20;
                            BigVirusSpeed = 0.001f;
                            BigVirusHealth = 40;
                        }

                        else if (Player.ArcadeMode == false)
                        {
                            EnemyCount = 7;
                            SmallVirusSpeed = 0.003f;
                            SmallVirusHealth = 20;
                            BigVirusSpeed = 0.001f;
                            BigVirusHealth = 40;
                        }
                        break;

                    case 2:
                        if (Player.ArcadeMode == true)
                        {
                            EnemyCount = 16;
                            SmallVirusSpeed = 0.004f;
                            SmallVirusHealth = 40;
                            BigVirusSpeed = 0.002f;
                            BigVirusHealth = 50;
                        }

                        else if (Player.ArcadeMode == false)
                        {
                            EnemyCount = 12;
                            SmallVirusSpeed = 0.003f;
                            SmallVirusHealth = 40;
                            BigVirusSpeed = 0.002f;
                            BigVirusHealth = 50;
                        }
                        break;

                    case 3:
                        if (Player.ArcadeMode == true)
                        {
                            EnemyCount = 20;
                            SmallVirusSpeed = 0.009f;
                            SmallVirusHealth = 60;
                            BigVirusSpeed = 0.003f;
                            BigVirusHealth = 60;
                        }

                        else if (Player.ArcadeMode == false)
                        {
                            EnemyCount = 17;
                            SmallVirusSpeed = 0.004f;
                            SmallVirusHealth = 60;
                            BigVirusSpeed = 0.003f;
                            BigVirusHealth = 60;
                        }
                        break;

                    case 4:
                        SpawnBoss = true;
                        SmallVirusSpeed = 0.01f;
                        SmallVirusHealth = 20;
                        BossVirusSpeed = 0.1f;
                        BossVirusHealth = 400;
                        break;

                    default:
                        break;
                }

                CheckCount = false;
                FightVirusPlayer.GetComponent<Player>().DisplayWaveNumber = true;
                Invoke("CreateWave", 5);
            }
        }

        if (Player.TutorialMode == true)
        {
            if (CanISpawn == true)
            {
                switch (WaveNumber)
                {
                    case 1:
                        EnemyCount = 3;
                        SmallVirusSpeed = 0.003f;
                        SmallVirusHealth = 20;
                        BigVirusSpeed = 0.007f;
                        BigVirusHealth = 40;
                        Invoke("CreateWave", 2);
                        break;

                    case 2:
                        EnemyCount = 7;
                        SmallVirusSpeed = 0.002f;
                        SmallVirusHealth = 20;
                        BigVirusSpeed = 0.001f;
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

            CheckCount = true;
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

        WaveNumber += 1;
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
