using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnitbodySpawnerScript : MonoBehaviour {

    public GameObject platelet;
    public GameObject virus;
    public GameObject refAntiBody;
    public GameObject refPoint;
    public List<GameObject> Obstacles = new List<GameObject>();
    public int  zGap, levelNum;
    public GameObject theLevel, obsStart, obsEnd;
    List<GameObject> randPositions = new List<GameObject>();
    List<GameObject> points = new List<GameObject>();
    List<GameObject> platelets = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        levelNum = 0;
        GenerateObstacles();
	}
	public void Restart()
    {
        levelNum = 0;
        theLevel.GetComponent<TMPro.TextMeshPro>().text = "LEVEL: " + levelNum.ToString();
        GenerateObstacles();
    }
	public void GenerateObstacles()
    {
        levelNum++;
        theLevel.GetComponent<TMPro.TextMeshPro>().text = "LEVEL: " + levelNum.ToString();
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
        if (platelets.Count != 0)
        {
            for (int i = 0; i < platelets.Count; i++)
            {
                Destroy(platelets[i]);
            }
            platelets.Clear();
        }

        for (int z =(int) obsStart.transform.position.z; z < obsEnd.transform.position.z; z += zGap)
        {
            List<GameObject> tmpNum = new List<GameObject>();
            List<GameObject> tmpPoints = new List<GameObject>();
            List<GameObject> tmpPlats= new List<GameObject>();
            for (int i = 0; i < Obstacles.Count; i++)
            {
                tmpNum.Add(Obstacles[i]);
                tmpPlats.Add(Obstacles[i]);
                if (GlobalVariables.arcadeMode)
                tmpPoints.Add(Obstacles[i]);
            }
            for (int j = 0; j < Obstacles.Count; j++)
            {
                int obNum = Random.Range(0, Obstacles.Count - 1 - j);
                Vector3 tmpPos = new Vector3(tmpNum[obNum].transform.position.x, tmpNum[obNum].transform.position.y, z);
                int sz = Random.Range(0, 3);
                float sizE = 0;
                if (sz == 0)
                    sizE = 0.25f;
                else if (sz == 1)
                    sizE = 0;
                else if (sz == 2)
                    sizE = -0.25f;
               
                tmpNum.Remove(tmpNum[obNum]);
                GameObject tmp = Instantiate(refAntiBody, tmpPos, refAntiBody.transform.rotation) as GameObject;
                tmp.transform.localScale = new Vector3(tmp.transform.localScale.x + sizE, tmp.transform.localScale.y + sizE, tmp.transform.localScale.z + sizE);
                randPositions.Add(tmp);
                if(j%7 == 0 && GlobalVariables.arcadeMode)
                {
                    obNum = Random.Range(0, Obstacles.Count - 1 - j);
                    tmpPos = new Vector3(tmpPoints[obNum].transform.position.x, tmpPoints[obNum].transform.position.y, z);
                    tmpPlats.Remove(tmpPoints[obNum]);
                    tmp = Instantiate(platelet, tmpPos, platelet.transform.rotation) as GameObject;
                    platelets.Add(tmp);
                }
                    
                if(j%2 == 0 && GlobalVariables.arcadeMode)
                {
                    obNum = Random.Range(0, Obstacles.Count - 1 - j);
                    tmpPos = new Vector3(tmpPoints[obNum].transform.position.x, tmpPoints[obNum].transform.position.y, z);
                    tmpPoints.Remove(tmpPoints[obNum]);
                    tmp = Instantiate(refPoint, tmpPos, refPoint.transform.rotation) as GameObject;
                    points.Add(tmp);
                }
               

                if(j%4 == 3)
                    z += zGap;
            }
        }
    }
	void FixedUpdate ()
    {
	    if(levelNum >= 20)
        {
            BannerScript.UnlockTrophy("White Cell");
        }
	}
}
