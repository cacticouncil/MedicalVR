using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CellManager : MonoBehaviour
{
    public GameObject Cell;
    public List<GameObject> CellList = new List<GameObject>();
    public int CellCount;
    Vector3 SpawnRandomCell;

    void Start()
    {
        CellCount = 17;
        for (int i = 0; i < CellCount; i++)
        {
            SpawnRandomCell = Random.onUnitSphere * 5.0f;
            if (SpawnRandomCell.y < 0)
            {
                SpawnRandomCell.y = Random.Range(0, 7);
            }
            CellList.Add(Instantiate(Cell, SpawnRandomCell, Quaternion.identity, transform) as GameObject);
        }
    }

    void Update()
    {
        //Remove Cell if it's Dead
        //for (int i = 0; i < CellList.Count; i++)
        //{
        //    if (CellList[i] == null || CellList[i].GetComponent<Cell>().isDead == true)
        //    {
        //        CellCount -= 1;
        //        CellList.Remove(CellList[i]);
        //        i--;
        //    }
        //}
    }
}
