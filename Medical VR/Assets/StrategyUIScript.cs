using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyUIScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] items = new GameObject[6];
    [SerializeField]
    List<StrategyItem> inventory;

    void Awake()
    {
        Debug.Log("Awake called");
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
            if (inventory[5].count > 0)
            {
                items[5].SetActive(true);
                items[5].GetComponentInChildren<TextMesh>().text = inventory[5].count.ToString();
            }
            else
            {
                items[5].SetActive(false);
            }
            items[0].SetActive(false);
            items[1].SetActive(false);
            items[2].SetActive(false);
            items[3].SetActive(false);
            items[4].SetActive(false);
        }
        else
        {
            items[5].SetActive(false);
            if (inventory[0].count > 0)
            {
                items[0].SetActive(true);
                items[0].GetComponentInChildren<TextMesh>().text = inventory[0].count.ToString();
            }
            else
                items[0].SetActive(false);
            if (inventory[1].count > 0)
            {
                items[1].SetActive(true);
                items[1].GetComponentInChildren<TextMesh>().text = inventory[1].count.ToString();
            }
            else
                items[1].SetActive(false);
            if (inventory[2].count > 0)
            {
                items[2].SetActive(true);
                items[2].GetComponentInChildren<TextMesh>().text = inventory[2].count.ToString();
            }
            else
                items[2].SetActive(false);
            if (inventory[3].count > 0)
            {
                items[3].SetActive(true);
                items[3].GetComponentInChildren<TextMesh>().text = inventory[3].count.ToString();
            }
            else
                items[3].SetActive(false);
            if (inventory[4].count > 0)
            {
                items[4].SetActive(true);
                items[4].GetComponentInChildren<TextMesh>().text = inventory[4].count.ToString();
            }
            else
                items[4].SetActive(false);
        }
    }
}
