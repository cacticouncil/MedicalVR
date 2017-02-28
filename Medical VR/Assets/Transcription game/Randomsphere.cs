using UnityEngine;
using System.Collections;

public class Randomsphere : MonoBehaviour {

    public GameObject[]  Spheres = new GameObject[5];

    private int i = Random.Range(0, 4);
    private int j = Random.Range(0, 4);
    private int k = Random.Range(0, 4);

    // Use this for initialization
    void Start () {
        Instantiate(Spheres[i], new Vector3(-35,26,30), Spheres[0].transform.rotation);

        while (j == 3 || j == i)
            j = Random.Range(0, 4);


                Instantiate(Spheres[j], new Vector3(-20, 26, 30), Spheres[1].transform.rotation);
           
        
            while (k == 2 || k == j || k == 1)
            k = Random.Range(0, 4);


            Instantiate(Spheres[k], new Vector3(-5, 26, 30), Spheres[2].transform.rotation);


    }

    // Update is called once per frame
    void Update () {
	
	}
}
