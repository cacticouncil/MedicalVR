using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    public GameObject MainMenu;
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

                if (ButtonHeldTimer >= 2.0f)
                {
                    ButtonHeldTimer = 0.0f;
                    MainMenu.SetActive(true);
                    //MainMenu.transform.position = Camera.main.transform.forward + Camera.main.transform.position;
                    MainMenu.transform.position = Player.transform.forward;
                    MainMenu.transform.LookAt(Player.transform.position);
                    MainMenu.transform.Rotate(new Vector3(0, 180, 0));
                    isPaused = true;
                    Time.timeScale = 0;
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
