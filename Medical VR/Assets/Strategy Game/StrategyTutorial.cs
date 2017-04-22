using UnityEngine;
using System.Collections;

public class StrategyTutorial : MonoBehaviour
{
    public GameObject redCell;
    public GameObject virus;
    public GameObject whiteCell;
    public GameObject yourCell;
    public GameObject plane;
    public GameObject planeDes;
    public GameObject mysteryBoxMesh;
    public GameObject cellManager;
    public GameObject mysteryBox;
    public Vector3 cellPosition;
    public Vector3 virusEnd = new Vector3(0, 0, 0);
    public TMPro.TextMeshPro[] text;

    public GameObject[] cells = new GameObject[7];
    public GameObject[] viruses = new GameObject[3];
    private int cNum = -1;
    private Vector3 offset = new Vector3(.5f, 0, 0);
    private int rNum = 0;

    void Start()
    {
        StartCoroutine(ClickForMe());
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
                StartCoroutine(SpawnRedCell());
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                StartCoroutine(SpawnRedCell());
                break;

            //Virus
            case 7:
                StartCoroutine(TurnTextOn(1));
                StartCoroutine(MoveVirus());
                break;
            case 8:
                StartCoroutine(VirusAttack());
                break;
            case 9:
                StartCoroutine(Dying());
                break;
            case 10:
                StartCoroutine(Split());
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
                StartCoroutine(SpawnCell());
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
                StartCoroutine(SpawnBox());
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
                StartCoroutine(LeaveBox());
                break;
            case 38:
                StartCoroutine(TurnTextOn(15));
                break;

            //Clean Up 
            case 40:
                cellManager.SetActive(true);
                mysteryBox.SetActive(true);
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

    IEnumerator FadeInObject(GameObject g)
    {
        g.SetActive(true);
        Color start = g.GetComponent<Renderer>().material.color;
        start.a = 0;
        Color c = start;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.a = t;
            g.GetComponent<Renderer>().material.color = c;
            yield return 0;
        }
    }

    IEnumerator FadeOutObject(GameObject g)
    {
        Color start = g.GetComponent<Renderer>().material.color;
        Color c = start;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.a = 1.0f - t;
            g.GetComponent<Renderer>().material.color = c;
            yield return 0;
        }
        Destroy(g);
    }

    #region Text
    IEnumerator StartText()
    {
        Color a = text[0].color;
        a.a = 0.0f;
        text[0].color = a;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            a.a = t;
            text[0].color = a;
            yield return 0;
        }
        a.a = 1.0f;
        text[0].color = a;
    }

    IEnumerator TurnTextOn(int index)
    {
        Color a = text[index - 1].color;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = (Time.time - startTime) * 2.0f;
            a.a = 1.0f - t;
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
        t = 0;
        while (t < 1.0f)
        {
            t = (Time.time - startTime) * 2.0f;
            a.a = t;
            text[index].color = a;
            yield return 0;
        }
        a.a = 1.0f;
        text[index].color = a;
    }
    #endregion

    #region RedCells
    IEnumerator SpawnRedCell()
    {
        GameObject c = Instantiate(redCell, cellPosition, redCell.transform.rotation, transform) as GameObject;
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
        cells[rNum] = c;
        rNum++;

        float startTime = Time.time;
        float t = 0;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.transform.position = Vector3.Lerp(cellPosition, desination, t);
            t = Mathf.Min(1.0f, t);
            c.transform.localScale = new Vector3(t, t, t);
            yield return 0;
        }
        c.transform.position = desination;
        c.transform.localScale = Vector3.one;
    }

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
    IEnumerator MoveVirus()
    {
        float startTime = Time.time;
        float t = 0;
        Vector3 startPos = new Vector3(-5, 15, 0);
        viruses[0] = Instantiate(virus, startPos, virus.transform.rotation, transform) as GameObject;
        viruses[0].transform.localScale = Vector3.zero;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            viruses[0].transform.position = Vector3.Lerp(startPos, virusEnd, t);
            float temp = Mathf.Clamp01(t * 2.0f);
            viruses[0].transform.localScale = new Vector3(temp, temp, temp);
            yield return 0;
        }
        viruses[0].GetComponent<Rotate>().enabled = false;
        cells[0].GetComponent<Rotate>().enabled = false;
    }


    IEnumerator VirusAttack()
    {
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

    IEnumerator Split()
    {
        float startTime = Time.time;
        float t = 0;
        Color sc = cells[0].GetComponent<Renderer>().material.color;
        Color b = Color.black;
        cells[0].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        viruses[1] = Instantiate(virus, viruses[0].transform.position, virus.transform.rotation, transform) as GameObject;
        viruses[1].transform.localScale = Vector3.zero;
        viruses[2] = Instantiate(virus, viruses[0].transform.position, virus.transform.rotation, transform) as GameObject;
        viruses[2].transform.localScale = Vector3.zero;

        while (t < 1.0f)
        {
            t = Time.time - startTime;
            sc.a = 1.0f - t;
            b.a = 1.0f - t;
            cells[0].GetComponent<Renderer>().material.color = sc;
            cells[0].GetComponent<Renderer>().material.SetColor("_OutlineColor", b);
            viruses[1].transform.position = Vector3.Lerp(cells[0].transform.position, cells[0].transform.position - offset, t);
            viruses[1].transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            viruses[2].transform.position = Vector3.Lerp(cells[0].transform.position, cells[0].transform.position + offset, t);
            viruses[2].transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
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
        whiteCell = Instantiate(whiteCell, startPos, virus.transform.rotation, transform) as GameObject;
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

    #region YourCell
    IEnumerator SpawnCell()
    {
        float startTime = Time.time;
        float t = Time.time - startTime;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            yourCell.transform.localScale = new Vector3(t, t, t);
            yield return 0;
        }
        yourCell.transform.localScale = Vector3.one;
    }
    #endregion

    #region MysteryBox
    IEnumerator SpawnBox()
    {
        float startTime = Time.time;
        float t = Time.time - startTime;
        Vector3 pos = yourCell.transform.position;
        Vector3 des = new Vector3(1, 0, 0);
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            yourCell.transform.position = Vector3.Lerp(pos, des, t);
            mysteryBoxMesh.transform.localScale = new Vector3(t, t, t);
            yield return 0;
        }
        yourCell.transform.position = des;
        mysteryBoxMesh.transform.localScale = Vector3.one;
    }

    IEnumerator LeaveBox()
    {
        float startTime = Time.time;
        float t = Time.time - startTime;
        Vector3 pos = mysteryBoxMesh.transform.position;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            mysteryBoxMesh.transform.position = Vector3.Lerp(pos, mysteryBox.transform.position, t);
            yield return 0;
        }
        mysteryBoxMesh.transform.position = mysteryBox.transform.position;
    }
    #endregion
}
