using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyTutorialScript : MonoBehaviour
{
    public List<GameObject> tutorials;
    private int index = 0;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject cellManager;
    public GameObject screenUI;

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Next()
    {
        index++;
        if (index > tutorials.Count - 1)
        {
            gameObject.SetActive(false);
            cellManager.SetActive(true);
        }
        else if (index == tutorials.Count - 1)
        {
            rightArrow.SetActive(false);
            tutorials[index - 1].SetActive(false);
            tutorials[index].SetActive(true);
        }
        else
        {
            if (!leftArrow.activeSelf)
                leftArrow.SetActive(true);
            tutorials[index - 1].SetActive(false);
            tutorials[index].SetActive(true);
        }
    }

    public void Previous()
    {
        index--;
        if (index < 0)
        {
            gameObject.SetActive(false);
            cellManager.SetActive(true);
        }
        else if (index == 0)
        {
            leftArrow.SetActive(false);
            tutorials[index + 1].SetActive(false);
            tutorials[index].SetActive(true);
        }
        else
        {
            if (!rightArrow.activeSelf)
                rightArrow.SetActive(true);
            tutorials[index + 1].SetActive(false);
            tutorials[index].SetActive(true);
        }
    }
}
