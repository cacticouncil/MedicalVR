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
            case 15:
                StartCoroutine(TurnTextOn(4));
                plane.SetActive(true);
                break;
            case 16:
                StartCoroutine(TurnTextOn(5));
                plane.SetActive(false);
                break;
            case 17:
                StartCoroutine(TurnTextOn(6));
                planeDes.SetActive(true);
                break;

            //Mystery Box
            case 18:
                StartCoroutine(TurnTextOn(7));
                StartCoroutine(SpawnBox());
                break;
            case 19:
                StartCoroutine(TurnTextOn(8));
                break;
            case 20:
                StartCoroutine(TurnTextOn(9));
                StartCoroutine(LeaveBox());
                break;

            //Clean Up 
            case 21:
                cellManager.SetActive(true);
                mysteryBox.SetActive(true);
                StopCoroutine("ClickForMe");
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

    #region Text
    IEnumerator StartText()
    {
        Color a = text[0].color;
        a.a = 0.0f;
        text[0].color = a;
        float startTime = Time.time;
        float percent = 0.0f;
        while (percent < 1.0f)
        {
            percent = (Time.time - startTime);
            a.a = Mathf.Lerp(0.0f, 1.0f, percent);
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

    IEnumerator ShrinkCells()
    {
        float startTime = Time.time;
        float percent = Time.time - startTime;
        while (percent < 1.0f)
        {
            percent = Time.time - startTime;
            for (int i = 1; i < 7; i++)
            {
                cells[i].transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, percent);
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
        float percent = 0;
        Vector3 startPos = new Vector3(-5, 15, 0);
        viruses[0] = Instantiate(virus, startPos, virus.transform.rotation, transform) as GameObject;
        viruses[0].transform.localScale = Vector3.zero;
        while (percent < 1.0f)
        {
            percent = Time.time - startTime;
            viruses[0].transform.position = Vector3.Lerp(startPos, cells[0].transform.position, percent);
            viruses[0].transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent * 2.0f);
            yield return 0;
        }
        viruses[0].transform.position = cells[0].transform.position;
    }


    IEnumerator VirusAttack()
    {
        Debug.Log("Attacking");
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
        float percent = 0;
        Color s = cells[0].GetComponent<Renderer>().material.color;

        while (percent < 1.0f)
        {
            percent = Time.time - startTime;
            viruses[0].transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, percent);
            cells[0].GetComponent<Renderer>().material.color = Color.Lerp(s, Color.black, percent);
            yield return 0;
        }
    }

    IEnumerator Split()
    {
        float startTime = Time.time;
        float percent = 0;
        Color a = cells[0].GetComponent<Renderer>().material.color;
        Color s = a;
        a.a = 0.0f;
        cells[0].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        viruses[1] = Instantiate(virus, viruses[0].transform.position, virus.transform.rotation, transform) as GameObject;
        viruses[1].transform.localScale = Vector3.zero;
        viruses[2] = Instantiate(virus, viruses[0].transform.position, virus.transform.rotation, transform) as GameObject;
        viruses[2].transform.localScale = Vector3.zero;

        while (percent < 1.0f)
        {
            percent = Time.time - startTime;
            cells[0].GetComponent<Renderer>().material.color = Color.Lerp(s, a, percent);
            cells[0].GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.Lerp(s, a, percent));
            viruses[1].transform.position = Vector3.Lerp(cells[0].transform.position, cells[0].transform.position - offset, percent);
            viruses[1].transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent);
            viruses[2].transform.position = Vector3.Lerp(cells[0].transform.position, cells[0].transform.position + offset, percent);
            viruses[2].transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent);
            yield return 0;
        }
        cells[0].SetActive(false);
    }
    #endregion

    #region WhiteCell
    IEnumerator ArriveWhiteCell()
    {
        float startTime = Time.time;
        float percent = 0;
        Vector3 startPos = new Vector3(5, 15, 0);
        whiteCell = Instantiate(whiteCell, startPos, virus.transform.rotation, transform) as GameObject;
        whiteCell.transform.localScale = Vector3.zero;
        while (percent < 1.0f)
        {
            percent = Time.time - startTime;
            whiteCell.transform.position = Vector3.Lerp(startPos, viruses[2].transform.position, percent);
            whiteCell.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent * 2.0f);
            yield return 0;
        }
        whiteCell.transform.position = viruses[2].transform.position;
        whiteCell.transform.localScale = Vector3.one;
        viruses[2].SetActive(false);
    }
    IEnumerator MoveWhiteCell()
    {
        float startTime = Time.time;
        float percent = 0;
        while (percent < 1.0f)
        {
            percent = Time.time - startTime;
            whiteCell.transform.position = Vector3.Lerp(viruses[2].transform.position, viruses[1].transform.position, percent);
            yield return 0;
        }
        whiteCell.transform.position = viruses[1].transform.position;
        viruses[1].SetActive(false);
    }

    IEnumerator LeaveWhiteCell()
    {
        float startTime = Time.time;
        float percent = 0;
        Vector3 endPos = new Vector3(-5, 15, 0);
        while (percent < 1.0f)
        {
            percent = Time.time - startTime;
            whiteCell.transform.position = Vector3.Lerp(viruses[1].transform.position, endPos, percent);
            whiteCell.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, (percent - .5f) * 2.0f);
            yield return 0;
        }
        whiteCell.SetActive(false);
    }
    #endregion

    #region YourCell
    IEnumerator SpawnCell()
    {
        float startTime = Time.time;
        float percent = Time.time - startTime;
        while (percent < 1.0f)
        {
            percent = Time.time - startTime;
            yourCell.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent);
            yield return 0;
        }
        yourCell.transform.localScale = Vector3.one;
    }
    #endregion

    #region MysteryBox
    IEnumerator SpawnBox()
    {
        float startTime = Time.time;
        float percent = Time.time - startTime;
        Vector3 pos = yourCell.transform.position;
        Vector3 des = new Vector3(1, 0, 0);
        while (percent < 1.0f)
        {
            percent = Time.time - startTime;
            yourCell.transform.position = Vector3.Lerp(pos, des, percent);
            mysteryBoxMesh.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent);
            yield return 0;
        }
        yourCell.transform.position = des;
        mysteryBoxMesh.transform.localScale = Vector3.one;
    }

    IEnumerator LeaveBox()
    {
        float startTime = Time.time;
        float percent = Time.time - startTime;
        Vector3 pos = mysteryBoxMesh.transform.position;
        Vector3 des = new Vector3(1, 0, 0);
        while (percent < 1.0f)
        {
            percent = Time.time - startTime;
            mysteryBoxMesh.transform.position = Vector3.Lerp(pos, des, percent);
            yield return 0;
        }
        mysteryBoxMesh.transform.position = des;
    }
    #endregion
}
