using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, TimedInputHandler
{
    public int index = 0;

    public void LoadScene(string scene)
    {
        SoundManager.PlaySFX("MenuEnter");
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
        SoundManager.PlaySFX("MenuEnter");
        Application.Quit();
    }

    public void SetAndEnter(int i)
    {
        index = i;
        EnterEvent();
    }

    public void EnterEvent()
    {
        SoundManager.PlaySFX("MenuEnter");
        GlobalVariables.arcadeMode = true;
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
                SoundManager.DestroySFX();
                LoadScene("MainMenu");
                break;
            case 8:
                Exit();
                break;
            case 9:
                LoadScene("MinigameMenu");
                break;
            case 10:
                LoadScene("TrophyRoom");
                break;
            case 11:
                LoadScene("Credits");
                break;
            case 12:
                LoadScene("OptionsMenu");
                break;
            case 13:
                LoadScene("DestroyTheCell");
                break;
            case 14:
                VirusGameplayScript.loadCase = 0;
                GlobalVariables.arcadeMode = false;
                LoadScene("VirusGameplay");
                break;
            case 15:
                CellGameplayScript.loadCase = 0;
                GlobalVariables.arcadeMode = false;
                LoadScene("CellGameplay");
                break;
        }
    }

    public void SetIndex(int anything)
    {
        index = anything;
    }

    public void HandleTimeInput()
    {
        EnterEvent();
    }
}



