using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AntibodySpawnerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject antibody;
    public GameObject platelet;
    public GameObject point;
    public int levelNum;
    public float zGap;
    public List<Transform> obstacles = new List<Transform>();
    public GameObject theLevel, obsStart, obsEnd;
    [Range(.1f, .5f)]
    public float pAntibodies, pPoints, pPlatelet;

    List<GameObject> instances = new List<GameObject>();

    // Use this for initialization
    void Start()
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
    void Update()
    {
        foreach (Renderer item in GetComponentsInChildren<Renderer>())
        {
            if (Vector3.Distance(item.transform.position, player.transform.position) > 36)
            {
                item.enabled = false;
            }
            else
            {
                item.enabled = true;
            }
        }
    }
    public void GenerateObstacles()
    {
        levelNum++;
        if (levelNum >= 20)
        {
            BannerScript.UnlockTrophy("White Cell");
        }
        theLevel.GetComponent<TMPro.TextMeshPro>().text = "LEVEL: " + levelNum.ToString();

        //Clean up spawned obstacles
        if (instances.Count != 0)
        {
            for (int i = 0; i < instances.Count; i++)
            {
                Destroy(instances[i]);
            }
            instances.Clear();
        }

        for (float z = obsStart.transform.position.z; z < obsEnd.transform.position.z; z += zGap)
        {
            for (int j = 0; j < obstacles.Count; j++)
            {
                float obType = Random.Range(0.0f, 1.0f);
                float size = Random.Range(0, 3);
                if (size < 1)
                    size = 0.25f;
                else if (size < 2)
                    size = -0.25f;
                else
                    size = 0;

                float loc = Random.Range(0, 3);
                if (loc < 1)
                    loc = zGap * .333f;
                else if (loc < 2)
                    loc = zGap * .666f;
                else
                    loc = 0;

                GameObject spawnType = null;

                //Spawn type antibody
                if (obType < pAntibodies)
                {
                    spawnType = antibody;
                }
                //Spawn type point
                else if (GlobalVariables.arcadeMode && obType < pAntibodies + pPoints)
                {
                    spawnType = point;
                }
                //Spawn type platlet
                else if (GlobalVariables.arcadeMode && obType < pPlatelet + pPoints + pAntibodies)
                {
                    spawnType = platelet;
                }

                if (spawnType != null)
                {
                    GameObject tmp = Instantiate(spawnType, new Vector3(obstacles[j].position.x + Random.Range(-1.0f, 1.0f), obstacles[j].position.y + Random.Range(-1.0f, 1.0f), z + loc), Quaternion.identity, transform) as GameObject;
                    tmp.transform.localScale = new Vector3(tmp.transform.localScale.x + size, tmp.transform.localScale.y + size, tmp.transform.localScale.z + size);
                    //tmp.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
                    instances.Add(tmp);
                }
            }
        }
    }
}
