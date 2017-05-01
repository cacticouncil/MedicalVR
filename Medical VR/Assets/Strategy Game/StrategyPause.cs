using UnityEngine;
using System.Collections;

public class StrategyPause : MonoBehaviour
{
    public GameObject PauseManager;
    public GameObject Buttons;
    public GameObject Player;
    public GameObject Unpause;
    public bool isPaused = false;
    public float ButtonHeldTimer = 0.0f;

    void Update()
    {
        bool bHeld = Input.GetButton("Fire1");
        if (bHeld == true)
        {
            if (isPaused == false)
            {
                ButtonHeldTimer += Time.deltaTime;

                if (ButtonHeldTimer >= 1.5f)
                {
                    ButtonHeldTimer = 0.0f;
                    Buttons.SetActive(true);
                    PauseManager.transform.position = Player.transform.position + Player.transform.forward * .45f;
                    PauseManager.transform.LookAt(Player.transform.position);
                    PauseManager.transform.Rotate(new Vector3(0, 180, 0));
                    isPaused = true;
                    Time.timeScale = 0;
                    SoundManager.Pause();
                }
            }
        }
        else
        {
            ButtonHeldTimer = 0;
        }
    }

    public void Resume()
    {
        Buttons.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        SoundManager.Resume();
    }
}
