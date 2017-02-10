using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject VirusCube;
    public Transform[] enemies;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("Spawn", 3.5f, 3.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Spawn()
    {
        Vector3 RandomPos;
        RandomPos.x = Random.Range(-75, 75);
        RandomPos.y = Random.Range(5, 15);
        RandomPos.z = Random.Range(-75, 75);

        Instantiate(VirusCube, RandomPos, Quaternion.identity);
    }
}
