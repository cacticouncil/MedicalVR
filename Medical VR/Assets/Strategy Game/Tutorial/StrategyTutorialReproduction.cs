using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyTutorialReproduction : MonoBehaviour
{
    public Transform cam;
    public GameObject fade;
    public LookCamera lc;
    public GameObject reticle;
    public GameObject eventSystem;
    public GameObject objects;
    public Renderer cdk, tgf;
    public TMPro.TextMeshPro repDes;
    public TMPro.TextMeshPro subtitles;
    public GameObject @base;
    public GameObject details;
    public GameObject[] cells;
    public GameObject[] viruses;
    public AudioClip[] voices = new AudioClip[9];

    private string[] texts =
        {
        "The Reproduction stat is vital to growing your colony. While it can be upgraded infinitely, each upgrade benefits less than the previous.",
        "At 0 Reproduction, it takes 50 turns to reproduce. At 5 Reproduction, it takes 7 turns to reproduce. At 10 Reproduction, it takes 5 turns to reproduce.",
        "You can view how many turns are left to reproduce in the cell's details tab.",
        "Proteins in your body change how quickly a cell reproduces.",
        "CDKs (cyclin-dependent kinase) and TGF (transforming growth factor beta 1) both modulate cell reproduction.",
        "The CDK power-up adds 5 extra stat points for 25 turns.",
        "The TGF power-up forces a cell to immediately reproduce.",
        "The child will also have the exact same stats.",
        "Be careful, purple viruses use the cell's reproduction to reproduce more viruses."
        };
    private Vector3 prevPos;
    private Quaternion prevRotation;
    private bool last = false, text = false, finish = false, clickable = false;
    private List<Vector3> nextPos = new List<Vector3>();
    private List<Coroutine> stop = new List<Coroutine>();
    private int index = 0;
    private Color c;

    void OnEnable()
    {
        eventSystem.SetActive(false);
        clickable = false;
        if (cam == null)
            cam = Camera.main.transform.parent;

        prevPos = cam.position;
        prevRotation = lc.transform.rotation;
        //Fade In
        fade.GetComponent<FadeIn>().enabled = true;
        index = 0;
        Invoke("Click", 1);
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
                if (clickable)
                    Click();
            }
        }
        last = held;
    }

    public void Click()
    {
        switch (index)
        {
            case 0:
                //Fade Out
                objects.SetActive(true);
                reticle.SetActive(false);
                cam.position = transform.position;
                lc.target = cells[0].transform;
                lc.enabled = true;
                repDes.text = "Reproduction: 0";
                fade.GetComponent<FadeOut>().enabled = true;
                Invoke("Click", 1);
                break;
            case 1:
                //The Reproduction stat is vital to growing your colony. While it can be upgraded infinitely, each upgrade benefits less than the previous.
                StartCoroutine(TurnTextOn(0));
                StrategySoundHolder.PlayVoice(voices[0]);
                stop.Add(StartCoroutine(SpawnObject(cells[0], cells[0].transform.position)));
                clickable = true;
                break;
            case 2:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                cells[0].transform.position = nextPos[0];
                cells[0].transform.localScale = Vector3.one;
                nextPos.Clear();

                //At 0 Reproduction, it takes 50 turns to reproduce. At 5 Reproduction, it takes 7 turns to reproduce. At 10 Reproduction, it takes 5 turns to reproduce. 
                StartCoroutine(TurnTextOn(1));
                StrategySoundHolder.PlayVoice(voices[1]);
                stop.Add(StartCoroutine(IncreaseReproduction()));
                stop.Add(StartCoroutine(SpawnCells()));
                @base.SetActive(true);
                foreach (Renderer item in @base.GetComponentsInChildren<Renderer>(true))
                {
                    if (item.material.HasProperty("_Color"))
                        stop.Add(StartCoroutine(FadeInObject(item)));
                }
                foreach (TMPro.TextMeshPro item in @base.GetComponentsInChildren<TMPro.TextMeshPro>(true))
                {
                    stop.Add(StartCoroutine(FadeInText(item)));
                }
                break;
            case 3:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                foreach (Renderer item in @base.GetComponentsInChildren<Renderer>(true))
                {
                    if (item.material.HasProperty("_Color"))
                    {
                        c = item.material.color;
                        c.a = 1.0f;
                        item.material.color = c;
                    }
                }
                foreach (TMPro.TextMeshPro item in @base.GetComponentsInChildren<TMPro.TextMeshPro>(true))
                {
                    item.color = Color.black;
                }
                if (cells[1].activeSelf)
                {
                    cells[1].transform.position = nextPos[0];
                    cells[1].transform.localScale = Vector3.one;
                }
                else
                    cells[1].SetActive(true);
                if (cells[2].activeSelf)
                {
                    cells[2].transform.position = nextPos[1];
                    cells[2].transform.localScale = Vector3.one;
                }
                else
                    cells[2].SetActive(true);
                if (cells[3].activeSelf)
                {
                    cells[3].transform.position = nextPos[2];
                    cells[3].transform.localScale = Vector3.one;
                }
                else
                    cells[3].SetActive(true);
                nextPos.Clear();
                repDes.text = "Reproduction: 10";

                //You can view how many turns are left to reproduce in the cell's details tab.
                StartCoroutine(TurnTextOn(2));
                StrategySoundHolder.PlayVoice(voices[2]);
                foreach (Renderer item in @base.GetComponentsInChildren<Renderer>(true))
                {
                    if (item.material.HasProperty("_Color"))
                        stop.Add(StartCoroutine(FadeOutObject(item)));
                }
                foreach (TMPro.TextMeshPro item in @base.GetComponentsInChildren<TMPro.TextMeshPro>(true))
                {
                    stop.Add(StartCoroutine(FadeOutText(item)));
                }
                foreach (Transform item in details.GetComponentsInChildren<Transform>(true))
                {
                    item.gameObject.SetActive(true);
                }
                foreach (Renderer item in details.GetComponentsInChildren<Renderer>(true))
                {
                    if (item.material.HasProperty("_Color"))
                        stop.Add(StartCoroutine(FadeInObject(item)));
                }
                foreach (TMPro.TextMeshPro item in details.GetComponentsInChildren<TMPro.TextMeshPro>(true))
                {
                    stop.Add(StartCoroutine(FadeInText(item)));
                }
                break;
            case 4:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                foreach (Transform item in @base.GetComponentsInChildren<Transform>(true))
                {
                    item.gameObject.SetActive(false);
                }
                foreach (Renderer item in details.GetComponentsInChildren<Renderer>(true))
                {
                    if (item.material.HasProperty("_Color"))
                    {
                        c = item.material.color;
                        c.a = 1.0f;
                        item.material.color = c;
                    }
                }
                foreach (TMPro.TextMeshPro item in details.GetComponentsInChildren<TMPro.TextMeshPro>(true))
                {
                    item.color = Color.black;
                }

                //Proteins in your body change how quickly a cell reproduces.
                StartCoroutine(TurnTextOn(3));
                StrategySoundHolder.PlayVoice(voices[3]);
                foreach (Renderer item in details.GetComponentsInChildren<Renderer>(true))
                {
                    if (item.material.HasProperty("_Color"))
                        stop.Add(StartCoroutine(FadeOutObject(item)));
                }
                foreach (TMPro.TextMeshPro item in details.GetComponentsInChildren<TMPro.TextMeshPro>(true))
                {
                    stop.Add(StartCoroutine(FadeOutText(item)));
                }
                foreach (Transform item in @base.GetComponentsInChildren<Transform>(true))
                {
                    item.gameObject.SetActive(true);
                }
                foreach (Renderer item in @base.GetComponentsInChildren<Renderer>(true))
                {
                    if (item.material.HasProperty("_Color"))
                        stop.Add(StartCoroutine(FadeInObject(item)));
                }
                foreach (TMPro.TextMeshPro item in @base.GetComponentsInChildren<TMPro.TextMeshPro>(true))
                {
                    stop.Add(StartCoroutine(FadeInText(item)));
                }
                break;
            case 5:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                foreach (Transform item in details.GetComponentsInChildren<Transform>(true))
                {
                    item.gameObject.SetActive(false);
                }
                foreach (Renderer item in @base.GetComponentsInChildren<Renderer>(true))
                {
                    if (item.material.HasProperty("_Color"))
                    {
                        c = item.material.color;
                        c.a = 1.0f;
                        item.material.color = c;
                    }
                }
                foreach (TMPro.TextMeshPro item in @base.GetComponentsInChildren<TMPro.TextMeshPro>(true))
                {
                    item.color = Color.black;
                }

                //CDKs (cyclin-dependent kinase) and TGF (transforming growth factor beta 1) both modulate cell reproduction.
                StartCoroutine(TurnTextOn(4));
                StrategySoundHolder.PlayVoice(voices[4]);
                stop.Add(StartCoroutine(FadeInObject(cdk)));
                stop.Add(StartCoroutine(FadeInObject(tgf)));
                break;
            case 6:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                cdk.gameObject.SetActive(true);
                c = cdk.material.color;
                c.a = 1.0f;
                cdk.material.color = c;
                cdk.material.SetColor("_OutlineColor", Color.black);
                tgf.gameObject.SetActive(true);
                tgf.material.color = c;
                tgf.material.SetColor("_OutlineColor", Color.black);

                //The CDK power-up adds 5 extra stat points for 25 turns.
                StartCoroutine(TurnTextOn(5));
                StrategySoundHolder.PlayVoice(voices[5]);
                repDes.text = "Reproduction: 15";
                repDes.color = Color.blue;
                stop.Add(StartCoroutine(FadeOutObject(tgf)));
                break;
            case 7:
                //The TGF power-up forces a cell to immediately reproduce.
                StartCoroutine(TurnTextOn(6));
                StrategySoundHolder.PlayVoice(voices[6]);
                stop.Add(StartCoroutine(FadeOutObject(cdk)));
                stop.Add(StartCoroutine(FadeInObject(tgf)));
                stop.Add(StartCoroutine(SpawnObject(cells[4], cells[0].transform.position)));
                break;
            case 8:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                cells[4].transform.position = nextPos[0];
                cells[4].transform.localScale = Vector3.one;
                nextPos.Clear();
                tgf.gameObject.SetActive(true);
                tgf.material.color = c;
                tgf.material.SetColor("_OutlineColor", Color.black);
                cdk.gameObject.SetActive(false);

                //The child will also have the exact same stats.
                StartCoroutine(TurnTextOn(7));
                StrategySoundHolder.PlayVoice(voices[7]);
                break;
            case 9:
                //Be careful, purple viruses use the cell's reproduction to reproduce more viruses.
                StartCoroutine(TurnTextOn(8));
                StrategySoundHolder.PlayVoice(voices[8]);
                stop.Add(StartCoroutine(FadeOutObject(tgf)));
                stop.Add(StartCoroutine(PaintItBlack(cells[0])));
                stop.Add(StartCoroutine(SpawnViruses()));
                cells[0].GetComponent<Rotate>().enabled = false;
                viruses[0].GetComponent<Rotate>().enabled = false;
                foreach (Renderer item in @base.GetComponentsInChildren<Renderer>(true))
                {
                    if (item.material.HasProperty("_Color"))
                        stop.Add(StartCoroutine(FadeOutObject(item)));
                }
                foreach (TMPro.TextMeshPro item in @base.GetComponentsInChildren<TMPro.TextMeshPro>(true))
                {
                    stop.Add(StartCoroutine(FadeOutText(item)));
                }
                break;
            case 10:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                tgf.gameObject.SetActive(false);
                cells[0].GetComponent<Renderer>().material.color = Color.black;
                if (viruses[0].activeSelf)
                {
                    viruses[0].transform.position = nextPos[0];
                    viruses[0].transform.localScale = Vector3.one;
                }
                else
                    viruses[0].SetActive(true);
                if (viruses[1].activeSelf)
                {
                    viruses[1].transform.position = nextPos[1];
                    viruses[1].transform.localScale = Vector3.one;
                }
                else
                    viruses[1].SetActive(true);
                if (viruses[2].activeSelf)
                {
                    viruses[2].transform.position = nextPos[2];
                    viruses[2].transform.localScale = Vector3.one;
                }
                else
                    viruses[2].SetActive(true);
                if (viruses[3].activeSelf)
                {
                    viruses[3].transform.position = nextPos[3];
                    viruses[3].transform.localScale = Vector3.one;
                }
                else
                    viruses[3].SetActive(true);

                //Fade In
                fade.GetComponent<FadeIn>().enabled = true;
                subtitles.text = "";
                clickable = false;
                Invoke("Click", 1);
                break;
            case 11:
                //Fade Out
                reticle.SetActive(true);
                cam.position = prevPos;
                lc.transform.rotation = prevRotation;
                fade.GetComponent<FadeOut>().enabled = true;
                Invoke("Click", 1);
                break;
            case 12:
                foreach (GameObject cell in cells)
                {
                    cell.SetActive(false);
                }
                foreach (GameObject virus in viruses)
                {
                    virus.SetActive(false);
                }
                cells[0].GetComponent<Rotate>().enabled = true;
                viruses[0].GetComponent<Rotate>().enabled = true;
                cells[0].GetComponent<Renderer>().material.color = Color.grey;
                objects.SetActive(false);
                eventSystem.SetActive(true);
                enabled = false;
                break;
            default:
                break;
        }
        index++;
    }

    #region Objects
    IEnumerator FadeInObject(Renderer g)
    {
        g.gameObject.SetActive(true);
        Color c = g.material.color;
        Color outline = Color.black;
        bool o = false;
        if (g.material.HasProperty("_OutlineColor"))
        {
            o = true;
            outline = g.material.GetColor("_OutlineColor");
        }
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.a = t;
            g.material.color = c;
            if (o)
            {
                outline.a = t;
                g.material.SetColor("_OutlineColor", outline);
            }
            yield return 0;
        }
    }

    IEnumerator FadeOutObject(Renderer g)
    {
        Color c = g.material.color;
        Color outline = Color.black;
        bool o = false;
        if (g.material.HasProperty("_OutlineColor"))
        {
            o = true;
            outline = g.material.GetColor("_OutlineColor");
        }
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.a = 1.0f - t;
            g.material.color = c;
            if (o)
            {
                outline.a = 1.0f - t;
                g.material.SetColor("_OutlineColor", outline);
            }
            yield return 0;
        }
        g.gameObject.SetActive(false);
    }

    IEnumerator SpawnObject(GameObject g, Vector3 startPos)
    {
        g.SetActive(true);
        nextPos.Add(g.transform.position);
        float startTime = Time.time;
        float t = 0;
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

    IEnumerator PaintItBlack(GameObject g)
    {
        g.SetActive(true);
        Color c = g.GetComponent<Renderer>().material.color;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            g.GetComponent<Renderer>().material.color = Color.Lerp(c, Color.black, t);
            yield return 0;
        }
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

    IEnumerator FadeInText(TMPro.TextMeshPro text)
    {
        text.gameObject.SetActive(true);
        Color a = text.color;
        a.a = 0.0f;
        text.color = a;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            a.a = t;
            text.color = a;
            yield return 0;
        }
    }

    IEnumerator FadeOutText(TMPro.TextMeshPro text)
    {
        Color a = text.color;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            a.a = 1.0f - t;
            text.color = a;
            yield return 0;
        }
        text.gameObject.SetActive(false);
    }

    IEnumerator IncreaseReproduction()
    {
        for (int rep = 0; rep < 10; rep++)
        {
            repDes.text = "Reproduction: " + rep;
            yield return new WaitForSeconds(.3f);
        }
        repDes.text = "Reproduction: 10";
    }

    IEnumerator SpawnCells()
    {
        stop.Add(StartCoroutine(SpawnObject(cells[1], cells[0].transform.position)));
        yield return new WaitForSeconds(1.0f);
        stop.Add(StartCoroutine(SpawnObject(cells[2], cells[0].transform.position)));
        yield return new WaitForSeconds(1.0f);
        stop.Add(StartCoroutine(SpawnObject(cells[3], cells[0].transform.position)));
    }

    IEnumerator SpawnViruses()
    {
        stop.Add(StartCoroutine(SpawnObject(viruses[0], cells[0].transform.position + new Vector3(0, 10, 0))));
        yield return new WaitForSeconds(1.0f);
        stop.Add(StartCoroutine(SpawnObject(viruses[1], cells[0].transform.position)));
        yield return new WaitForSeconds(1.0f);
        stop.Add(StartCoroutine(SpawnObject(viruses[2], cells[0].transform.position)));
        yield return new WaitForSeconds(1.0f);
        stop.Add(StartCoroutine(SpawnObject(viruses[3], cells[0].transform.position)));
    }
    #endregion
}