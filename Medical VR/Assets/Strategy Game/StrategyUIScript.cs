using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyUIScript : MonoBehaviour
{
    public TMPro.TextMeshPro[] texts;
    [SerializeField]
    List<StrategyItem> inventory;

    void Awake()
    {
        inventory = transform.parent.transform.parent.GetComponent<StrategyCellManagerScript>().inventory;
    }

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = inventory[i].count.ToString();
            if (inventory[i].count > 0)
            {
                texts[i].color = Color.white;
            }
            else
            {
                texts[i].color = Color.red;
            }
        }
        if (transform.parent.GetComponent<StrategyCellScript>().hosted)
        {
            foreach (TMPro.TextMeshPro text in texts)
            {
                text.color = Color.red;
            }
            if (inventory[inventory.Count - 1].count > 0)
            {
                texts[inventory.Count - 1].color = Color.white;
            }
        }
    }
}
