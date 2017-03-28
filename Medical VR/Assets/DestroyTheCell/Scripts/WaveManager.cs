using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    //Should take care of spawning anti viral proteins and target proteins
    public GameObject Player;

    public GameObject AntiViralProtein;
    public List<GameObject> AntiViralProteinList = new List<GameObject>();
    public int AntiViralProteinCount;
    Vector3 AntiViralProteinLocation;

    public GameObject Protein;
    public List<GameObject> ProteinList = new List<GameObject>();
    public int ProteinCount;
    Vector3 ProteinLocation;

    public int WaveNumber = 1;

    void Update()
    {
        //If you clear the wave
        if (WaveNumber < 5)
        {
            if (ProteinCount == 0 && ProteinList.Count == 0)
            {
                switch (WaveNumber)
                {
                    case 1:
                        ProteinCount = 3;
                        AntiViralProteinCount = 2;
                        break;

                    case 2:
                        ProteinCount = 5;
                        AntiViralProteinCount = 3;
                        break;

                    case 3:
                        ProteinCount = 7;
                        AntiViralProteinCount = 4;
                        break;

                    case 4:
                        ProteinCount = 10;
                        AntiViralProteinCount = 5;
                        break;

                    default:
                        break;
                }

                CreateProteinWave();
                Player.GetComponent<VirusPlayer>().Respawn();
                Player.GetComponent<VirusPlayer>().Speed = 0.0f;
                Invoke("CreateAntiViralProteinWave", 5);
            }
        }
    }

    void CreateAntiViralProteinWave()
    {
        for (int i = 0; i < AntiViralProteinCount; i++)
        {
            AntiViralProteinLocation = Random.insideUnitSphere * 6.5f;
            AntiViralProteinList.Add(Instantiate(AntiViralProtein, AntiViralProteinLocation, Quaternion.identity, transform) as GameObject);
        }
        Player.GetComponent<VirusPlayer>().Speed = .01f;
    }

    void CreateProteinWave()
    {
        for (int i = 0; i < ProteinCount; i++)
        {
            ProteinLocation = Random.onUnitSphere * 6.5f;
            ProteinList.Add(Instantiate(Protein, ProteinLocation, Quaternion.identity, transform) as GameObject);
        }
    }
}
