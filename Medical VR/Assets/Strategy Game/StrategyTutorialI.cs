using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyTutorialI : MonoBehaviour
{
    public int immunity = 1;
    public int immCap = 15;
    public TextMesh text;
    public GameObject virus, virus2, cell, protein, arrows;

    private enum Status
    {
        Protein,
        Moving,
        Dieing,
        Attacking,
        Kill
    }
    private Status stat;
    private Vector3 startPos;
    private Vector3 pstartPos, pstartScale;
    private float startTime;
    private bool enumerStarted;
    void Start()
    {
        startPos = virus.transform.position;
        pstartPos = protein.transform.position;
        pstartScale = protein.transform.localScale;
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
            case Status.Protein:
                {
                    float percent = (Time.time - startTime);
                    protein.transform.position = Vector3.Lerp(pstartPos, cell.transform.position, percent);
                    protein.transform.localScale = Vector3.Lerp(pstartScale, new Vector3(0, 0, 0), percent);
                    if (percent >= 1.0f)
                    {
                        startTime = Time.time;
                        stat = Status.Moving;
                        cell.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                }
                break;
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
        if (immunity < immCap)
        {
            immunity++;
            text.text = "Immunity: " + immunity;
        }
    }

    IEnumerator VirusAttack()
    {
        enumerStarted = true;
        Debug.Log("Attacking");
        if (!protein.activeSelf)
        {
            cell.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0, 0);
            yield return new WaitForSeconds(1.0f);
            cell.GetComponent<SpriteRenderer>().enabled = false;
            virus2.gameObject.SetActive(true);
        }
        else
        {
            virus.GetComponent<SpriteRenderer>().color = Color.red;
            cell.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(1.0f);
            virus.SetActive(false);
            arrows.SetActive(true);
        }
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
        if (immunity >= immCap)
        {
            protein.SetActive(true);
            protein.transform.position = pstartPos;
            protein.transform.localScale = pstartScale;
            virus.SetActive(true);
            virus.GetComponent<SpriteRenderer>().color = Color.white;
            arrows.SetActive(false);
            cell.GetComponent<SpriteRenderer>().sortingOrder = 0;
            yield return new WaitForSeconds(1.0f);
            stat = Status.Protein;
        }
        else
        {
            cell.GetComponent<SpriteRenderer>().color = Color.green;
            stat = Status.Moving;
        }
        enumerStarted = false;
        startTime = Time.time;
    }
}
