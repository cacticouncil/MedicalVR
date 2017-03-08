using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnStruct : MonoBehaviour
{
    public struct Spawn
    {
        public GameObject Pos;
        public List<GameObject> VirusList;
        public int ListCount;

        public Spawn(GameObject go)
        {
            Pos = go;
            VirusList = null;
            ListCount = 0;
        }
    }
}
