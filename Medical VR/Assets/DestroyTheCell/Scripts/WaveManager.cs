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
    public bool CanDestroyProteins = false;
    void Update()
    {
        if (WaveNumber < 5)
        {
            if (ProteinCount == 0 && ProteinList.Count == 0 && Player.GetComponent<VirusPlayer>().WaveStarted == false)
            {
                if (WaveNumber != 1)
                {
                    for (int i = 0; i < AntiViralProteinList.Count; i++)
                    {
                        AntiViralProteinList[i].gameObject.GetComponent<Antibody>().DestroyAntiBody();
                    }
                }

                switch (WaveNumber)
                {
                    case 1:
                        ProteinCount = 3;
                        AntiViralProteinCount = 2;
                        break;

                    case 2:
                        ProteinCount = 4;
                        AntiViralProteinCount = 3;
                        break;

                    case 3:
                        ProteinCount = 5;
                        AntiViralProteinCount = 4;
                        break;

                    case 4:
                        ProteinCount = 6;
                        AntiViralProteinCount = 4;
                        break;

                    default:
                        break;
                }

                CreateProteinWave();
                Player.GetComponent<VirusPlayer>().Respawn();
                Player.GetComponent<VirusPlayer>().Speed = 0.0f;
                Player.GetComponent<VirusPlayer>().WaveStarted = true;
                CanDestroyProteins = false;
                Invoke("CreateAntiViralProteinWave", 7);
            }
        }

        //Completed All Waves
        else if (WaveNumber >= 5)
        {
            if (ProteinCount == 0 && ProteinList.Count == 0 && Player.GetComponent<VirusPlayer>().WaveStarted == false)
            {
                Player.GetComponent<VirusPlayer>().isGameover = true;
                Player.GetComponent<VirusPlayer>().Speed = 0.0f;
            }
        }


        for (int i = 0; i < AntiViralProteinList.Count; i++)
        {
            if (AntiViralProteinList[i].gameObject == null)
                AntiViralProteinList.Remove(AntiViralProteinList[i]);
        }
    }

    void CreateProteinWave()
    {
        for (int i = 0; i < ProteinCount; i++)
        {
            ProteinLocation = Random.onUnitSphere * 6.5f;
            ProteinList.Add(Instantiate(Protein, ProteinLocation, Quaternion.identity, transform) as GameObject);
        }
    }

    void CreateAntiViralProteinWave()
    {
        for (int i = 0; i < AntiViralProteinCount; i++)
        {
            AntiViralProteinLocation = Random.insideUnitSphere * 6.5f;
            while (Vector3.Distance(AntiViralProteinLocation, Player.transform.position) <= 2.5f)
                AntiViralProteinRespawn(AntiViralProteinLocation);
            
            AntiViralProteinList.Add(Instantiate(AntiViralProtein, AntiViralProteinLocation, Quaternion.identity, transform) as GameObject);
        }

        Player.GetComponent<VirusPlayer>().Speed = .01f;
        CanDestroyProteins = true;
    }

    Vector3 AntiViralProteinRespawn(Vector3 Position)
    {
        Position = Random.insideUnitSphere * 6.5f;
        return Position;
    }
}
