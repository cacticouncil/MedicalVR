using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct Spawn
{
    public GameObject Pos;
    public List<GameObject> VirusList;
    public int ListCount;

    public Spawn(GameObject go)
    {
        Pos = go;
        VirusList = new List<GameObject>();
        ListCount = 0;
    }
}

public class VirusLocations : MonoBehaviour
{
    public List<Spawn> VirusLocationList = new List<Spawn>();
    void Start()
    {
        Spawn S1 = new Spawn(GameObject.Find("1VirusGoTo"));
        Spawn S2 = new Spawn(GameObject.Find("2VirusGoTo"));
        Spawn S3 = new Spawn(GameObject.Find("3VirusGoTo"));
        Spawn S4 = new Spawn(GameObject.Find("4VirusGoTo"));

        VirusLocationList.Add(S1);
        VirusLocationList.Add(S2);
        VirusLocationList.Add(S3);
        VirusLocationList.Add(S4);

    }
}

