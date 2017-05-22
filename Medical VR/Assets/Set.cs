using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    public GameObject fade;
    public float fadeWaitDelay = 1.0f;

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

    public void EnterScene()
    {
        SoundManager.PlaySFX("MenuEnter");
        if (fade)
        {
            fade.GetComponent<FadeIn>().enabled = true;
        }
        StartCoroutine(EnterDelay());
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