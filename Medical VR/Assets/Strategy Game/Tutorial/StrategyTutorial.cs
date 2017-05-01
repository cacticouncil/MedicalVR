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
    public List<TMPro.TextMeshPro> texts = new List<TMPro.TextMeshPro>();

    private int cNum = 1;
    private bool last = false, text = false, finish = false;
    private Vector3 cellPosition = new Vector3(1, 0, 0);
    private List<Vector3> nextPos = new List<Vector3>();
    private List<Coroutine> stop = new List<Coroutine>();

    void Start()
    {
        if (GlobalVariables.tutorial)
        {
            stop.Add(StartCoroutine(SpawnObject(cells[0], objects.transform.position)));
            stop.Add(StartCoroutine(CallOnTimer()));
            StartCoroutine(TurnTextOn(1));
        }
        else
        {
            cellManager.SetActive(true);
            strategyBox.SetActive(true);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        bool held = Input.GetButton("Fire1");
        if (held && !last)
        {
            if (text)
            {
                finish = true;
            }
            else
            {
                Click();
            }
        }
        last = held;
    }

    public void Click()
    {
        switch (cNum)
        {
            //Cells
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                cells[cNum - 1].transform.position = nextPos[0];
                nextPos.Clear();
                cells[cNum - 1].transform.localScale = Vector3.one;

                stop.Add(StartCoroutine(SpawnObject(cells[cNum], objects.transform.position)));
                if (cNum < 6)
                    stop.Add(StartCoroutine(CallOnTimer()));
                break;

            //Virus
            case 7:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                cells[cNum - 1].transform.position = nextPos[0];
                nextPos.Clear();
                cells[cNum - 1].transform.localScale = Vector3.one;

                StartCoroutine(TurnTextOn(1));
                stop.Add(StartCoroutine(SpawnObject(viruses[0], objects.transform.position + new Vector3(0, 10, 0))));
                stop.Add(StartCoroutine(CallOnTimer()));
                break;
            case 8:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                viruses[0].transform.position = nextPos[0];
                nextPos.Clear();
                viruses[0].transform.localScale = Vector3.one;

                stop.Add(StartCoroutine(VirusAttack()));
                stop.Add(StartCoroutine(CallOnTimer()));
                break;
            case 9:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                viruses[0].GetComponent<Rotate>().enabled = false;
                cells[0].GetComponent<Rotate>().enabled = false;
                cells[0].GetComponent<Renderer>().material.color = cells[1].GetComponent<Renderer>().material.color;

                stop.Add(StartCoroutine(Dying()));
                stop.Add(StartCoroutine(CallOnTimer()));
                break;
            case 10:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                viruses[0].SetActive(false);
                cells[0].GetComponent<Renderer>().material.color = Color.black;

                stop.Add(StartCoroutine(KillRedCell()));
                break;

            //White Cells
            case 11:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                viruses[1].transform.position = nextPos[0];
                viruses[1].transform.localScale = Vector3.one;
                viruses[2].transform.position = nextPos[1];
                viruses[2].transform.localScale = Vector3.one;
                nextPos.Clear();
                cells[0].SetActive(false);

                StartCoroutine(TurnTextOn(2));
                stop.Add(StartCoroutine(ArriveWhiteCell()));
                stop.Add(StartCoroutine(CallOnTimer()));
                break;
            case 12:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                whiteCell.transform.position = viruses[2].transform.position;
                whiteCell.transform.localScale = Vector3.one;
                viruses[2].SetActive(false);

                stop.Add(StartCoroutine(MoveWhiteCell()));
                stop.Add(StartCoroutine(CallOnTimer()));
                break;
            case 13:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                whiteCell.transform.position = viruses[1].transform.position;
                viruses[1].SetActive(false);

                stop.Add(StartCoroutine(LeaveWhiteCell()));
                break;

            //Your Cell
            case 14:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                whiteCell.SetActive(false);

                StartCoroutine(TurnTextOn(3));
                stop.Add(StartCoroutine(ShrinkCells()));
                stop.Add(StartCoroutine(SpawnObject(cells[7], objects.transform.position)));
                break;
            case 15:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                for (int i = 1; i < 7; i++)
                {
                    cells[i].SetActive(false);
                }
                cells[7].SetActive(true);
                cells[7].transform.position = nextPos[0];
                nextPos.Clear();
                cells[7].transform.localScale = Vector3.one;

                StartCoroutine(TurnTextOn(4));
                break;
            case 16:
                StartCoroutine(TurnTextOn(5));
                break;
            case 17:
                StartCoroutine(TurnTextOn(6));
                stop.Add(StartCoroutine(FadeInObject(plane)));
                break;
            case 18:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                plane.GetComponent<Renderer>().material.color = Color.red;

                StartCoroutine(TurnTextOn(7));
                break;
            case 19:
                StartCoroutine(TurnTextOn(8));
                break;
            case 20:
                StartCoroutine(TurnTextOn(9));
                stop.Add(StartCoroutine(FadeOutObject(plane)));
                stop.Add(StartCoroutine(FadeInObject(planeDes)));
                break;

            //Strategy Box
            case 21:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                plane.GetComponent<Renderer>().material.color = Color.clear;
                planeDes.GetComponent<Renderer>().material.color = Color.red;

                StartCoroutine(TurnTextOn(10));
                cells[7].transform.GetChild(0).gameObject.SetActive(false);
                stop.Add(StartCoroutine(SpawnObject(strategyBoxMesh, strategyBox.transform.position)));
                stop.Add(StartCoroutine(MoveObject(cells[7], cellPosition)));
                break;
            case 22:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                strategyBoxMesh.transform.position = nextPos[0];
                nextPos.Clear();
                strategyBoxMesh.transform.localScale = Vector3.one;
                cells[7].transform.position = cellPosition;

                StartCoroutine(TurnTextOn(11));
                break;
            case 23:
                StartCoroutine(TurnTextOn(12));
                break;
            case 24:
                StartCoroutine(TurnTextOn(13));
                break;
            case 25:
                StartCoroutine(TurnTextOn(14));
                stop.Add(StartCoroutine(MoveObject(strategyBoxMesh, strategyBox.transform.position)));
                strategyBoxMesh.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 26:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                strategyBoxMesh.transform.position = strategyBox.transform.position;

                StartCoroutine(TurnTextOn(15));
                break;

            //Clean Up 
            case 27:
                subtitles.gameObject.SetActive(false);
                StopAllCoroutines();
                cellManager.SetActive(true);
                strategyBox.SetActive(true);
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

    IEnumerator CallOnTimer()
    {
        yield return new WaitForSeconds(1);
        Click();
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
        g.SetActive(false);
    }

    IEnumerator SpawnObject(GameObject g, Vector3 startPos)
    {
        g.SetActive(true);
        float startTime = Time.time;
        float t = 0;
        nextPos.Add(g.transform.position);
        int index = nextPos.Count - 1;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            g.transform.position = Vector3.Lerp(startPos, nextPos[index], t);
            g.transform.localScale = new Vector3(t, t, t);
            yield return 0;
        }
        g.transform.position = nextPos[index];
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
    IEnumerator TurnTextOn(int index)
    {
        //subtitles.text += "_";

        //while (subtitles.text.Length > 1)
        //{
        //    subtitles.text = subtitles.text.Remove(subtitles.text.Length - 2, 1);
        //    yield return new WaitForSeconds(GlobalVariables.textDelay * .5f);
        //}
        //yield return new WaitForSeconds(GlobalVariables.textDelay);

        while (text)
            yield return 0;

        text = true;
        subtitles.text = "_";

        while (subtitles.text != texts[index].text && !finish)
        {
            yield return new WaitForSeconds(GlobalVariables.textDelay);

            if (subtitles.text.Length == texts[index].text.Length)
            {
                subtitles.text = texts[index].text;
            }
            else
            {
                subtitles.text = subtitles.text.Insert(subtitles.text.Length - 1, texts[index].text[subtitles.text.Length - 1].ToString());
            }
        }
        subtitles.text = texts[index].text;
        finish = false;
        text = false;
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

    IEnumerator SpawnCells()
    {
        for (int i = 0; i < 7; i++)
        {
            StartCoroutine(SpawnObject(cells[i], objects.transform.position));
            yield return new WaitForSeconds(1.0f);
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
        Color sc = cells[0].GetComponent<Renderer>().material.color;
        Color b = Color.black;
        cells[0].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        viruses[1].SetActive(true);
        viruses[2].SetActive(true);
        nextPos.Add(viruses[1].transform.position);
        nextPos.Add(viruses[2].transform.position);

        float t = 0;
        float startTime = Time.time;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            sc.a = 1.0f - t;
            b.a = 1.0f - t;
            cells[0].GetComponent<Renderer>().material.color = sc;
            cells[0].GetComponent<Renderer>().material.SetColor("_OutlineColor", b);
            viruses[1].transform.position = Vector3.Lerp(cells[0].transform.position, nextPos[0], t);
            viruses[1].transform.localScale = new Vector3(t, t, t);
            viruses[2].transform.position = Vector3.Lerp(cells[0].transform.position, nextPos[1], t);
            viruses[2].transform.localScale = new Vector3(t, t, t);
            yield return 0;
        }
        viruses[1].transform.position = nextPos[0];
        viruses[1].transform.localScale = Vector3.one;
        viruses[2].transform.position = nextPos[1];
        viruses[2].transform.localScale = Vector3.one;
        cells[0].SetActive(false);
    }
    #endregion

    #region WhiteCell
    IEnumerator ArriveWhiteCell()
    {
        Vector3 startPos = new Vector3(5, 15, 0);
        whiteCell.SetActive(true);
        whiteCell.transform.localScale = Vector3.zero;

        float t = 0;
        float startTime = Time.time;
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
