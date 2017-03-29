using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyTutorialR : MonoBehaviour
{
    public int reproduction = 0;
    public int repCap = 5;
    public TMPro.TextMeshPro text;
    public GameObject left, right;

    public void Click()
    {
        reproduction++;
        text.text = "Reproduction: " + reproduction;
        left.GetComponent<MoveLeftOrRight>().IncreaseSpeed();
        right.GetComponent<MoveLeftOrRight>().IncreaseSpeed();
    }
}
