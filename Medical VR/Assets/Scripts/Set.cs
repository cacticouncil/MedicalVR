using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    private static Set localInstance;
    public static Set instance { get { return localInstance; } }

    public GameObject fade;
    public float fadeWaitDelay = 1.0f;

    private void Awake()
    {
        if (localInstance != null && localInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            localInstance = this;
        }
    }

    public void SetDifficulty(int i)
    {
        StrategyCellManagerScript.difficulty = i;
    }

    public void SetTutorial(bool s)
    {
        GlobalVariables.tutorial = s;
    }

    public void SetIndex(int i)
    {
        ChangeScene.index = i;
    }

    public static void EnterSceneStatic()
    {
        SoundManager.PlaySFX("MenuEnter");
        if (FadeIn.instance)
        {
            FadeIn.instance.enabled = true;
        }
        instance.StartCoroutine(instance.EnterDelay());
    }

    public void EnterScene()
    {
        SoundManager.PlaySFX("MenuEnter");
        if (fade)
        {
            fade.GetComponent<FadeIn>().enabled = true;
        }
        StartCoroutine(EnterDelay());
    }

    public static void SetAndEnterStatic(int i)
    {
        SoundManager.PlaySFX("MenuEnter");
        ChangeScene.index = i;
        if (FadeIn.instance)
        {
            FadeIn.instance.enabled = true;
        }
        instance.StartCoroutine(instance.EnterDelay());
    }

    public void SetAndEnter(int i)
    {
        SoundManager.PlaySFX("MenuEnter");
        ChangeScene.index = i;
        if (fade)
        {
            fade.GetComponent<FadeIn>().enabled = true;
        }
        StartCoroutine(EnterDelay());
    }

    IEnumerator EnterDelay()
    {
        yield return new WaitForSeconds(fadeWaitDelay);
        ChangeScene.EnterEvent();
    }
}