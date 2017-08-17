using UnityEngine;
using System.Collections;

public class PauseCapsules : MonoBehaviour
{
    public SmartPause pausemenu;
    public GameObject capsules;
    public GameObject sphere;
    public GameObject victoryeffect;

    // Update is called once per frame
    void Start()
    {
        pausemenu.onPSC += PSC;
    }

    void PSC(bool isPaused)
    {
        if (victoryeffect.GetComponent<ParticleSystem>().isPlaying == false)
        {
            capsules.SetActive(!isPaused);
            sphere.SetActive(!isPaused);
        }
    }

    void OnDestroy()
    {
        if (pausemenu)
        {
            pausemenu.onPSC -= PSC;
        }
    }
}
