﻿using UnityEngine;
using System.Collections;

public class SmartPause : MonoBehaviour
{
    public GameObject player;
    public GameObject buttons;
    public GameObject mainMenu;
    public GameObject resume;
    public ChangeScene sceneController;
    public bool isPaused = false;
    public float buttonHeldTimer = 0.0f;
    public float angle = 30;

    private bool last = false;

    void Update()
    {
        bool held = Input.GetButton("Fire1");
        if (held)
        {
            if (!isPaused)
            {
                buttonHeldTimer += Time.deltaTime;
                if (buttonHeldTimer >= 1.5f)
                {
                    buttonHeldTimer = 0.0f;
                    buttons.SetActive(true);
                    transform.position = player.transform.position + player.transform.forward * .45f;
                    transform.LookAt(player.transform.position);
                    transform.Rotate(new Vector3(0, 180, 0));
                    isPaused = true;
                    Time.timeScale = 0;
                    SoundManager.Pause();
                }
            }
            else if (held != last)
            {
                if (Vector3.Angle(player.transform.forward, resume.transform.position - player.transform.position) < angle)
                {
                    Resume();
                }
                else if (Vector3.Angle(player.transform.forward, mainMenu.transform.position - player.transform.position) < angle)
                {
                    Resume();
                    sceneController.index = 7;
                    sceneController.EnterEvent();
                }
            }
        }
        else
        {
            buttonHeldTimer = 0;
        }
        last = held;
    }

    public void Resume()
    {
        buttons.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        SoundManager.Resume();
    }
}
