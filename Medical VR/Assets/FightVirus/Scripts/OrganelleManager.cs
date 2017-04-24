using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class OrganelleManager : MonoBehaviour
{
    public GameObject FightVirusPlayer;
    public GameObject DestoryTheCellPlayer;
    public GameObject Organelle;
    public GameObject Nucleus;
    public GameObject Mitocondria;

    public List<GameObject> OrganelleList = new List<GameObject>();
    public int OrganelleCount;
    Vector3 SpawnRandomCell;

    void Start()
    {
        if (Player.TutorialMode == false || VirusPlayer.TutorialMode == false)
        {
            OrganelleCount = 5;
            for (int i = 0; i < OrganelleCount; i++)
            {
                SpawnRandomCell = Random.insideUnitSphere * 6.0f;

                if (SpawnRandomCell.y < 0)
                    SpawnRandomCell.y = Random.Range(0, 7);

                OrganelleList.Add(Instantiate(Organelle, SpawnRandomCell, Quaternion.identity, transform) as GameObject);
            }

            //Quick fix
            //Random range will be called everytime before spawning 
            SpawnRandomCell = Random.insideUnitSphere * 6.0f;

            if (SpawnRandomCell.y < 0)
                SpawnRandomCell.y = Random.Range(0, 7);
            OrganelleList.Add(Instantiate(Nucleus, SpawnRandomCell, Quaternion.identity, transform) as GameObject);

            SpawnRandomCell = Random.insideUnitSphere * 6.0f;

            if (SpawnRandomCell.y < 0)
                SpawnRandomCell.y = Random.Range(0, 7);
            OrganelleList.Add(Instantiate(Mitocondria, SpawnRandomCell, Quaternion.identity, transform) as GameObject);
        }
    }
}
