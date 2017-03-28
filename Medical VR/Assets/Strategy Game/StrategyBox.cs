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

    public GameObject boxTab;
    public Hilighter AHighlighter;
    public GameObject animationTab;
    public GameObject anim;
    public TextMesh animationText;

    public MoveCamera mainCamera;

    private Vector2 key = new Vector2(-100, -100);
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.GetComponent<MoveCamera>();
        }
    }

    public void AClick()
    {
        actionsLeft--;
        cellmanager.ActionPreformed();
        if (actionsLeft == 0)
        {
            actionsLeft = 4;
            actionText.text = "Actions Left: " + actionsLeft;
            int item = Random.Range(0, items.Count);
            AHighlighter.SetGazedAt(false);

            //play animation
            anim.GetComponent<SpriteRenderer>().sprite = items[item].image;
            animationText.text = "You Received " + items[item].type;
            animationTab.SetActive(true);

            items[item].count++;
            text[item].text = items[item].count.ToString();
            boxTab.SetActive(false);
        }
        else
            actionText.text = "Actions Left: " + actionsLeft;
    }

    public void MoveTo()
    {
        if (key != cellmanager.selected)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Collider>().enabled = false;
            //This is the new target position
            mainCamera.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f));
            cellmanager.SetSelected(key);
        }
    }

    public void ToggleUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<Collider>().enabled = true;
    }
}
