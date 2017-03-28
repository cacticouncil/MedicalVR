using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrophyLoader : MonoBehaviour {

    public List<GameObject> Trophies = new List<GameObject>();
    public GameObject unlockedText;
    int numUnlocked = 0;
	void Start ()
    {
        LockAllTrophies();
        for (int i = 0; i < Trophies.Count; i++)
        {
            if (PlayerPrefs.GetInt(Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TMPro.TextMeshPro>().text) == -1)
                Trophies[i].SetActive(false);
            else
                numUnlocked++;
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
            PlayerPrefs.SetInt(Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TMPro.TextMeshPro>().text, 1);
            string tmp = Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TMPro.TextMeshPro>().text;
        }
    }
}
