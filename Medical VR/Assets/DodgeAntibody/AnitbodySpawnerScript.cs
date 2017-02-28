using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnitbodySpawnerScript : MonoBehaviour {

    public GameObject redCell;
    public GameObject virus;
    public GameObject refAntiBody;
    public List<GameObject> Obstacles = new List<GameObject>();
    public int  zGap, maxZ;
    List<GameObject> randPositions = new List<GameObject>();
	// Use this for initialization
	void Start ()
    {
        GenerateObstacles();
	}
	
	public void GenerateObstacles()
    {
        if(randPositions.Count != 0)
        {
            for (int i = 0; i < randPositions.Count; i++)
            {
                Destroy(randPositions[i]);
            }
            randPositions.Clear();
        }
        for (int z = 1000; z < maxZ; z += zGap)
        {
            List<GameObject> tmpNum = new List<GameObject>();

            for (int i = 0; i < Obstacles.Count; i++)
            {
                tmpNum.Add(Obstacles[i]);
            }
            for (int j = 0; j < Obstacles.Count; j++)
            {
                int obNum = Random.Range(0, Obstacles.Count - 1 - j);
                Vector3 tmpPos = new Vector3(tmpNum[obNum].transform.position.x, tmpNum[obNum].transform.position.y, z);
                tmpNum.Remove(tmpNum[obNum]);
                GameObject tmp = Instantiate(refAntiBody, tmpPos, refAntiBody.transform.rotation) as GameObject;
                randPositions.Add(tmp);
                z += zGap;
            }
        }
    }
	void Update ()
    {
	
	}
}
