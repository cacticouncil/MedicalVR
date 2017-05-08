using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    public void SetDifficulty(int i)
    {
        GlobalVariables.difficulty = i;
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
        ChangeScene.EnterEvent();
    }

    public void SetAndEnter(int i)
    {
        ChangeScene.index = i;
        ChangeScene.EnterEvent();
    }
}
