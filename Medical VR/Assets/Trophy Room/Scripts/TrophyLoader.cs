using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrophyLoader : MonoBehaviour {

    public List<GameObject> Trophies = new List<GameObject>();
    public GameObject unlockedText;
    public Material lockedMaterial;
    int numUnlocked = 0;
	void Start ()
    {
       //LockAllTrophies();
        for (int i = 0; i < Trophies.Count; i++)
        {
            if (PlayerPrefs.GetInt(Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TMPro.TextMeshPro>().text) == 1)
            {
                numUnlocked++;
                Trophies[i].GetComponent<TrophyScript>().unlock.SetActive(false);
            }
            else
            {
                if (Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TMPro.TextMeshPro>().text == "DNA" || Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TMPro.TextMeshPro>().text == "RNA" || Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TMPro.TextMeshPro>().text == "cGAMP")
                {
                    for (int j = 0; j < Trophies[i].transform.GetChild(2).transform.childCount; j++)
                        Trophies[i].transform.GetChild(2).GetChild(j).GetComponent<MeshRenderer>().material = lockedMaterial;
                }
                else
                    Trophies[i].GetComponent<MeshRenderer>().material = lockedMaterial;

                Trophies[i].GetComponent<TrophyScript>().enabled = false;
                Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TMPro.TextMeshPro>().text = "";
                Trophies[i].GetComponent<TrophyScript>().unlock.SetActive(true);
            }
                
        }
        unlockedText.GetComponent<TMPro.TextMeshPro>().text = "Unlocked: " + numUnlocked.ToString() + "/" + Trophies.Count.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    void LockAllTrophies()
    {
        for(int i = 0; i < Trophies.Count; i++)
        {
            PlayerPrefs.SetInt(Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TMPro.TextMeshPro>().text, -1);
            //string tmp = Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TMPro.TextMeshPro>().text;
        }
    }
}
