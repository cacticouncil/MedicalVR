using UnityEngine;
using System.Collections.Generic;

public class SpawnSting : MonoBehaviour
{
    public GameObject mainCamera;
    public Transform stingHolder;
    public Transform[] SpawnPoints;
    [System.NonSerialized]
    public List<Transform> takenPoints = new List<Transform>();
    public float spawntime = 7.5f;
    public GameObject Stings;
    public float size;

    //public Quaternion min;
    //public Quaternion max;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnC", 0, spawntime);
    }

    public void SpawnC()
    {
        while (takenPoints.Count != SpawnPoints.Length)
        {
            Debug.Log("taken points size" + takenPoints.Count);
            Debug.Log("spawn points size" + SpawnPoints.Length);
            int index = Random.Range(0, SpawnPoints.Length);
            if (!takenPoints.Contains(SpawnPoints[index]))
            {
            //    if(Storebullets.stingamount < 6)
            //    {
                    takenPoints.Add(SpawnPoints[index]);
                    GameObject obj = Instantiate(Stings, SpawnPoints[index].position, Quaternion.identity, stingHolder);
            //        Storebullets.stingamount += 1;
                    obj.GetComponent<GetpointsER>().storebullets = mainCamera;
                    obj.GetComponent<GetpointsER>().parent = this;
                    obj.GetComponent<GetpointsER>().position = SpawnPoints[index];
                    return;
            //    }
            }
        }
    }

    private void OnDisable()
    {
        CancelInvoke("SpawnC");
    }
}
