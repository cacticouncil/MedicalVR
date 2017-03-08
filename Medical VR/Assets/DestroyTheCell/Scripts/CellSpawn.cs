using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellSpawn : MonoBehaviour
{
    public GameObject Cell;
    public List<GameObject> CellList = new List<GameObject>();
    public int CellCount;
    Vector3 SpawnRandomCell;

    void Start ()
    {
        CellCount = 5;
        for (int i = 0; i < CellCount; i++)
        {
            SpawnRandomCell = Random.onUnitSphere * 50.0f;
            CellList.Add(Instantiate(Cell, SpawnRandomCell, Quaternion.identity, transform) as GameObject);
        }
    }
}
