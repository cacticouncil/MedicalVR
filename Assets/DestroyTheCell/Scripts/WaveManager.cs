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
    public GameObject CellReceptor1;
    public GameObject CellReceptor2;
    public List<GameObject> CellReceptorsList = new List<GameObject>();
    public int CellReceptorCount;
    Vector3 CellReceptorLocation;

    public int WaveNumber = 1;

    //Tutorial Variables
    public GameObject TutorialLocationStart;
    public GameObject TutorialLocationEnd;

    public bool CanISpawnCellReceptor = false;
    public bool CanISpawnAntiViralProtein = false;
    public bool DoneSpawningCellReceptors = false;
    void Update()
    {
        if (GlobalVariables.tutorial == false)
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
                            CellReceptorCount = 2;
                            AntiViralProteinCount = 1;
                            break;

                        case 2:
                            CellReceptorCount = 3;
                            AntiViralProteinCount = 2;
                            break;

                        case 3:
                            if (GlobalVariables.arcadeMode)
                            {
                                CellReceptorCount = 7;
                                AntiViralProteinCount = 4;
                            }
                            else
                            {
                                CellReceptorCount = 5;
                                AntiViralProteinCount = 2;
                            }
                            break;

                        case 4:
                            if (GlobalVariables.arcadeMode)
                            {
                                CellReceptorCount = 8;
                                AntiViralProteinCount = 5;
                            }
                            else
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
                    Player.GetComponent<VirusPlayer>().currSpeed = 0.0f;
                    Player.GetComponent<VirusPlayer>().WaveStarted = true;
                    Invoke("CreateAntiViralProteinWave", 5);
                }
            }

            //Completed All Waves
            else if (WaveNumber == 5)
            {
                if (CellReceptorCount == 0 && CellReceptorsList.Count == 0 && Player.GetComponent<VirusPlayer>().WaveStarted == false)
                {
                    Player.GetComponent<VirusPlayer>().isGameover = true;
                    Player.GetComponent<VirusPlayer>().currSpeed = 0.0f;
                }
            }

            for (int i = 0; i < AntiViralProteinList.Count; i++)
            {
                if (AntiViralProteinList[i].gameObject == null)
                    AntiViralProteinList.Remove(AntiViralProteinList[i]);
            }
        }

        else if (GlobalVariables.tutorial == true)
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
            CellReceptorLocation = Random.onUnitSphere * 11.6f;
            WhatColorCellReceptor = Random.Range(1, 3);

            if (GlobalVariables.tutorial == true && WaveNumber == 1)
                CellReceptorLocation = TutorialLocationStart.transform.position;

            
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
        DoneSpawningCellReceptors = true;
    }

    void CreateAntiViralProteinWave()
    {
        for (int i = 0; i < AntiViralProteinCount; i++)
        {
            if (GlobalVariables.tutorial == false)
                AntiViralProteinLocation = Random.onUnitSphere * 5.5f;

            else if (GlobalVariables.tutorial == true)
            {
                if (WaveNumber == 1)
                    AntiViralProteinLocation = TutorialLocationStart.transform.position;

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

        if (GlobalVariables.tutorial == false)
        {
            Player.GetComponent<VirusPlayer>().currSpeed = Player.GetComponent<VirusPlayer>().baseSpeed;
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
