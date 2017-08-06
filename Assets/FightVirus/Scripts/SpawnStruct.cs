using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Spawn : System.Object
{
    public GameObject Pos;
    public List<GameObject> VirusList;
    public int SmallVirusCount;

    public Spawn(GameObject go)
    {
        Pos = go;
        VirusList = new List<GameObject>();
        SmallVirusCount = 0;
    }
}
