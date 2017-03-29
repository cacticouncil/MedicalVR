using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyTutorialD : MonoBehaviour
{
    public float defense = 1;
    public float hp = 1;
    public TMPro.TextMeshPro text;
    public GameObject virus, virus2, cell;

    private enum Status
    {
        Moving,
        Attacking,
        Kill
    }
    private Status stat;
    private Vector3 startPos;
    private float startTime;
    private bool enumerStarted;
    void Start()
    {
        startPos = virus.transform.position;
        startTime = Time.time;
        stat = Status.Moving;
        enumerStarted = false;
        cell.GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("Moving");
    }

    // Update is called once per frame
    void Update()
    {
        switch (stat)
        {
            case Status.Moving:
                {
                    float percent = (Time.time - startTime);
                    virus.transform.position = Vector3.Lerp(startPos, cell.transform.position, percent);
                    if (percent >= 1.0f)
                        stat = Status.Attacking;
                }
                break;
            case Status.Attacking:
                {
                    if (!enumerStarted)
                        StartCoroutine(VirusAttack());
                }
                break;
            case Status.Kill:
                {
                    if (!enumerStarted)
                        StartCoroutine(Reset());
                }
                break;
        }
    }

    public void Click()
    {
        defense++;
        text.text = "Defense: " + defense;
    }

    IEnumerator VirusAttack()
    {
        enumerStarted = true;
        Debug.Log("Attacking");
        while (hp > 0)
        {
            cell.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0, 0);
            yield return new WaitForSeconds(.9f);
            hp -= 1;
            cell.GetComponent<SpriteRenderer>().color = new Color(.8f, .2f, .2f);
            yield return new WaitForSeconds(.1f);
        }
        cell.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0, 0);
        yield return new WaitForSeconds(.1f);
        cell.GetComponent<SpriteRenderer>().enabled = false;
        virus2.gameObject.SetActive(true);
        stat = Status.Kill;
        enumerStarted = false;
    }

    IEnumerator Reset()
    {
        enumerStarted = true;
        Debug.Log("Resetting");
        yield return new WaitForSeconds(1.0f);
        virus.transform.position = startPos;
        cell.GetComponent<SpriteRenderer>().enabled = true;
        cell.GetComponent<SpriteRenderer>().color = Color.white;
        virus2.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Moving");
        hp = Mathf.Sqrt(defense * 5 + 1);
        cell.GetComponent<SpriteRenderer>().color = Color.green;
        stat = Status.Moving;
        enumerStarted = false;
        startTime = Time.time;
    }
}
