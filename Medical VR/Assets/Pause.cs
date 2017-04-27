using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
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
                    PauseManager.transform.position = Player.transform.position + Player.transform.forward;
                    PauseManager.transform.LookAt(Player.transform.position);
                    PauseManager.transform.Rotate(new Vector3(0, 180, 0));
                    isPaused = true;
                    Time.timeScale = 0;
                    SoundManager.PauseSFX();
                }
            }
        }

        else
        {
            ButtonHeldTimer -= Time.deltaTime;

            if (ButtonHeldTimer <= 0)
                ButtonHeldTimer = 0;
        }
    }
}
