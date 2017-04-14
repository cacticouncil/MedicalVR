using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public GameObject Player;

    int WhatColorProtein;
    public GameObject BlueAntiViralProtein;
    public GameObject RedAntiViralProtein;
    public GameObject YellowAntiViralProtein;
    public GameObject GreenAntiViralProtein;
    public List<GameObject> AntiViralProteinList = new List<GameObject>();
    public int AntiViralProteinCount;
    Vector3 AntiViralProteinLocation;

    int WhatColorCellReceptor;
    public GameObject CellReceptors;
    public GameObject CellReceptor1;
    public GameObject CellReceptor2;
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
                            CellReceptorCount = 1;
                            AntiViralProteinCount = 1;
                            break;

                        case 2:
                            CellReceptorCount = 3;
                            AntiViralProteinCount = 2;
                            break;

                        case 3:
                            if (VirusPlayer.ArcadeMode == true)
                            {
                                CellReceptorCount = 7;
                                AntiViralProteinCount = 4;
                            }

                            else if (VirusPlayer.ArcadeMode == false)
                            {
                                CellReceptorCount = 5;
                                AntiViralProteinCount = 2;
                            }
                            break;

                        case 4:
                            if (VirusPlayer.ArcadeMode == true)
                            {
                                CellReceptorCount = 9;
                                AntiViralProteinCount = 5;
                            }

                            else if (VirusPlayer.ArcadeMode == false)
                            {
                                CellReceptorCount = 5;
                                AntiViralProteinCount = 4;
                            }
                            break;

                        default:
                            break;
                    }

                    CreateCellReceptorWave();
                    Player.GetComponent<VirusPlayer>().Respawn();
                    Player.GetComponent<VirusPlayer>().PlayerSpeed = 0.0f;
                    Player.GetComponent<VirusPlayer>().WaveStarted = true;
                    CanDestroyProteins = false;
                    Invoke("CreateAntiViralProteinWave", 5);
                }
            }

            //Completed All Waves
            else if (WaveNumber == 5)
            {
                if (CellReceptorCount == 0 && CellReceptorsList.Count == 0 && Player.GetComponent<VirusPlayer>().WaveStarted == false)
                {
                    Player.GetComponent<VirusPlayer>().isGameover = true;
                    Player.GetComponent<VirusPlayer>().PlayerSpeed = 0.0f;
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
            switch (WaveNumber)
            {
                case 1:
                    CellReceptorCount = 1;
                    AntiViralProteinCount = 1;
                    break;

                case 2:
                    CellReceptorCount = 3;
                    AntiViralProteinCount = 2;
                    break;
            }

            if (CanISpawnCellReceptor == true)
            {
                CreateCellReceptorWave();
                CanISpawnCellReceptor = false;
            }

            if (CanISpawnAntiViralProtein == true)
            {
                CreateAntiViralProteinWave();
                CanISpawnAntiViralProtein = false;
            }
        }
    }

    void CreateCellReceptorWave()
    {
        for (int i = 0; i < CellReceptorCount; i++)
        {
            CellReceptorLocation = Random.onUnitSphere * 7.0f;
            WhatColorCellReceptor = Random.Range(1, 3);

            switch (WhatColorCellReceptor)
            {
                case 1:
                    CellReceptorsList.Add(Instantiate(CellReceptor1, CellReceptorLocation, Quaternion.identity, transform) as GameObject);
                    break;

                case 2:
                    CellReceptorsList.Add(Instantiate(CellReceptor2, CellReceptorLocation, Quaternion.identity, transform) as GameObject);
                    break;

                default:
                    break;
            }
        }
    }

    void CreateAntiViralProteinWave()
    {
        for (int i = 0; i < AntiViralProteinCount; i++)
        {
            if (VirusPlayer.TutorialMode == false)
                AntiViralProteinLocation = Random.onUnitSphere * 5.0f;

            else if (VirusPlayer.TutorialMode == true)
            {
                if (WaveNumber == 1)
                    AntiViralProteinLocation = TutorialCellReceptor.transform.position;

                else if (WaveNumber == 2)
                    AntiViralProteinLocation = Random.onUnitSphere * 6.0f;
            }

            WhatColorProtein = Random.Range(0, 5);

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

                default:
                    break;
            }
        }

        if (VirusPlayer.TutorialMode == false)
        {
            Player.GetComponent<VirusPlayer>().PlayerSpeed = .02f;
            CanDestroyProteins = true;
        }
    }

    Vector3 AntiViralProteinRespawn(Vector3 Position)
    {
        Position = Random.insideUnitSphere * 6.5f;
        return Position;
    }

    Vector3 CellReceptorRespawn(Vector3 Position)
    {
        Position = Random.insideUnitSphere * 6.5f;
        return Position;
    }
}
