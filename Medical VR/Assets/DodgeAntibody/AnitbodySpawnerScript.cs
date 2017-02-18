using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnitbodySpawnerScript : MonoBehaviour {

    public GameObject redCell;
    public GameObject virus;
    public GameObject refAntiBody;
    public int xRange, yRange, zGap, maxZ;
    List<int> randPositions = new List<int>();
	// Use this for initialization
	void Start ()
    {
        

        int num = 500;
        for(int i = 0; i < 5; i++)
        {
            randPositions.Add(num);
            num = num - 250;
        }
        for (int z = 1000; z < maxZ; z+=zGap)
        {
            List<int> tmpXList = new List<int>();
            List<int> tmpYList = new List<int>();
            for(int n = 0; n < 5; n++)
            {
                tmpXList.Add(randPositions[n]);
                tmpYList.Add(randPositions[n]);
            }
            for (int j = 0; j < 5; j++)
            {
                int xIdx = Random.Range(0, 4-j);
                int yIdx = Random.Range(0, 4-j);
                Vector3 tmpPos = new Vector3(tmpXList[xIdx], tmpYList[yIdx], z);
                tmpXList.Remove(tmpXList[xIdx]);
                tmpYList.Remove(tmpYList[yIdx]);
                GameObject tmpAntibody = Instantiate(refAntiBody, tmpPos, refAntiBody.transform.rotation) as GameObject;
                z += zGap;
            }
            
        }
        //float antibodyCount = amount;
        //if (amount > randPositions.Count)
        //    amount = randPositions.Count;
    
        //for (int i = 0; i < antibodyCount ; i++)
        //{
        //    int randIndex = Random.Range(0, randPositions.Count - i);
        //    GameObject tmpAntibody = Instantiate(refAntiBody, randPositions[randIndex], refAntiBody.transform.rotation) as GameObject;
            
        //    randPositions.Remove(tmpAntibody.transform.position);
        //}
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
