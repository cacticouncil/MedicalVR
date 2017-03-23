using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrophyLoader : MonoBehaviour {

    public List<GameObject> Trophies = new List<GameObject>();
	void Start ()
    {
        //LockAllTrophies();
        for (int i = 0; i < Trophies.Count; i++)
        {
            if (PlayerPrefs.GetInt(Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TextMesh>().text) == -1)
                Trophies[i].SetActive(false);
            string tmp = Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TextMesh>().text;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    void LockAllTrophies()
    {
        for(int i = 0; i < Trophies.Count; i++)
        {
            PlayerPrefs.SetInt(Trophies[i].GetComponent<TrophyScript>().trophyName.GetComponent<TextMesh>().text, -1);
        }
    }
}
