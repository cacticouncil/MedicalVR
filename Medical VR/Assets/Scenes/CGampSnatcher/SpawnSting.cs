using UnityEngine;
using System.Collections.Generic;

public class SpawnSting : MonoBehaviour
{
    public GameObject mainCamera;
    public Transform stingHolder;
    public Transform[] SpawnPoints;
    [System.NonSerialized]
    public List<Transform> takenPoints = new List<Transform>();
    public float spawntime = 1.5f;
    public GameObject Stings;
    public float size;

    //public Quaternion min;
    //public Quaternion max;

    // Use this for initialization
    void Start()
    {
        SpawnEnzime();
        SpawnC();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            SpawnEnzime();
        }
    }

    public void SpawnEnzime()
    {
        InvokeRepeating("SpawnC", spawntime, spawntime);
    }

    public void SpawnC()
    {
        while (takenPoints.Count != SpawnPoints.Length)
        {
            int index = Random.Range(0, SpawnPoints.Length);
            if (!takenPoints.Contains(SpawnPoints[index]))
            {
                takenPoints.Add(SpawnPoints[index]);
                GameObject obj = Instantiate(Stings, SpawnPoints[index].position, Quaternion.identity, stingHolder);
                obj.GetComponent<GetpointsER>().storebullets = mainCamera;
                obj.GetComponent<GetpointsER>().parent = this;
                obj.GetComponent<GetpointsER>().position = SpawnPoints[index];
                return;
            }
        }
    }

    private void OnDisable()
    {
        CancelInvoke("SpawnC");
    }
}
