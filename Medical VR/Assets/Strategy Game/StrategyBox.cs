using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyBox : MonoBehaviour
{
    public StrategyCellManagerScript cellmanager;

    public List<StrategyItem> items = new List<StrategyItem>();
    public List<TMPro.TextMeshPro> text = new List<TMPro.TextMeshPro>();

    public int actionsLeft;
    public TMPro.TextMeshPro actionText;

    public GameObject boxTab;
    public Hilighter AHighlighter;
    public GameObject animationTab;
    public GameObject anim;
    public TMPro.TextMeshPro animationText;

    public MoveCamera mainCamera;

    private Vector2 key = new Vector2(500, 500);
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
            anim.GetComponent<MeshRenderer>().material = items[item].material;
            anim.GetComponent<MeshFilter>().mesh = items[item].mesh;
            animationText.text = "You Received " + items[item].type;
            animationTab.SetActive(true);

            items[item].count++;
            text[item].text = items[item].count.ToString();
            boxTab.SetActive(false);
        }
        else
        {
            actionText.text = "Actions Left: " + actionsLeft;
        }
    }

    public void MoveTo()
    {
        if (key != cellmanager.selected)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Collider>().enabled = false;
            //This is the new target position
            mainCamera.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f));
            cellmanager.SetSelected(key);
        }
    }

    public void ToggleUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<Collider>().enabled = true;
    }
}
