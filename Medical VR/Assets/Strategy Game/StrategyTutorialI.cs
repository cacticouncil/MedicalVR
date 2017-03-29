using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyTutorialI : MonoBehaviour
{
    public int immunity = 0;
    public int tImmunity = 0;
    public TMPro.TextMeshPro text;
    public GameObject virus, virus2, cell, protein, arrow1, arrow2;
    public Transform arrow1End, arrow2End;

    private enum Status
    {
        Wait,
        Arrows,
        Protein,
        Moving,
        Dieing,
        Attacking,
        Kill
    }
    private Status stat;
    private Vector3 startPos, arrow1StartPos, arrow1StartScale, arrow2StartPos, arrow2StartScale, pstartPos, pstartScale;
    private float startTime;
    private bool enumerStarted;
    void Start()
    {
        startPos = virus.transform.position;
        arrow1StartPos = arrow1.transform.localPosition;
        arrow2StartPos = arrow2.transform.localPosition;
        arrow1StartScale = arrow1.transform.localScale;
        arrow2StartScale = arrow2.transform.localScale;
        pstartPos = protein.transform.position;
        pstartScale = protein.transform.localScale;
        startTime = Time.time;
        stat = Status.Wait;
        enumerStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stat)
        {
            case Status.Wait:
                {
                    if (Time.time - startTime >= 1.0f)
                    {
                        if (tImmunity >= 1)
                        {
                            stat = Status.Arrows;
                        }
                        else
                        {
                            stat = Status.Moving;
                            cell.GetComponent<SpriteRenderer>().color = Color.green;
                        }
                        startTime = Time.time;
                    }
                }
                break;
            case Status.Arrows:
                {
                    float percent = (Time.time - startTime);
                    arrow1.transform.localPosition = Vector3.Lerp(arrow1StartPos, arrow1End.localPosition, percent);
                    arrow1.transform.localScale = Vector3.Lerp(arrow1StartScale, arrow1End.localScale, percent);
                    arrow2.transform.localPosition = Vector3.Lerp(arrow2StartPos, arrow2End.localPosition, percent);
                    arrow2.transform.localScale = Vector3.Lerp(arrow2StartScale, arrow2End.localScale, percent);

                    if (percent >= 1.0f)
                    {
                        if (tImmunity >= 10)
                        {
                            protein.SetActive(true);
                            stat = Status.Protein;
                        }
                        else
                        {
                            virus.SetActive(true);
                            stat = Status.Moving;
                            cell.GetComponent<SpriteRenderer>().color = Color.green;
                        }
                        startTime = Time.time;
                    }
                }
                break;
            case Status.Protein:
                {
                    float percent = (Time.time - startTime);
                    protein.transform.position = Vector3.Lerp(pstartPos, cell.transform.position, percent);
                    protein.transform.localScale = Vector3.Lerp(pstartScale, new Vector3(0, 0, 0), percent);
                    if (percent >= 1.0f)
                    {
                        startTime = Time.time;
                        cell.GetComponent<SpriteRenderer>().color = Color.green;
                        stat = Status.Moving;
                    }
                }
                break;
            case Status.Moving:
                {
                    float percent = (Time.time - startTime);
                    virus.transform.position = Vector3.Lerp(startPos, cell.transform.position, percent);
                    if (percent >= 1.0f)
                    {
                        stat = Status.Attacking;
                    }
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
        immunity++;
        text.text = "Immunity: " + immunity;
    }

    IEnumerator VirusAttack()
    {
        enumerStarted = true;
        if (!protein.activeSelf)
        {
            cell.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0, 0);
            yield return new WaitForSeconds(1.0f);
            cell.GetComponent<SpriteRenderer>().enabled = false;
            virus2.gameObject.SetActive(true);
            arrow1.SetActive(false);
            arrow2.SetActive(false);
        }
        else if (tImmunity >= 20)
        {

        }
        else
        {
            virus.GetComponent<SpriteRenderer>().color = Color.red;
            cell.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(1.0f);
            virus.SetActive(false);
            cell.SetActive(false);
            arrow1.SetActive(false);
            arrow2.SetActive(false);
        }
        stat = Status.Kill;
        enumerStarted = false;
    }

    IEnumerator Reset()
    {
        enumerStarted = true;
        yield return new WaitForSeconds(1.0f);
        virus.transform.position = startPos;
        cell.GetComponent<SpriteRenderer>().enabled = true;
        cell.GetComponent<SpriteRenderer>().color = Color.white;
        virus.SetActive(true);
        virus2.SetActive(false);
        tImmunity = immunity;
        if (tImmunity >= 10)
        {
            //Reset Arrows
            arrow1.SetActive(true);
            arrow1.transform.localPosition = arrow1StartPos;
            arrow1.transform.localScale = arrow1StartScale;
            arrow2.SetActive(true);
            arrow2.transform.localPosition = arrow2StartPos;
            arrow2.transform.localScale = arrow2StartScale;

            //Reset Protein
            protein.transform.position = pstartPos;
            protein.transform.localScale = pstartScale;
            protein.SetActive(true);
            virus.GetComponent<SpriteRenderer>().color = Color.white;
            cell.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else if (tImmunity >= 1)
        {
            //Reset Arrows
            arrow1.SetActive(true);
            arrow1.transform.localPosition = arrow1StartPos;
            arrow1.transform.localScale = arrow1StartScale;
            arrow2.SetActive(true);
            arrow2.transform.localPosition = arrow2StartPos;
            arrow2.transform.localScale = arrow2StartScale;
        }
        cell.SetActive(true);
        stat = Status.Wait;
        //arrows.SetActive(true);
        enumerStarted = false;
        startTime = Time.time;
    }
}
