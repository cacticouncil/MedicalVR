using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyBox : MonoBehaviour
{
    public StrategyCellManagerScript cellmanager;

    public List<StrategyItem> items = new List<StrategyItem>();
    public List<TextMesh> text = new List<TextMesh>();

    public int actionsLeft;
    public TextMesh actionText;

    public GameObject animationTab;
    public GameObject anim;
    public TextMesh animationText;

    public void Click()
    {
        actionsLeft--;
        cellmanager.ActionPreformed();
        if (actionsLeft == 0)
        {
            actionsLeft = 4;
            actionText.text = "Actions Left: " + actionsLeft;
            int item = Random.Range(0, items.Count);

            //play animation
            anim.GetComponent<SpriteRenderer>().sprite = items[item].image;
            animationText.text = "You Received " + items[item].type;
            animationTab.SetActive(true);

            items[item].count++;
            text[item].text = items[item].count.ToString();
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
            actionText.text = "Actions Left: " + actionsLeft;
    }
}
