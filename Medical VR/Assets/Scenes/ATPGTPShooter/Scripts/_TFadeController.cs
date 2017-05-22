using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TFadeController : MonoBehaviour
{
    // public GameObject fadeScreen;
    public GameObject gameCongroller;
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
        if (gameCongroller)
        {
            _TGameController gc = gameCongroller.GetComponent<_TGameController>();
            gc.FadeScreen();
            gc.scoreBoard.GetComponent<_TSizeChange>().StartShrink();
        }
        StartCoroutine(EnterDelay());
    }

    public void SetAndEnter(int i)
    {
        ChangeScene.index = i;
        EnterScene();
    }

    IEnumerator EnterDelay()
    {
        yield return new WaitForSeconds(fadeWaitDelay);
        ChangeScene.EnterEvent();
    }
}
