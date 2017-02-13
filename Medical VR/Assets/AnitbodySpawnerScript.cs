using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnitbodySpawnerScript : MonoBehaviour {

    public GameObject redCell;
    public GameObject virus;
    public GameObject refAntiBody;
    public int xRange, xGap, yRange, yGap, zGap, amount;
    List<Vector3> randPositions = new List<Vector3>();
	// Use this for initialization
	void Start ()
    {
        float distance = redCell.transform.position.z - virus.transform.position.z;
        float cellSpeed = redCell.GetComponent<RedCellScript>().speed;
        float virusSpeed = virus.GetComponent<MovingCamera>().speed;
        float tmpSpeed = virusSpeed - cellSpeed;
        float maxZ = (distance / tmpSpeed);

        for (int z = 200; z < maxZ; z+=zGap)
        {
            for(int x = -xRange; x < xRange + 1; x+=xGap)
            {
                for (int y = -yRange; y < yRange + 1; y += yGap)
                {
                    Vector3 tmp = new Vector3(x, y, z);
                    randPositions.Add(tmp);
                }
            }
           
        }
        float antibodyCount = amount;
        if (amount > randPositions.Count)
            amount = randPositions.Count;
    
        for (int i = 0; i < antibodyCount ; i++)
        {
            int randIndex = Random.Range(0, randPositions.Count - i);
            GameObject tmpAntibody = Instantiate(refAntiBody, randPositions[randIndex], refAntiBody.transform.rotation) as GameObject;
            
            randPositions.Remove(tmpAntibody.transform.position);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
