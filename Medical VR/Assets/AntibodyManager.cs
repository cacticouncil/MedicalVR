using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AntibodyManager : MonoBehaviour
{
    public GameObject AntiBody;
    public GameObject Player;
    public List<GameObject> AntiBodyList = new List<GameObject>();
    public int AntiBodyCount;
    Vector3 AntiBodyLocation;

    void Start()
    {
        AntiBodyCount = 1;
        for (int i = 0; i < AntiBodyCount; i++)
        {
            AntiBodyLocation = Random.insideUnitSphere * 6.0f;
            AntiBodyList.Add(Instantiate(AntiBody, AntiBodyLocation, Quaternion.identity, transform) as GameObject);
        }
    }
}
