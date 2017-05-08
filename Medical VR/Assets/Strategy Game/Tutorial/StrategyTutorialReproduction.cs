using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyTutorialReproduction : MonoBehaviour
{
    public GameObject cam;
    public GameObject eventSystem;
    public GameObject fadePrefab;
    public GameObject holder;
    public GameObject objects;
    public GameObject CDK, TGF;
    public TMPro.TextMeshPro repDes;
    public TMPro.TextMeshPro subtitles;
    public GameObject[] bas = new GameObject[0];
    public GameObject[] details = new GameObject[0];
    public GameObject[] cells = new GameObject[4];
    public GameObject[] viruses = new GameObject[4];
    public List<TMPro.TextMeshPro> texts = new List<TMPro.TextMeshPro>();

    private Vector3 prevPos;
    private GameObject fade;
    private bool last = false, text = false, finish = false, advance = false;
    private List<Vector3> nextPos = new List<Vector3>();
    private List<Coroutine> stop = new List<Coroutine>();

    void OnEnable()
    {
        eventSystem.SetActive(false);
        if (cam == null)
            cam = Camera.main.gameObject;

        prevPos = cam.transform.position;
        StartCoroutine(Advance());
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
                advance = true;
            }
        }
        last = held;
    }

    IEnumerator Advance()
    {
        //Fade to black
        fade = Instantiate(fadePrefab, cam.transform.position, cam.transform.rotation, cam.transform) as GameObject;
        yield return 0;
        StartCoroutine(FadeInObject(fade));
        yield return new WaitForSeconds(1.0f);

        //Fade In
        cam.transform.position = transform.position;
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f;
        holder.transform.position = forward + transform.position;
        yield return 0;
        holder.transform.LookAt(cam.transform);
        holder.SetActive(true);
        repDes.text = "Reproduction: 0";
        StartCoroutine(FadeOutObject(fade));
        yield return new WaitForSeconds(1.0f);


        //The Reproduction stat is vital to growing your colony. 
        StartCoroutine(TurnTextOn(0));
        stop.Add(StartCoroutine(SpawnObject(cells[0], objects.transform.position)));

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();
        cells[0].transform.position = nextPos[0];
        nextPos.Clear();

        //At 0 Reproduction, it takes 50 turns to reproduce. 
        StartCoroutine(TurnTextOn(1));
        foreach (GameObject item in bas)
        {
            stop.Add(StartCoroutine(FadeInObject(item)));
            stop.Add(StartCoroutine(FadeInText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>())));
        }
        stop.Add(StartCoroutine(SpawnObject(cells[1], objects.transform.position)));
        
        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();
        foreach (GameObject item in bas)
        {
            Color c = item.GetComponent<Renderer>().material.color;
            c.a = 1.0f;
            item.GetComponent<Renderer>().material.color = c;
            item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().color = Color.black;
        }
        cells[1].transform.position = nextPos[0];
        nextPos.Clear();

        //At 10 Reproduction, it takes 5 turns to reproduce. 
        StartCoroutine(TurnTextOn(2));
        repDes.text = "Reproduction: 10";
        stop.Add(StartCoroutine(SpawnObject(cells[2], objects.transform.position)));

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();
        cells[2].transform.position = nextPos[0];
        nextPos.Clear();

        //The CDK powerup temporarily adds 5 extra stat points.
        StartCoroutine(TurnTextOn(3));
        stop.Add(StartCoroutine(FadeInObject(CDK)));
        repDes.text = "Reproduction: 15";
        repDes.color = Color.blue;

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();
        CDK.GetComponent<Renderer>().material.color = Color.red;

        //These points aren’t depreciated in value. 
        StartCoroutine(TurnTextOn(4));

        while (!advance)
            yield return 0;
        advance = false;

        stop.Add(StartCoroutine(FadeOutObject(CDK)));
        foreach (GameObject item in bas)
        {
            stop.Add(StartCoroutine(FadeOutObject(item)));
            stop.Add(StartCoroutine(FadeOutText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>())));
        }

        //You can view how many turns left to reproduce in the cell’s details tab. 
        StartCoroutine(TurnTextOn(5));
        foreach (GameObject item in details)
        {
            stop.Add(StartCoroutine(FadeInObject(item)));
            stop.Add(StartCoroutine(FadeInText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>())));
        }

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();

        foreach (GameObject item in bas)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in details)
        {
            Color c = item.GetComponent<Renderer>().material.color;
            c.a = 1.0f;
            item.GetComponent<Renderer>().material.color = c;
            item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().color = Color.black;
        }

        //The TGF powerup forces a cell to immediately reproduce.
        StartCoroutine(TurnTextOn(6));
        foreach (GameObject item in details)
        {
            stop.Add(StartCoroutine(FadeOutObject(item)));
            stop.Add(StartCoroutine(FadeOutText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>())));
        }
        foreach (GameObject item in bas)
        {
            stop.Add(StartCoroutine(FadeInObject(item)));
            stop.Add(StartCoroutine(FadeInText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>())));
        }
        stop.Add(StartCoroutine(FadeInObject(TGF)));
        stop.Add(StartCoroutine(SpawnObject(cells[3], objects.transform.position)));
        
        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();

        foreach (GameObject item in details)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in bas)
        {
            Color c = item.GetComponent<Renderer>().material.color;
            c.a = 1.0f;
            item.GetComponent<Renderer>().material.color = c;
            item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().color = Color.black;
        }

        TGF.GetComponent<Renderer>().material.color = Color.red;
        cells[3].transform.position = nextPos[0];
        nextPos.Clear();

        //The child will also have the exact same stats. 
        StartCoroutine(TurnTextOn(7));

        while (!advance)
            yield return 0;
        advance = false;


        //Be careful, purple viruses use the cell’s reproduction to reproduce more viruses. 
        StartCoroutine(TurnTextOn(8));
        stop.Add(StartCoroutine(FadeOutObject(TGF)));
        stop.Add(StartCoroutine(PaintItBlack(cells[0])));
        stop.Add(StartCoroutine(SpawnObject(viruses[0], objects.transform.position + new Vector3(0, 10, 0))));
        cells[0].GetComponent<Rotate>().enabled = false;
        viruses[0].GetComponent<Rotate>().enabled = false;

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();
        Color r = Color.red;
        r.a = 0;
        TGF.GetComponent<Renderer>().material.color = r;
        cells[0].GetComponent<Renderer>().material.color = Color.black;
        viruses[0].transform.position = nextPos[0];
        nextPos.Clear();
        viruses[0].transform.localScale = Vector3.one;
        
        stop.Add(StartCoroutine(SpawnObject(viruses[1], objects.transform.position)));

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();
        viruses[1].transform.position = nextPos[0];
        nextPos.Clear();
        viruses[1].transform.localScale = Vector3.one;
        
        stop.Add(StartCoroutine(SpawnObject(viruses[2], objects.transform.position)));

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();
        viruses[2].transform.position = nextPos[0];
        nextPos.Clear();
        viruses[2].transform.localScale = Vector3.one;

        stop.Add(StartCoroutine(SpawnObject(viruses[3], objects.transform.position)));

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();
        viruses[3].transform.position = nextPos[0];
        nextPos.Clear();
        viruses[3].transform.localScale = Vector3.one;

        foreach (GameObject item in bas)
        {
            StartCoroutine(FadeOutObject(item));
            StartCoroutine(FadeOutText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }

        subtitles.text = "";

        //Fade to black
        StartCoroutine(FadeInObject(fade));
        yield return new WaitForSeconds(1.0f);

        //Fade In
        cam.transform.position = prevPos;
        StartCoroutine(FadeOutObject(fade));
        yield return new WaitForSeconds(1.5f);

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

        holder.SetActive(false);
        eventSystem.SetActive(true);
        Destroy(fade);
        enabled = false;
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
    #endregion
}