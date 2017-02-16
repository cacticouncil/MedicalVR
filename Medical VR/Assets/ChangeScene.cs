using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, TimedInputHandler
{

   public int index = 0;



    bool locked = false;

    public void LoadScene(string scene)
    {
        //SoundManager.PlaySFX("MenuEnter");
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
        //SoundManager.PlaySFX("MenuEnter");
        Application.Quit();
    }

   public void EnterEvent()
    {
        switch (index)
        {
            case 0:
                LoadScene("Strategy");
                break;
            case 1:
                LoadScene("MemoryGame");
                break;
            case 2:
                LoadScene("FightVirus"); 
                break;
            case 3:
                LoadScene("DodgeAnitbodies");
                break;
            case 4:
                LoadScene("SimonDNA");
                break;
            case 5:
                LoadScene("CGampSnatcher");
                break;
            case 6:
                LoadScene("ATPGTPShooter");
                break;
            case 7:
                LoadScene("MainMenu");
                break;
            case 8:
                Exit();
                break;
        }
    }

    public void Highlight(int anything)
    {
        index = anything;
    }

    public void HandleTimeInput()
    {
        Highlight(index);
        EnterEvent();
    }
}



