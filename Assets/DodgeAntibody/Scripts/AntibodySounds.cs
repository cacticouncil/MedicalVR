using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntibodySounds : MonoBehaviour {

    public List<AudioClip> bubbleSounds;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        bool playSound = true;
        if(other.tag == "Player")
        {
            //for(int i = 0; i < bubbleSounds.Count; i++)
            //{
            //    if (SoundManager.IsSFXPlaying("StrategyGame/"+bubbleSounds[i].name))
            //        playSound = false;
            //}
            if(playSound == true)
            {
                int r = Random.Range(0, bubbleSounds.Count - 1);
                SoundManager.PlaySFX("StrategyGame/"+bubbleSounds[r].name);
                SoundManager.GetSFXSource(bubbleSounds[r].name).volume = 0.25f;
            }
        }
    }
}
