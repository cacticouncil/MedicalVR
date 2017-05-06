using UnityEngine;
using System.Collections;

public class PauseCapsules : MonoBehaviour {
    public GameObject pausemenu;
    public GameObject capsules;
    public GameObject victoryeffect;
    // Use this for initialization
    void Start () {


	}
	
	// Update is called once per frame
	void Update () {
        if (pausemenu.GetComponent<Pause>().isPaused == true)
        {
            capsules.SetActive(false);
        }
        else if(pausemenu.GetComponent<Pause>().isPaused == false && victoryeffect.GetComponent<ParticleSystem>().isPlaying == false)
        {
            capsules.SetActive(true);
        }

    }
}
