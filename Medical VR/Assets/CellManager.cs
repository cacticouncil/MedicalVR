using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CellManager : MonoBehaviour
{

    public GameObject Cell;
    public List<GameObject> CellList = new List<GameObject>();
    public int CellCount = 5;
    Vector3 SpawnRandomCell;

    void Start()
    {
        for (int i = 0; i < CellCount; i++)
        {
            SpawnRandomCell = Random.onUnitSphere;
            SpawnRandomCell *= 15.0f;
            CellList.Add(Instantiate(Cell, SpawnRandomCell, Quaternion.identity, transform) as GameObject);
        }
    }

    void Update()
    {
        //Remove Cell if it's Dead
        for (int i = 0; i < CellList.Count; i++)
        {
            if (CellList[i] == null || CellList[i].GetComponent<Cell>().isDead == true)
            {
                CellList.Remove(CellList[i]);
                i--;
            }
        }
    }
}
