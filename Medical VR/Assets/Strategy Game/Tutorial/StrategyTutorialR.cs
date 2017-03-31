using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyTutorialR : MonoBehaviour
{
    public StrategyTutorialReproduce r;
    public TMPro.TextMeshPro text;

    public void Click()
    {
        r.reproduction++;
        text.text = "Reproduction: " + r.reproduction;
    }
}
