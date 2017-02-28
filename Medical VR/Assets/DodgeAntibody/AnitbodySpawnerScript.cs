using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnitbodySpawnerScript : MonoBehaviour {

    public GameObject redCell;
    public GameObject virus;
    public GameObject refAntiBody;
    public List<GameObject> Obstacles = new List<GameObject>();
    public int  zGap, maxZ;
    List<int> randPositions = new List<int>();
	// Use this for initialization
	void Start ()
    {
       
        for (int z = 1000; z < maxZ; z+=zGap)
        {
            List<GameObject> tmpNum = new List<GameObject>();
          
           for(int i = 0; i < Obstacles.Count; i++)
            {
                tmpNum.Add(Obstacles[i]);
            }
            for (int j = 0; j < Obstacles.Count; j++)
            {
                int obNum = Random.Range(0, Obstacles.Count - 1 - j);
                Vector3 tmpPos = new Vector3(tmpNum[obNum].transform.position.x, tmpNum[obNum].transform.position.y, z);
                tmpNum.Remove(tmpNum[obNum]);
                Instantiate(refAntiBody, tmpPos, refAntiBody.transform.rotation);
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
