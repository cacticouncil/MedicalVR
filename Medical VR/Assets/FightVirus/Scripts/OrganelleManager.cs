using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class OrganelleManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject VirusPlayer;
    public GameObject Organelle;
    public List<GameObject> OrganelleList = new List<GameObject>();
    public int OrganelleCount;
    Vector3 SpawnRandomCell;

    void Start()
    {
        OrganelleCount = 15;
        for (int i = 0; i < OrganelleCount; i++)
        {
            SpawnRandomCell = Random.insideUnitSphere * 6.0f;

            if (SpawnRandomCell.y < 0)
                SpawnRandomCell.y = Random.Range(0, 7);

            OrganelleList.Add(Instantiate(Organelle, SpawnRandomCell, Quaternion.identity, transform) as GameObject);
        }
    }
}
