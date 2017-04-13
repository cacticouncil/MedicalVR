using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyUIScript : MonoBehaviour
{
    public GameObject[] items;
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
        if (transform.parent.GetComponent<StrategyCellScript>().hosted)
        {
            foreach (GameObject item in items)
            {
                item.SetActive(false);
            }
            if (inventory[inventory.Count - 1].count > 0)
            {
                items[inventory.Count - 1].SetActive(true);
                items[inventory.Count - 1].GetComponentInChildren<TMPro.TextMeshPro>().text = inventory[inventory.Count - 1].count.ToString();
            }
        }
        else
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                if (inventory[i].count > 0)
                {
                    items[i].SetActive(true);
                    items[i].GetComponentInChildren<TMPro.TextMeshPro>().text = inventory[i].count.ToString();
                }
                else
                {
                    items[i].SetActive(false);
                }
            }
            items[inventory.Count - 1].SetActive(false);
        }
    }
}
