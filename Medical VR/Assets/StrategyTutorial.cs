using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyTutorial : MonoBehaviour
{
    public GameObject plane;
    public GameObject planeDes;
    public GameObject cellManager;
    public GameObject strategyBox;
    public GameObject strategyBoxMesh;
    public TMPro.TextMeshPro subtitles;
    public GameObject objects;
    public GameObject whiteCell;
    public GameObject[] cells = new GameObject[8];
    public GameObject[] viruses = new GameObject[3];
    public List<TMPro.TextMeshPro> text = new List<TMPro.TextMeshPro>();

    private int cNum = -1;
    private Vector3 cellPosition = new Vector3(1, 0, 0);

    void Start()
    {
        if (GlobalVariables.tutorial)
        {
            StartCoroutine(ClickForMe());
        }
        else
        {
            cellManager.SetActive(true);
            strategyBox.SetActive(true);
            StopCoroutine(ClickForMe());
            Destroy(gameObject);
        }
    }

    public void Click()
    {
        switch (cNum)
        {
            case -1:
                StartCoroutine(StartText());
                break;
            //Cells
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                StartCoroutine(SpawnObject(cells[cNum], objects.transform.position));
                break;

            //Virus
            case 7:
                StartCoroutine(TurnTextOn(1));
                StartCoroutine(SpawnObject(viruses[0], objects.transform.position + new Vector3(0, 10, 0)));
                break;
            case 8:
                StartCoroutine(VirusAttack());
                break;
            case 9:
                StartCoroutine(Dying());
                break;
            case 10:
                StartCoroutine(KillRedCell());
                StartCoroutine(SpawnObject(viruses[1], objects.transform.position));
                StartCoroutine(SpawnObject(viruses[2], objects.transform.position));
                break;

            //White Cells
            case 11:
                StartCoroutine(TurnTextOn(2));
                StartCoroutine(ArriveWhiteCell());
                break;
            case 12:
                StartCoroutine(MoveWhiteCell());
                break;
            case 13:
                StartCoroutine(LeaveWhiteCell());
                break;

            //Your Cell
            case 14:
                StartCoroutine(ShrinkCells());
                StartCoroutine(TurnTextOn(3));
                StartCoroutine(SpawnObject(cells[7], objects.transform.position));
                break;
            case 16:
                StartCoroutine(TurnTextOn(4));
                break;
            case 18:
                StartCoroutine(TurnTextOn(5));
                break;
            case 20:
                StartCoroutine(TurnTextOn(6));
                StartCoroutine(FadeInObject(plane));
                break;
            case 22:
                StartCoroutine(TurnTextOn(7));
                break;
            case 24:
                StartCoroutine(TurnTextOn(8));
                break;
            case 26:
                StartCoroutine(TurnTextOn(9));
                StartCoroutine(FadeOutObject(plane));
                StartCoroutine(FadeInObject(planeDes));
                break;

            //Strategy Box
            case 28:
                StartCoroutine(TurnTextOn(10));
                StartCoroutine(SpawnObject(strategyBoxMesh, strategyBox.transform.position));
                StartCoroutine(MoveObject(cells[7], cellPosition));
                break;
            case 30:
                StartCoroutine(TurnTextOn(11));
                break;
            case 32:
                StartCoroutine(TurnTextOn(12));
                break;
            case 34:
                StartCoroutine(TurnTextOn(13));
                break;
            case 36:
                StartCoroutine(TurnTextOn(14));
                StartCoroutine(MoveObject(strategyBoxMesh, strategyBox.transform.position));
                break;
            case 38:
                StartCoroutine(TurnTextOn(15));
                break;

            //Clean Up 
            case 40:
                StartCoroutine(EndText());
                break;
            case 41:
                cellManager.SetActive(true);
                strategyBox.SetActive(true);
                StopCoroutine(ClickForMe());
                Destroy(gameObject);
                break;

            default:
                break;
        }
        cNum++;
    }

    IEnumerator ClickForMe()
    {
        while (true)
        {
            Click();
            yield return new WaitForSeconds(1.0f);
        }
    }

    #region Objects
    IEnumerator FadeInObject(GameObject g)
    {
        g.SetActive(true);
        Color c = g.GetComponent<Renderer>().material.color;
        Color outline = Color.black;
        bool o = false;
        if (g.GetComponent<Renderer>().material.HasProperty("_OutlineColor"))
        {
            o = true;
            outline = g.GetComponent<Renderer>().material.GetColor("_OutlineColor");
        }
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.a = t;
            g.GetComponent<Renderer>().material.color = c;
            if (o)
            {
                outline.a = t;
                g.GetComponent<Renderer>().material.SetColor("_OutlineColor", outline);
            }
            yield return 0;
        }
    }

    IEnumerator FadeOutObject(GameObject g)
    {
        Color c = g.GetComponent<Renderer>().material.color;
        Color outline = Color.black;
        bool o = false;
        if (g.GetComponent<Renderer>().material.HasProperty("_OutlineColor"))
        {
            o = true;
            outline = g.GetComponent<Renderer>().material.GetColor("_OutlineColor");
        }
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.a = 1.0f - t;
            g.GetComponent<Renderer>().material.color = c;
            if (o)
            {
                outline.a = 1.0f - t;
                g.GetComponent<Renderer>().material.SetColor("_OutlineColor", outline);
            }
            yield return 0;
        }
        Destroy(g);
    }

    IEnumerator SpawnObject(GameObject g, Vector3 startPos)
    {
        g.SetActive(true);
        float startTime = Time.time;
        float t = 0;
        Vector3 des = g.transform.position;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            g.transform.position = Vector3.Lerp(startPos, des, t);
            g.transform.localScale = new Vector3(t, t, t);
            yield return 0;
        }
        g.transform.position = des;
        g.transform.localScale = Vector3.one;
    }

    IEnumerator MoveObject(GameObject g, Vector3 endPos)
    {
        float startTime = Time.time;
        float t = 0;
        Vector3 startPos = g.transform.position;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            g.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return 0;
        }
        g.transform.position = endPos;
    }
    #endregion

    #region Text
    IEnumerator StartText()
    {
        subtitles.gameObject.SetActive(true);
        subtitles.color = Color.white;
        subtitles.text = "_";
        float startTime = Time.time;
        float t = 0;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            subtitles.text = text[0].text;
            yield return new WaitForSeconds(GlobalVariables.textDelay);
        }
    }

    IEnumerator TurnTextOn(int index)
    {
        Color a = subtitles.color;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = (Time.time - startTime) * 2.0f;
            a.a = 1.0f - t;
            subtitles.color = a;
            yield return 0;
        }
        yield return 0;
        subtitles.text = text[index].text;
        a = subtitles.color;
        a.a = 0.0f;
        subtitles.color = a;
        startTime = Time.time;
        t = 0;
        while (t < 1.0f)
        {
            t = (Time.time - startTime) * 2.0f;
            a.a = t;
            subtitles.color = a;
            yield return 0;
        }
    }

    IEnumerator EndText()
    {
        Color a = subtitles.color;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            a.a = 1.0f - t;
            subtitles.color = a;
            yield return 0;
        }
        subtitles.gameObject.SetActive(false);
    }
    #endregion

    #region RedCells

    IEnumerator ShrinkCells()
    {
        float startTime = Time.time;
        float t = Time.time - startTime;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            float s = 1.0f - t;
            Mathf.Max(s, 0.0f);
            for (int i = 1; i < 7; i++)
            {
                cells[i].transform.localScale = new Vector3(s, s, s);
            }
            yield return 0;
        }
        for (int i = 1; i < 7; i++)
        {
            cells[i].transform.localScale = Vector3.zero;
        }
    }
    #endregion

    #region Virus

    IEnumerator VirusAttack()
    {
        viruses[0].GetComponent<Rotate>().enabled = false;
        cells[0].GetComponent<Rotate>().enabled = false;
        Color s = cells[0].GetComponent<Renderer>().material.color;
        Color f = new Color(s.r * .2f, s.g * .2f, s.b * .2f);

        //Flash
        cells[0].GetComponent<Renderer>().material.color = f;
        yield return new WaitForSeconds(.1f);
        cells[0].GetComponent<Renderer>().material.color = s;
        yield return new WaitForSeconds(.4f);

        //Flash
        cells[0].GetComponent<Renderer>().material.color = f;
        yield return new WaitForSeconds(.1f);
        cells[0].GetComponent<Renderer>().material.color = s;
        yield return new WaitForSeconds(.4f);

        cells[0].GetComponent<Renderer>().material.color = s;
    }

    IEnumerator Dying()
    {
        float startTime = Time.time;
        float t = 0;
        Color sc = cells[0].GetComponent<Renderer>().material.color;
        Color sv = viruses[0].GetComponent<Renderer>().material.color;
        Color b = Color.black;
        viruses[0].transform.GetChild(0).GetComponent<ParticleSystem>().Play();

        while (t < 1.0f)
        {
            t = Time.time - startTime;
            sv.a = 1.0f - t;
            viruses[0].GetComponent<Renderer>().material.color = sv;
            b.a = 1.0f - t;
            viruses[0].GetComponent<Renderer>().material.SetColor("_OutlineColor", b);
            cells[0].GetComponent<Renderer>().material.color = Color.Lerp(sc, Color.black, t);
            yield return 0;
        }
        viruses[0].SetActive(false);
    }

    IEnumerator KillRedCell()
    {
        float startTime = Time.time;
        float t = 0;
        Color sc = cells[0].GetComponent<Renderer>().material.color;
        Color b = Color.black;
        cells[0].transform.GetChild(0).GetComponent<ParticleSystem>().Play();

        while (t < 1.0f)
        {
            t = Time.time - startTime;
            sc.a = 1.0f - t;
            b.a = 1.0f - t;
            cells[0].GetComponent<Renderer>().material.color = sc;
            cells[0].GetComponent<Renderer>().material.SetColor("_OutlineColor", b);
            yield return 0;
        }
        cells[0].SetActive(false);
    }
    #endregion

    #region WhiteCell
    IEnumerator ArriveWhiteCell()
    {
        float startTime = Time.time;
        float t = 0;
        Vector3 startPos = new Vector3(5, 15, 0);
        whiteCell.SetActive(true);
        whiteCell.transform.localScale = Vector3.zero;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            whiteCell.transform.position = Vector3.Lerp(startPos, viruses[2].transform.position, t);
            whiteCell.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t * 2.0f);
            yield return 0;
        }
        whiteCell.transform.position = viruses[2].transform.position;
        whiteCell.transform.localScale = Vector3.one;
        viruses[2].SetActive(false);
    }
    IEnumerator MoveWhiteCell()
    {
        float startTime = Time.time;
        float t = 0;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            whiteCell.transform.position = Vector3.Lerp(viruses[2].transform.position, viruses[1].transform.position, t);
            yield return 0;
        }
        whiteCell.transform.position = viruses[1].transform.position;
        viruses[1].SetActive(false);
    }

    IEnumerator LeaveWhiteCell()
    {
        float startTime = Time.time;
        float t = 0;
        Vector3 endPos = new Vector3(-5, 15, 0);
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            whiteCell.transform.position = Vector3.Lerp(viruses[1].transform.position, endPos, t);
            float temp = Mathf.Clamp01(1.0f - (t - .5f) * 2.0f);
            whiteCell.transform.localScale = new Vector3(temp, temp, temp);
            yield return 0;
        }
        whiteCell.SetActive(false);
    }
    #endregion
}
