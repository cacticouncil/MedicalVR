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
	 if (Input.GetKey(KeyCode.Q))
            {
            SpawnEnzime();
        }
    }
    public void SpawnEnzime()
    {
        

        InvokeRepeating("SpawnC", 1, 1);
    }

    public void SpawnC()
    {
        Vector3 pos = /*Center.transform.position + */Random.insideUnitSphere * size;
        //GameObject CGamp = Instantiate(Cgampprefab, pos, Quaternion.identity) as GameObject;
        Instantiate(Cgampprefab, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(/*transform.localPosition + */Center.transform.position, size);
    }
}
