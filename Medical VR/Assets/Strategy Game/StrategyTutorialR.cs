using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyTutorialR : MonoBehaviour
{
    public int reproduction = 1;
    public int repCap = 5;
    public TextMesh text;
    public GameObject left, right;

    public void Click()
    {
        if (reproduction < repCap)
        {
            reproduction++;
            text.text = "Reproduction: " + reproduction;
            left.GetComponent<MoveLeftOrRight>().IncreaseSpeed();
            right.GetComponent<MoveLeftOrRight>().IncreaseSpeed();
        }
    }
}
