﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SmartPause : MonoBehaviour
{
    public GameObject player;
    public GameObject buttons;
    public GameObject mainMenu;
    public GameObject resume;
    public GameObject fade;

    //public GameObject reticle;
    public GameObject[] ui = new GameObject[0];

    [System.NonSerialized]
    public bool isPaused = false;

    public delegate void OnPauseStateChange(bool isPaused);
    public OnPauseStateChange onPSC;

    private float buttonHeldTimer = 0.0f;
    private float angle = 10;

    private bool last = false; //lastRet;
    private bool[] previousStates;
    private GameObject eventReference;

    void Start()
    {
        previousStates = new bool[ui.Length];
    }

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
                    Pause();
                }
            }
            else if (held != last)
            {
                if (Vector3.Angle(player.transform.forward, resume.transform.position - player.transform.position) < angle)
                {
                    Resume();
                    //if (reticle)
                    //{
                    //    reticle.SetActive(lastRet);
                    //    reticle.GetComponent<GvrReticlePointer>().enabled = true;
                    //}

                    for (int i = 0; i < ui.Length; i++)
                    {
                        ui[i].SetActive(previousStates[i]);
                    }
                }
                else if (Vector3.Angle(player.transform.forward, mainMenu.transform.position - player.transform.position) < angle)
                {
                    Resume();
                    ChangeScene.index = 7;
                    if (fade)
                    {
                        fade.GetComponent<FadeIn>().enabled = true;
                        StartCoroutine(Delay());
                    }
                    else
                    {
                        ChangeScene.EnterEvent();
                    }
                }
            }
        }
        else
        {
            buttonHeldTimer = 0;
        }
        last = held;
    }

    public void Pause()
    {
        if (EventSystem.current && EventSystem.current.gameObject)
        {
            eventReference = EventSystem.current.gameObject;
            eventReference.SetActive(false);
        }
        //if (reticle)
        //{
        //lastRet = reticle.activeSelf;
        //reticle.SetActive(true);
        //reticle.GetComponent<GvrReticlePointer>().enabled = false;
        //reticle.GetComponent<Renderer>().material.SetFloat("_InnerDiameter", 0);
        //reticle.GetComponent<Renderer>().material.SetFloat("_OuterDiameter", .09f);
        //reticle.GetComponent<Renderer>().material.SetFloat("_DistanceInMeters", 10);
        //}
        for (int i = 0; i < ui.Length; i++)
        {
            previousStates[i] = ui[i].activeSelf;
            ui[i].SetActive(false);
        }

        buttonHeldTimer = 0.0f;
        buttons.SetActive(true);
        transform.position = player.transform.position + player.transform.forward * .90f;
        transform.LookAt(player.transform.position);
        transform.Rotate(new Vector3(0, 180, 0));
        isPaused = true;
        Time.timeScale = 0;
        //SoundManager.Pause();
        SoundManager.PauseSFX();

        OnPauseStateChange t = onPSC;
        if (t != null)
            t(true);
    }

    public void Resume()
    {
        if (eventReference)
            eventReference.SetActive(true);
        buttons.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        //SoundManager.Resume();
        SoundManager.UnPauseSFX();

        OnPauseStateChange t = onPSC;
        if (t != null)
            t(false);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
        Resume();
        ChangeScene.EnterEvent();
    }
}
