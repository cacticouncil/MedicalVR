using UnityEngine;
using System.Collections;

public class SpawnSting : MonoBehaviour {

    public Transform[] SpawnPoints;
    public float spawntime = 1.5f;

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
        Vector3 pos = /*Center.transform.position + */Random.insideUnitSphere * size;
        GameObject CGamp = Instantiate(Stings, /*pos*/ SpawnPoints[Randomindex].position, /*Quaternion.identity*/SpawnPoints[Randomindex].rotation) as GameObject;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(/*transform.localPosition + */Center.transform.position, size);
    }
}
