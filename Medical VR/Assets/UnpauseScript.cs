using UnityEngine;
using System.Collections;

public class UnpauseScript : MonoBehaviour
{
    public GameObject PauseManager;
    public GameObject Buttons;
    public GameObject Player;

    public void UnpauseGame()
    {
        Buttons.SetActive(false);
        PauseManager.GetComponent<Pause>().isPaused = false;
        Time.timeScale = 1;
        SoundManager.Resume();
    }
}
