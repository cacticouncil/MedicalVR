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
        //Get the direction of the player from the cell
        Vector3 heading = mainCamera.transform.position - transform.position;
        //Don't change y value
        heading.y = 0;
        //Find normalized direction
        float distance = Mathf.Max(heading.magnitude, .001f);
        Vector3 direction = heading / distance;
        if (direction.magnitude < 0.01f)
        {
            direction = new Vector3(0.0f, 0.0f, 1.0f);
        }
        //Scale it to 1.5
        direction *= scaledDistance;

        Vector3 finalPos = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);

        transform.GetChild(0).transform.LookAt(finalPos);

        //This is the new target position
        mainCamera.SetDestination(finalPos);
        cellmanager.Unselect();
    }

    public void Back()
    {
        mainCamera.SetDestination(new Vector3(1, 5, 0));
    }
}
