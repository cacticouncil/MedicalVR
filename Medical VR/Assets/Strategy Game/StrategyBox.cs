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
    public GameObject animationTab;
    public GameObject anim;
    public TextMesh animationText;

    public MoveCamera mainCamera;
    public Vector3 startingPosition;
    public float scaledDistance = 1.5f;

    void Start()
    {
        startingPosition = transform.position;
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
        Vector3 finalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

        //This is the new target position
        mainCamera.SetDestination(finalPos);
        cellmanager.Unselect();
    }

    public void Back()
    {
        mainCamera.SetDestination(new Vector3(1, 5, 0));
    }

    public void ToggleUI()
    {

    }
}
