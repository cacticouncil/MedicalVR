using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public GameObject Player;

    int WhatColorProtein;
    public GameObject TheOGProtien;
    public GameObject BlueAntiViralProtein;
    public GameObject RedAntiViralProtein;
    public GameObject YellowAntiViralProtein;
    public GameObject GreenAntiViralProtein;
    public List<GameObject> AntiViralProteinList = new List<GameObject>();
    public int AntiViralProteinCount;
    Vector3 AntiViralProteinLocation;

    public GameObject CellReceptors;
    public List<GameObject> CellReceptorsList = new List<GameObject>();
    public int CellReceptorCount;
    Vector3 CellReceptorLocation;

    public int WaveNumber = 1;
    public bool CanDestroyProteins = false;

    //Tutorial Variables
    public GameObject TutorialCellReceptor;
    public bool CanISpawnAntiViralProtein = false;
    public bool CanISpawnCellReceptor = false;
    void Update()
    {
        if (VirusPlayer.TutorialMode == false)
        {
            if (WaveNumber < 5)
            {
                if (CellReceptorCount == 0 && CellReceptorsList.Count == 0 && Player.GetComponent<VirusPlayer>().WaveStarted == false)
                {
                    if (WaveNumber != 1)
                    {
                        for (int i = 0; i < AntiViralProteinList.Count; i++)
                        {
                            AntiViralProteinList[i].gameObject.GetComponent<AntiViralProtein>().DestroyAntiBody();
                        }
                    }

                    switch (WaveNumber)
                    {
                        case 1:
                            CellReceptorCount = 3;
                            AntiViralProteinCount = 2;
                            break;

                        case 2:
                            CellReceptorCount = 4;
                            AntiViralProteinCount = 3;
                            break;

                        case 3:
                            CellReceptorCount = 5;
                            AntiViralProteinCount = 4;
                            break;

                        case 4:
                            CellReceptorCount = 6;
                            AntiViralProteinCount = 4;
                            break;

                        default:
                            break;
                    }

                    CreateCellReceptorWave();
                    Player.GetComponent<VirusPlayer>().Respawn();
                    Player.GetComponent<VirusPlayer>().Speed = 0.0f;
                    Player.GetComponent<VirusPlayer>().WaveStarted = true;
                    CanDestroyProteins = false;
                    Invoke("CreateAntiViralProteinWave", 6);
                }
            }

            //Completed All Waves
            else if (WaveNumber >= 5)
            {
                if (CellReceptorCount == 0 && CellReceptorsList.Count == 0 && Player.GetComponent<VirusPlayer>().WaveStarted == false)
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

        else if (VirusPlayer.TutorialMode == true)
        {
            if (CanISpawnCellReceptor == true)
            {
                CellReceptorCount = 1;
                CreateCellReceptorWave();
                CanISpawnCellReceptor = false;
            }

            if (CanISpawnAntiViralProtein == true)
            {
                AntiViralProteinCount = 1;
                CreateAntiViralProteinWave();
                CanISpawnAntiViralProtein = false;
            }
        }
    }

    void CreateCellReceptorWave()
    {
        for (int i = 0; i < CellReceptorCount; i++)
        {
            if (VirusPlayer.TutorialMode == false)
            {
                CellReceptorLocation = Random.onUnitSphere * 6.5f;
                CellReceptorsList.Add(Instantiate(CellReceptors, CellReceptorLocation, Quaternion.identity, transform) as GameObject);
            }

            else if (VirusPlayer.TutorialMode == true)
            {
                CellReceptorsList.Add(Instantiate(CellReceptors, TutorialCellReceptor.transform.position, Quaternion.identity, transform) as GameObject);
            }
        }
    }

    void CreateAntiViralProteinWave()
    {
        for (int i = 0; i < AntiViralProteinCount; i++)
        {
            if (VirusPlayer.TutorialMode == false)
            {
                AntiViralProteinLocation = Random.insideUnitSphere * 6.5f;
                while (Vector3.Distance(AntiViralProteinLocation, Player.transform.position) <= 2.5f)
                    AntiViralProteinRespawn(AntiViralProteinLocation);
            }

            else if (VirusPlayer.TutorialMode == true)
            {
                AntiViralProteinLocation = TutorialCellReceptor.transform.position;
            }

            //WhatColorProtein = Random.Range(0, 5);
            WhatColorProtein = 5;
            switch (WhatColorProtein)
            {
                case 1:
                    AntiViralProteinList.Add(Instantiate(BlueAntiViralProtein, AntiViralProteinLocation, Quaternion.identity, transform) as GameObject);
                    break;

                case 2:
                    AntiViralProteinList.Add(Instantiate(RedAntiViralProtein, AntiViralProteinLocation, Quaternion.identity, transform) as GameObject);
                    break;

                case 3:
                    AntiViralProteinList.Add(Instantiate(YellowAntiViralProtein, AntiViralProteinLocation, Quaternion.identity, transform) as GameObject);
                    break;

                case 4:
                    AntiViralProteinList.Add(Instantiate(GreenAntiViralProtein, AntiViralProteinLocation, Quaternion.identity, transform) as GameObject);
                    break;

                case 5:
                    AntiViralProteinList.Add(Instantiate(TheOGProtien, AntiViralProteinLocation, Quaternion.identity, transform) as GameObject);
                    break;
                default:
                    break;
            }
        }

        if (VirusPlayer.TutorialMode == false)
        {
            Player.GetComponent<VirusPlayer>().Speed = .01f;
            CanDestroyProteins = true;
        }
    }

    Vector3 AntiViralProteinRespawn(Vector3 Position)
    {
        Position = Random.insideUnitSphere * 6.5f;
        return Position;
    }
}
