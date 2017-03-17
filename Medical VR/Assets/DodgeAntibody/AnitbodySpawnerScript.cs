using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnitbodySpawnerScript : MonoBehaviour {

    public GameObject redCell;
    public GameObject virus;
    public GameObject refAntiBody;
    public GameObject refPoint;
    public List<GameObject> Obstacles = new List<GameObject>();
    public int  zGap, levelNum;
    public GameObject theLevel, obsStart, obsEnd;
    List<GameObject> randPositions = new List<GameObject>();
    List<GameObject> points = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        levelNum = 0;
        GenerateObstacles();
	}
	public void Restart()
    {
        levelNum = 0;
        theLevel.GetComponent<TextMesh>().text = "LEVEL: " + levelNum.ToString();
        GenerateObstacles();
    }
	public void GenerateObstacles()
    {
        levelNum++;
        theLevel.GetComponent<TextMesh>().text = "LEVEL: " + levelNum.ToString();
        if (randPositions.Count != 0)
        {
            for (int i = 0; i < randPositions.Count; i++)
            {
                Destroy(randPositions[i]);
            }
            randPositions.Clear();
        }

        if (points.Count != 0)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Destroy(points[i]);
            }
            points.Clear();
        }

        for (int z =(int) obsStart.transform.position.z; z < obsEnd.transform.position.z; z += zGap)
        {
            List<GameObject> tmpNum = new List<GameObject>();
            List<GameObject> tmpPoints = new List<GameObject>();
            for (int i = 0; i < Obstacles.Count; i++)
            {
                tmpNum.Add(Obstacles[i]);
                if(virus.GetComponent<MovingCamera>().arcadeMode == true)
                tmpPoints.Add(Obstacles[i]);
            }
            for (int j = 0; j < Obstacles.Count; j++)
            {
                int obNum = Random.Range(0, Obstacles.Count - 1 - j);
                Vector3 tmpPos = new Vector3(tmpNum[obNum].transform.position.x, tmpNum[obNum].transform.position.y, z);
                int sz = Random.Range(0, 3);
                if (sz == 0)
                    sz = 0;
                else if (sz == 1)
                    sz = 10000;
                else if (sz == 2)
                    sz = -10000;
               
                tmpNum.Remove(tmpNum[obNum]);
                GameObject tmp = Instantiate(refAntiBody, tmpPos, refAntiBody.transform.rotation) as GameObject;
                tmp.transform.localScale = new Vector3(tmp.transform.localScale.x + sz, tmp.transform.localScale.y + sz, tmp.transform.localScale.z + sz);
                randPositions.Add(tmp);

                if(j%2 == 0 && virus.GetComponent<MovingCamera>().arcadeMode == true)
                {
                    obNum = Random.Range(0, Obstacles.Count - 1 - j);
                    tmpPos = new Vector3(tmpPoints[obNum].transform.position.x, tmpPoints[obNum].transform.position.y, z);
                    tmpPoints.Remove(tmpPoints[obNum]);
                    tmp = Instantiate(refPoint, tmpPos, refPoint.transform.rotation) as GameObject;
                    points.Add(tmp);
                }
               

                z += zGap;
            }
        }
    }
	void Update ()
    {
	
	}
}
