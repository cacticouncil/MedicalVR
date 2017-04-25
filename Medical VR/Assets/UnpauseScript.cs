using UnityEngine;
using System.Collections;

public class UnpauseScript : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Player;
    public GameObject Pause;

    public void UnpauseGame()
    {
        MainMenu.SetActive(false);
        Pause.GetComponent<Pause>().isPaused = false;
        Time.timeScale = 1;
    }
}
