using UnityEngine;
using TMPro;
using System.Collections;

public class DisplayScore : MonoBehaviour
{
    public GameObject GetLists;

    void Update()
    {
        if (transform.name == "Zone 1")
        {
            transform.GetComponent<TextMeshPro>().text = "Zone 1 " + "\n" + "     " + GetLists.GetComponent<VirusLocations>().VirusLocationList[0].VirusList.Count;
        }

        else if (transform.name == "Zone 2")
        {
            transform.GetComponent<TextMeshPro>().text = "Zone 2 " + "\n" + "     " + GetLists.GetComponent<VirusLocations>().VirusLocationList[1].VirusList.Count;
        }

        else if (transform.name == "Zone 3")
        {
            transform.GetComponent<TextMeshPro>().text = "Zone 3 " + "\n" + "     " + GetLists.GetComponent<VirusLocations>().VirusLocationList[2].VirusList.Count;
        }

        else if (transform.name == "Zone 4")
        {
            transform.GetComponent<TextMeshPro>().text = "Zone 4 " + "\n" + "     " + GetLists.GetComponent<VirusLocations>().VirusLocationList[3].VirusList.Count;
        }
    }
}
