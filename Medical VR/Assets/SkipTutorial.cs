using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SkipTutorial : MonoBehaviour
{
    public static bool GlobalPause = false;
    public GameObject SkipTutorialAndArcade;
    int i = 0;

	void Update ()
    {
        if (GlobalPause == true)
        {
            if(i == 30)
            Time.timeScale = 0;
        }
        i++;
    }

    public void UnPauseTutorial()
    {
        Time.timeScale = 1;
        SkipTutorialAndArcade.SetActive(false);
    }

    public void LoadArcade()
    {
        Player.TutorialMode = false;
        Player.ArcadeMode = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("FightVirus");
    }
}
