using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyTutorial : MonoBehaviour
{
    public GameObject reticle;
    public TMPro.TextMeshPro subtitles;
    public GameObject cellManager;
    public GameObject strategyBox;
    public GameObject strategyBoxMesh;
    public GameObject objects;
    public GameObject ui;
    public GameObject[] cells = new GameObject[7];
    public GameObject[] viruses = new GameObject[3];
    public AudioClip[] voices = new AudioClip[15];

    private string[] texts =
        {
        "Hello, Human. This will be your final challenge." ,
        "Your goal is to raise a colony of 50 red blood cells against attacking viruses." ,
        "This is one of your cells." ,
        "Each cell has 3 stats that benefit itself and the colony." ,
        "These stats are Reproduction, Defense, and Immunity." ,
        "Each stat can be infinitely increased. Increasing a stat will advance you forward a turn." ,
        "Cells can reproduce and spread immunity on each turn." ,
        "Viruses can also spawn and attack cells on each turn." ,
        "For more insight on each stat, activate its respective tutorial button." ,
        "Random events will also occur. These events can increase cells' stats as well as the viruses'." ,
        "You can learn more about it by clicking on the Events tab." ,
        "This is the Strategy Box. Using it advances turns and gives you a random power-up." ,
        "These power-ups can give large stat boosts or defend your cell." ,
        "Check your inventory at the Strategy Box to learn more." ,
        "That's all I have for you human. I leave this cell in your hands. Good luck and have fun."
        };
    private int cNum = 0;
    private bool last = false, text = false, finish = false;
    private Vector3 cellPosition = new Vector3(1, 0, 0);
    private List<Vector3> nextPos = new List<Vector3>();
    private List<Coroutine> stop = new List<Coroutine>();

    void Start()
    {
        if (GlobalVariables.tutorial)
        {
            Click();
        }
        else
        {
            reticle.SetActive(true);
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
            case 0:
                //Hello, Human. This will be your final challenge.
                StartCoroutine(TurnTextOn(0));
                StrategySoundHolder.PlayVoice(voices[0]);
                stop.Add(StartCoroutine(SpawnObject(cells[0], objects.transform.position)));
                break;
            case 1:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                cells[0].transform.position = nextPos[0];
                nextPos.Clear();
                cells[0].transform.localScale = Vector3.one;

                //Your goal is to raise a colony of 50 red blood cells against attacking viruses.
                StartCoroutine(TurnTextOn(1));
                StrategySoundHolder.PlayVoice(voices[1]);
                stop.Add(StartCoroutine(SpawnCells()));
                break;
            case 2:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                nextPos.Clear();
                //This is one of your cells.
                StartCoroutine(TurnTextOn(2));
                StrategySoundHolder.PlayVoice(voices[2]);
                stop.Add(StartCoroutine(ShrinkCells()));
                break;
            case 3:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                cells[1].SetActive(false);
                cells[2].SetActive(false);
                cells[3].SetActive(false);
                cells[4].SetActive(false);
                cells[5].SetActive(false);
                cells[6].SetActive(false);
                //Each cell has 3 stats that benefit itself and the colony.
                stop.Add(StartCoroutine(SpawnObject(ui, objects.transform.position)));
                StartCoroutine(TurnTextOn(3));
                StrategySoundHolder.PlayVoice(voices[3]);
                break;
            case 4:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                ui.transform.position = nextPos[0];
                nextPos.Clear();
                ui.transform.localScale = Vector3.one;
                //These stats are Reproduction, Defense, and Immunity.
                StartCoroutine(TurnTextOn(4));
                StrategySoundHolder.PlayVoice(voices[4]);
                break;
            case 5:
                //Each stat can be infinitely increased. Increasing a stat will advance you forward a turn.
                StartCoroutine(TurnTextOn(5));
                StrategySoundHolder.PlayVoice(voices[5]);
                break;
            case 6:
                //Cells can reproduce and spread immunity on each turn.
                StartCoroutine(TurnTextOn(6));
                StrategySoundHolder.PlayVoice(voices[6]);
                break;
            case 7:
                //Viruses can also spawn and attack cells on each turn.
                StartCoroutine(TurnTextOn(7));
                StrategySoundHolder.PlayVoice(voices[7]);
                break;
            case 8:
                //For more insight on each stat, activate its respective tutorial button.
                StartCoroutine(TurnTextOn(8));
                StrategySoundHolder.PlayVoice(voices[8]);
                break;
            case 9:
                //Random events will also occur. These events can increase cells' stats as well as the viruses'.
                StartCoroutine(TurnTextOn(9));
                StrategySoundHolder.PlayVoice(voices[9]);
                break;
            case 10:
                //You can learn more about it by clicking on the Events tab.
                StartCoroutine(TurnTextOn(10));
                StrategySoundHolder.PlayVoice(voices[10]);
                break;
            //Strategy Box
            case 11:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();

                //This is the Strategy Box. Using it advances turns and gives you a random power-up.
                StartCoroutine(TurnTextOn(11));
                StrategySoundHolder.PlayVoice(voices[11]);
                ui.gameObject.SetActive(false);
                stop.Add(StartCoroutine(SpawnObject(strategyBoxMesh, strategyBox.transform.position)));
                stop.Add(StartCoroutine(MoveObject(cells[0], cellPosition)));
                break;
            case 12:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                strategyBoxMesh.transform.position = nextPos[0];
                nextPos.Clear();
                strategyBoxMesh.transform.localScale = Vector3.one;
                cells[0].transform.position = cellPosition;

                //These power-ups can give large stat boosts or defend your cell.
                StartCoroutine(TurnTextOn(12));
                StrategySoundHolder.PlayVoice(voices[12]);
                break;
            case 13:
                //Check your inventory at the Strategy Box to learn more.
                StartCoroutine(TurnTextOn(13));
                StrategySoundHolder.PlayVoice(voices[13]);
                break;
            case 14:
                //That's all I have for you human. I leave this cell in your hands. Good luck and have fun."
                StartCoroutine(TurnTextOn(14));
                StrategySoundHolder.PlayVoice(voices[14]);
                stop.Add(StartCoroutine(MoveObject(strategyBoxMesh, strategyBox.transform.position)));
                strategyBoxMesh.transform.GetChild(0).gameObject.SetActive(false);
                break;
            //Clean Up 
            case 15:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                strategyBoxMesh.transform.position = strategyBox.transform.position;
                subtitles.text = "";
                StopAllCoroutines();
                reticle.SetActive(true);
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
        while (text)
            yield return 0;

        text = true;
        subtitles.text = "_";

        while (subtitles.text != texts[index] && !finish)
        {
            yield return new WaitForSeconds(GlobalVariables.textDelay);

            if (subtitles.text.Length == texts[index].Length)
            {
                subtitles.text = texts[index];
            }
            else
            {
                subtitles.text = subtitles.text.Insert(subtitles.text.Length - 1, texts[index][subtitles.text.Length - 1].ToString());
            }
        }
        subtitles.text = texts[index];
        finish = false;
        text = false;
    }
    #endregion

    #region RedCells
    IEnumerator ShrinkCells()
    {
        float startTime = Time.time;
        float t = Time.time - startTime;
        float[] s =
        {
            cells[1].transform.localScale.x,
            cells[2].transform.localScale.x,
            cells[3].transform.localScale.x,
            cells[4].transform.localScale.x,
            cells[5].transform.localScale.x,
            cells[6].transform.localScale.x
        };
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            for (int i = 1; i < 6; i++)
            {
                float f = s[i] * (1.0f - t / 1.0f);
                Mathf.Max(f, 0.0f);
                cells[i].transform.localScale = new Vector3(f, f, f);
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
        for (int i = 1; i < 6 && !finish; i++)
        {
            stop.Add(StartCoroutine(SpawnObject(cells[i], objects.transform.position)));
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
}
