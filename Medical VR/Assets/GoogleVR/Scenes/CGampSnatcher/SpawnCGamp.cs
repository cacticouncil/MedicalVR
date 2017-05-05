using UnityEngine;
using System.Collections;

public class SpawnCGamp : MonoBehaviour {

    public GameObject Cgampprefab;

    public GameObject Center;
    public float size;

    //public Quaternion min;
    //public Quaternion max;

	// Use this for initialization
	void Start () {
        SpawnEnzime();
    }
	
	// Update is called once per frame
	void Update () {
    }
    public void SpawnEnzime()
    {
        InvokeRepeating("SpawnC", 1, 0.5f);
    }

    public void SpawnC()
    {
        Vector3 pos = /*Center.transform.position + */Random.insideUnitSphere * size;
        pos.y += 10;
        GameObject ent = Instantiate(Cgampprefab, pos, Quaternion.identity);
        ent.GetComponent<_TSizeChange>().Inititalize();
        ent.GetComponent<_TSizeChange>().StartGrow();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(/*transform.localPosition + */Center.transform.position, size);
    }
}
