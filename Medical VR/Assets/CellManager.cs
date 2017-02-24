using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CellManager : MonoBehaviour
{

    public GameObject Cell;
    public List<GameObject> Cells = new List<GameObject>();
    public int CellCount = 15;
    Vector3 SpawnRandomCell;
    void Start()
    {
        for (int i = 0; i < CellCount; i++)
        {
            SpawnRandomCell = Random.onUnitSphere;
            SpawnRandomCell *= 15.0f;
            Cells.Add(Instantiate(Cell, SpawnRandomCell, Quaternion.identity, transform) as GameObject);
        }
    }
}
