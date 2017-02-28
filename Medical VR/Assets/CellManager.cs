using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CellManager : MonoBehaviour
{

    public GameObject Cell;
    public List<GameObject> CellList = new List<GameObject>();
    public int CellCount = 7;
    Vector3 SpawnRandomCell;

    void Start()
    {
        for (int i = 0; i < CellCount; i++)
        {
            SpawnRandomCell = Random.onUnitSphere * 10.0f;
            if (SpawnRandomCell.y < 0)
            {
                SpawnRandomCell.y = Random.Range(0, 8);
            }
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
