using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrategyTutorialD : MonoBehaviour
{
    public float defense = 1;
    public float hp = 1;
    public TMPro.TextMeshPro text;
    public GameObject virus, virus1, virus2, cell;
    public ParticleSystem particles;

    private Vector3 v1end = new Vector3(-1, 0, 0);
    private Vector3 v2end = new Vector3(1, 0, 0);
    private Color sc;

    private enum Status
    {
        Moving,
        Attacking,
        Dying,
        Split,
        Reset
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
        cell.GetComponent<Renderer>().material.color = Color.green;
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
                    {
                        stat = Status.Attacking;
                    }
                }
                break;
            case Status.Attacking:
                {
                    if (!enumerStarted)
                    {
                        StartCoroutine(VirusAttack());
                    }
                }
                break;
            case Status.Dying:
                {
                    float percent = (Time.time - startTime) * .5f;
                    virus.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, percent);
                    cell.GetComponent<Renderer>().material.color = Color.Lerp(sc, Color.black, percent);
                    virus1.transform.position = Vector3.Lerp(virus.transform.position, virus.transform.position + v1end, percent * .5f);
                    virus1.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent * .5f);
                    virus2.transform.position = Vector3.Lerp(virus.transform.position, virus.transform.position + v2end, percent * .5f);
                    virus2.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent * .5f);
                    if (percent >= 1.0f)
                    {
                        particles.Play();
                        virus.gameObject.SetActive(false);
                        stat = Status.Split;
                        //startTime = Time.time;
                    }
                }
                break;
            case Status.Split:
                {
                    float percent = (Time.time - startTime) * .5f;
                    virus1.transform.position = Vector3.Lerp(virus.transform.position, virus.transform.position + v1end, percent * .5f);
                    virus1.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent * .5f);
                    virus2.transform.position = Vector3.Lerp(virus.transform.position, virus.transform.position + v2end, percent * .5f);
                    virus2.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent * .5f);
                    cell.GetComponent<Renderer>().material.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, percent - 1.0f));
                    if (percent >= 2.0f)
                    {
                        cell.GetComponent<Renderer>().enabled = false;
                        stat = Status.Reset;
                    }
                }
                break;
            case Status.Reset:
                {
                    if (!enumerStarted)
                    {
                        StartCoroutine(Reset());
                    }
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
            cell.GetComponent<Renderer>().material.color = new Color(1.0f, 0, 0);
            yield return new WaitForSeconds(.9f);
            hp -= 1;
            cell.GetComponent<Renderer>().material.color = new Color(.8f, .2f, .2f);
            yield return new WaitForSeconds(.1f);
        }
        cell.GetComponent<Renderer>().material.color = new Color(1.0f, 0, 0);
        yield return new WaitForSeconds(.1f);
        sc = cell.GetComponent<Renderer>().material.color;
        virus1.gameObject.SetActive(true);
        virus2.gameObject.SetActive(true);
        startTime = Time.time;
        stat = Status.Dying;
        enumerStarted = false;
    }

    IEnumerator Reset()
    {
        enumerStarted = true;
        Debug.Log("Resetting");
        yield return new WaitForSeconds(1.0f);
        virus.transform.position = startPos;
        cell.GetComponent<Renderer>().enabled = true;
        cell.GetComponent<Renderer>().material.color = Color.white;
        virus.transform.localScale = Vector3.one;
        virus.gameObject.SetActive(true);
        virus1.transform.localScale = Vector3.zero;
        virus1.gameObject.SetActive(false);
        virus2.transform.localScale = Vector3.zero;
        virus2.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Moving");
        hp = Mathf.Sqrt(defense * 5 + 1);
        cell.GetComponent<Renderer>().material.color = Color.green;
        stat = Status.Moving;
        enumerStarted = false;
        startTime = Time.time;
    }
}
