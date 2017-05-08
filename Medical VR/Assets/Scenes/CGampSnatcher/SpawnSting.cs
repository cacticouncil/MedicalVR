using UnityEngine;
using System.Collections;

public class SpawnSting : MonoBehaviour {

    public GameObject mainCamera;
    public Transform[] SpawnPoints;
    public float spawntime = 1.5f;
    private int previousRandomIndex = 0;
    public GameObject Stings;

    public GameObject Center;
    public float size;

    //public Quaternion min;
    //public Quaternion max;

    // Use this for initialization
    void Start()
    {
        SpawnEnzime();
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
        int Randomindex = Random.Range(0, SpawnPoints.Length);
        if(Randomindex != previousRandomIndex)
        {
        GameObject obj =  Instantiate(Stings, SpawnPoints[Randomindex].position, SpawnPoints[Randomindex].rotation);
        obj.GetComponent<GetpointsER>().storebullets = mainCamera;

        }

        previousRandomIndex = Randomindex;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(/*transform.localPosition + */Center.transform.position, size);
    }

    private void OnDisable()
    {
        CancelInvoke("SpawnC");
    }
}
