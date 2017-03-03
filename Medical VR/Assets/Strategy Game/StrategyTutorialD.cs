using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyTutorialD : MonoBehaviour
{
    public int defense = 1;
    public int defCap = 5;
    public TextMesh text;
    public GameObject virus, cell;

    public void Click()
    {
        if (defense < defCap)
        {
            defense++;
            text.text = "Defense: " + defense;
        }
    }

    
}
