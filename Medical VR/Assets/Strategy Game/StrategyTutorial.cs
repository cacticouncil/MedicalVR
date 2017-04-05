using UnityEngine;
using System.Collections;

public class StrategyTutorial : MonoBehaviour
{
    public GameObject redCell;
    public GameObject virus;
    public GameObject whiteCell;
    public Vector3 cellPosition;
    public SimulateSun sun;
    public int cNum = 0;

    public TMPro.TextMeshPro[] text;

    private int rNum = 0;

    public void Click()
    {
        switch (cNum)
        {
            case 11:
                StartCoroutine(TurnTextOn(2));

                break;
            case 4:
                StartCoroutine(TurnTextOn(1));
                StartCoroutine(SpawnRedCell());
                break;
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                StartCoroutine(SpawnRedCell());
                break;

            default:
                sun.TurnUpdate();
                break;
        }
        cNum++;
    }

    IEnumerator SpawnRedCell()
    {
        GameObject c = Instantiate(redCell, cellPosition, redCell.transform.rotation) as GameObject;
        Vector3 desination;
        switch (rNum)
        {
            case 1:
                //Top Right (1, 1)
                desination = new Vector3(2, transform.position.y, 2);
                break;
            case 2:
                //Right (1, 0)
                desination = new Vector3(3, transform.position.y, 0);
                break;
            case 3:
                //Bottom Right (1, -1)
                desination = new Vector3(2, transform.position.y, -2);
                break;
            case 4:
                //Bottom Left (0, -1)
                desination = new Vector3(0, transform.position.y, -2);
                break;
            case 5:
                //Left (-1, 0)
                desination = new Vector3(-1, transform.position.y, 0);
                break;
            case 6:
                //Top Left (0, 1)
                desination = new Vector3(0, transform.position.y, 2);
                break;
            default:
                desination = cellPosition;
                break;
        }
        rNum++;

        float startTime = Time.time;
        float percent = Time.time - startTime;
        while (percent < 1.0f)
        {
            percent = Time.time - startTime;
            c.transform.position = Vector3.Lerp(cellPosition, desination, percent);
            c.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent);
            yield return 0;
        }
        c.transform.position = desination;
        c.transform.localScale = Vector3.one;
    }

    IEnumerator TurnTextOn(int index)
    {
        Color a = text[index - 1].color;
        float startTime = Time.time;
        float percent = 0.0f;
        while (percent < 1.0f)
        {
            percent = (Time.time - startTime) * 2.0f;
            a.a = Mathf.Lerp(1.0f, 0.0f, percent);
            text[index - 1].color = a;
            yield return 0;
        }
        a.a = 0.0f;
        text[index - 1].color = a;
        yield return 0;
        text[index - 1].gameObject.SetActive(false);
        text[index].gameObject.SetActive(true);
        a = text[index].color;
        a.a = 0.0f;
        text[index].color = a;
        startTime = Time.time;
        percent = (Time.time - startTime);
        while (percent < 1.0f)
        {
            percent = (Time.time - startTime) * 2.0f;
            a.a = Mathf.Lerp(0.0f, 1.0f, percent);
            text[index].color = a;
            yield return 0;
        }
        a.a = 1.0f;
        text[index].color = a;
    }
}
