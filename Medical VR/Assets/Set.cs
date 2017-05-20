using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    public GameObject fade;

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
            StartCoroutine(EnterDelay());
        }
        else
            ChangeScene.EnterEvent();
    }

    public void SetAndEnter(int i)
    {
        SoundManager.PlaySFX("MenuEnter");
        ChangeScene.index = i;
        if (fade)
        {
            fade.GetComponent<FadeIn>().enabled = true;
            StartCoroutine(EnterDelay());
        }
        else
            ChangeScene.EnterEvent();
    }

    IEnumerator EnterDelay()
    {
        yield return new WaitForSeconds(1.0f);
        ChangeScene.EnterEvent();
    }
}
