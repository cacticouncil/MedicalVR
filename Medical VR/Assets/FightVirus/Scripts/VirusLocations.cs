using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VirusLocations : MonoBehaviour
{
    public List<Spawn> VirusLocationList = new List<Spawn>();
    public GameObject VirusManager;
    void Start()
    {
        Spawn S1 = new Spawn(GameObject.Find("1VirusGoTo"));
        Spawn S2 = new Spawn(GameObject.Find("2VirusGoTo"));
        Spawn S3 = new Spawn(GameObject.Find("3VirusGoTo"));
        Spawn S4 = new Spawn(GameObject.Find("4VirusGoTo"));

        VirusLocationList.Add(S1);
        VirusLocationList.Add(S2);
        VirusLocationList.Add(S3);
        VirusLocationList.Add(S4);
    }

    void Update()
    {
        for (int i = 0; i < VirusLocationList.Count; i++)
        {
            if (VirusLocationList[i].SmallVirusCount == 3)
            {
                for (int j = 0; j < VirusManager.GetComponent<VirusManager>().VirusList.Count; j++)
                {
                    if (VirusLocationList[i].VirusList.Contains(VirusManager.GetComponent<VirusManager>().VirusList[j]) && VirusManager.GetComponent<VirusManager>().VirusList[j].gameObject.tag == "Virus")
                    {
                        VirusLocationList[i].VirusList.Remove(VirusManager.GetComponent<VirusManager>().VirusList[j]);
                        Destroy(VirusManager.GetComponent<VirusManager>().VirusList[j]);
                    }
                }

                VirusLocationList[i].SmallVirusCount = 0;
                VirusManager.GetComponent<VirusManager>().CreateBigVirus(VirusLocationList[i].Pos);
            }
        }
    }
}

