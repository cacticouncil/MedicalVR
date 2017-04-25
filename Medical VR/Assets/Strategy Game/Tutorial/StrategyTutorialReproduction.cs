using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyTutorialReproduction : MonoBehaviour
{
    public GameObject cam;
    public GameObject eventSystem;
    public GameObject fadePrefab;
    public GameObject redCellPrefab;
    public GameObject pupleVirusPrefab;
    public GameObject spawn;
    public GameObject holder;
    public GameObject objects;
    public GameObject[] bas = new GameObject[0];
    public GameObject[] details = new GameObject[0];
    public GameObject CDK, TGF;
    public TMPro.TextMeshPro repDes;
    public List<TMPro.TextMeshPro> text = new List<TMPro.TextMeshPro>();

    private GameObject[] cells = new GameObject[7];
    private Vector3 prevPos;
    private GameObject fade;
    private int rNum = 0;

    void OnEnable()
    {
        eventSystem.SetActive(false);
        if (cam == null)
            cam = Camera.main.gameObject;

        prevPos = cam.transform.position;
        StartCoroutine(Advance());
    }

    IEnumerator Advance()
    {
        spawn = redCellPrefab;
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


        //Start Text
        StartCoroutine(StartText());
        //Give it 1 sec to come in, then 1 sec to read
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(SpawnRedCell());
        yield return new WaitForSeconds(1.0f);

        //At 0 Reproduction, it takes 50 turns to reproduce. 
        StartCoroutine(TurnTextOn(1));
        foreach (GameObject item in bas)
        {
            StartCoroutine(FadeInObject(item));
            StartCoroutine(FadeInText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(SpawnRedCell());
        yield return new WaitForSeconds(1.0f);

        //At 10 Reproduction, it takes 5 turns to reproduce. 
        StartCoroutine(TurnTextOn(2));
        yield return new WaitForSeconds(1.0f);
        repDes.text = "Reproduction: 10";
        StartCoroutine(SpawnRedCell());
        yield return new WaitForSeconds(1.0f);

        //The CDK powerup temporarily adds 5 extra stat points.
        StartCoroutine(TurnTextOn(3));
        StartCoroutine(FadeInObject(CDK));
        yield return new WaitForSeconds(1.0f);
        repDes.text = "Reproduction: 15";
        repDes.color = Color.blue;
        yield return new WaitForSeconds(1.0f);

        //These points aren’t depreciated in value. 
        StartCoroutine(TurnTextOn(4));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeOutObject(CDK));
        foreach (GameObject item in bas)
        {
            StartCoroutine(FadeOutObject(item));
            StartCoroutine(FadeOutText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        yield return new WaitForSeconds(.5f);

        //You can view how many turns left to reproduce in the cell’s details tab. 
        StartCoroutine(TurnTextOn(5));
        foreach (GameObject item in details)
        {
            StartCoroutine(FadeInObject(item));
            StartCoroutine(FadeInText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        yield return new WaitForSeconds(1.5f);
        foreach (GameObject item in details)
        {
            StartCoroutine(FadeOutObject(item));
            StartCoroutine(FadeOutText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        yield return new WaitForSeconds(.5f);

        //The TGF powerup forces a cell to immediately reproduce.
        StartCoroutine(TurnTextOn(6));
        foreach (GameObject item in bas)
        {
            StartCoroutine(FadeInObject(item));
            StartCoroutine(FadeInText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        StartCoroutine(FadeInObject(TGF));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(SpawnRedCell());
        yield return new WaitForSeconds(1.0f);

        //The child will also have the exact same stats. 
        StartCoroutine(TurnTextOn(7));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeOutObject(TGF));
        yield return new WaitForSeconds(.5f);

        //Be careful, purple viruses use the cell’s reproduction to reproduce more viruses. 
        StartCoroutine(TurnTextOn(8));
        spawn = pupleVirusPrefab;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(SpawnRedCell());
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(SpawnRedCell());
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(SpawnRedCell());
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(EndText());
        foreach (GameObject item in bas)
        {
            StartCoroutine(FadeOutObject(item));
            StartCoroutine(FadeOutText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        yield return new WaitForSeconds(1.0f);


        //Fade to black
        StartCoroutine(FadeInObject(fade));
        yield return new WaitForSeconds(1.0f);

        //Fade In
        cam.transform.position = prevPos;
        StartCoroutine(FadeOutObject(fade));
        yield return new WaitForSeconds(1.5f);

        foreach (GameObject cell in cells)
        {
            Destroy(cell);
        }

        rNum = 0;
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
    #endregion

    #region Text
    IEnumerator StartText()
    {
        text[0].gameObject.SetActive(true);
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

    IEnumerator EndText()
    {
        int i = text.Count - 1;
        Color a = text[i].color;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            a.a = 1.0f - t;
            text[i].color = a;
            yield return 0;
        }
        text[i].gameObject.SetActive(false);
    }
    #endregion

    #region RedCells
    IEnumerator SpawnRedCell()
    {
        GameObject c;
        if (rNum == 4)
        {
            c = Instantiate(spawn, new Vector3(objects.transform.position.x, objects.transform.position.y + 10, objects.transform.position.z), redCellPrefab.transform.rotation, objects.transform) as GameObject;
            cells[0].GetComponent<Renderer>().material.color = Color.black;
        }
        else
            c = Instantiate(spawn, objects.transform.position, redCellPrefab.transform.rotation, objects.transform) as GameObject;
        Vector3 desination;
        switch (rNum)
        {
            case 1:
                //Top Right (1, 1)
                desination = new Vector3(objects.transform.position.x + 2, objects.transform.position.y, objects.transform.position.z + 2);
                break;
            case 2:
                //Right (1, 0)
                desination = new Vector3(objects.transform.position.x + 3, objects.transform.position.y, objects.transform.position.z);
                break;
            case 3:
                //Bottom Right (1, -1)
                desination = new Vector3(objects.transform.position.x + 2, objects.transform.position.y, objects.transform.position.z + -2);
                break;
            case 4:
                //UP
                desination = new Vector3(objects.transform.position.x, objects.transform.position.y + .9f, objects.transform.position.z);
                break;
            case 5:
                //Top Right (1, 1)
                desination = new Vector3(objects.transform.position.x + 1, objects.transform.position.y, objects.transform.position.z + 1);
                break;
            case 6:
                //Right (1, 0)
                desination = new Vector3(objects.transform.position.x + 1.5f, objects.transform.position.y, objects.transform.position.z);
                break;
            default:
                desination = objects.transform.position;
                break;
        }
        cells[rNum] = c;

        float startTime = Time.time;
        float t = 0;
        Vector3 startPos = c.transform.position;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.transform.position = Vector3.Lerp(startPos, desination, t);
            t = Mathf.Min(1.0f, t);
            c.transform.localScale = new Vector3(t, t, t);
            yield return 0;
        }
        if (rNum == 4)
        {
            cells[0].GetComponent<Rotate>().enabled = false;
            cells[4].GetComponent<Rotate>().enabled = false;
        }
        c.transform.position = desination;
        c.transform.localScale = Vector3.one;
        rNum++;
    }
    #endregion
}