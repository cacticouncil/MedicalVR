using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyTutorialDefense : MonoBehaviour
{
    public GameObject cam;
    public GameObject eventSystem;
    public GameObject fadePrefab;
    public GameObject immunityParticles;
    public GameObject holder;
    public GameObject objects;
    public GameObject fuzeon;
    public TMPro.TextMeshPro defDes;
    public TMPro.TextMeshPro immDes;
    public GameObject[] def = new GameObject[0];
    public GameObject[] imm = new GameObject[0];
    public GameObject[] cells = new GameObject[7];
    public GameObject[] viruses = new GameObject[4];
    public List<TMPro.TextMeshPro> text = new List<TMPro.TextMeshPro>();
    
    private Vector3 prevPos;
    private GameObject fade;
    private int rNum = 0, im = 0;

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
        defDes.text = "Defense: 0";
        immDes.text = "Immunity: 0";
        im = 0;
        Vector3 offset = cells[0].transform.localPosition;
        cells[0].transform.localPosition = Vector3.zero;
        cells[1].transform.localPosition -= offset;
        cells[2].transform.localPosition -= offset;
        cells[3].transform.localPosition -= offset;
        cells[4].transform.localPosition -= offset;
        cells[5].transform.localPosition -= offset;
        cells[6].transform.localPosition -= offset;
        viruses[1].transform.localPosition -= offset;
        viruses[2].transform.localPosition -= offset;
        viruses[3].transform.localPosition -= offset;

        StartCoroutine(FadeOutObject(fade));
        yield return new WaitForSeconds(1.0f);

        //The Defense stat delays viruses from killing your cells. 
        StartCoroutine(StartText());
        StartCoroutine(GrowInObject(cells[0]));
        foreach (GameObject item in def)
        {
            StartCoroutine(FadeInObject(item));
            StartCoroutine(FadeInText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        yield return new WaitForSeconds(1.0f);

        //When a virus spawns, it targets a random cell. 
        StartCoroutine(TurnTextOn(1));
        yield return new WaitForSeconds(1.0f);
        viruses[0].transform.position = cells[0].transform.position + new Vector3(0, 2, 0);
        StartCoroutine(GrowInObject(viruses[0]));
        yield return new WaitForSeconds(1.0f);

        //When a cell is targeted, it turns black.
        StartCoroutine(TurnTextOn(2));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PaintItBlack(cells[0]));
        yield return new WaitForSeconds(1.0f);

        //Once the virus reaches the cell it enters a hosted state. 
        StartCoroutine(TurnTextOn(3));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(MoveVirus());
        yield return new WaitForSeconds(1.0f);

        //During this time the virus attempts to penetrate the cell’s membrane. 
        StartCoroutine(TurnTextOn(4));
        yield return new WaitForSeconds(2);

        //You can’t upgrade the cell once it becomes hosted.
        StartCoroutine(TurnTextOn(5));
        foreach (GameObject item in def)
        {
            StartCoroutine(FadeOutObject(item));
            StartCoroutine(FadeOutText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        yield return new WaitForSeconds(2);

        //But, other cells can spread immunity to it. 
        StartCoroutine(TurnTextOn(6));
        StartCoroutine(GrowInObject(cells[1]));
        StartCoroutine(GrowInObject(cells[2]));
        StartCoroutine(GrowInObject(cells[3]));
        StartCoroutine(GrowInObject(cells[4]));
        StartCoroutine(GrowInObject(cells[5]));
        StartCoroutine(GrowInObject(cells[6]));
        foreach (GameObject item in imm)
        {
            StartCoroutine(FadeInObject(item));
            StartCoroutine(FadeInText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        yield return new WaitForSeconds(1.0f);
        for (int i = 1; i < cells.Length; i++)
        {
            GameObject p = Instantiate(immunityParticles, cells[i].transform.position, Quaternion.LookRotation(cells[0].transform.position - cells[i].transform.position), objects.transform) as GameObject;
            p.GetComponent<ImmunityParticles>().target = cells[0].transform;
            p.GetComponent<ImmunityParticles>().immunity = 1;
            p.GetComponent<ImmunityParticles>().startSpeed = 15;
            p.GetComponent<ImmunityParticles>().enabled = true;
        }
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < 6; i++)
        {
            im += 2;
            immDes.text = "Immunity: " + im;
            yield return new WaitForSeconds(.1f);
        }

        //During this time immunity is received at twice the value.  
        StartCoroutine(TurnTextOn(7));
        yield return new WaitForSeconds(2);

        //Defense is also the only stat that is copied to the child. 
        StartCoroutine(TurnTextOn(8));
        foreach (GameObject item in imm)
        {
            StartCoroutine(FadeOutObject(item));
            StartCoroutine(FadeOutText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        foreach (GameObject item in def)
        {
            StartCoroutine(FadeInObject(item));
            StartCoroutine(FadeInText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        yield return new WaitForSeconds(2);

        //Due to this, it is highly advised that you invest into this stat early on. 
        StartCoroutine(TurnTextOn(9));
        yield return new WaitForSeconds(2);

        //Alternatively, you could put points into a cell when it becomes targeted by a virus.
        StartCoroutine(TurnTextOn(10));
        yield return new WaitForSeconds(2);

        //The Fuzeon powerup permanently increases defense by 5.
        StartCoroutine(TurnTextOn(11));
        StartCoroutine(FadeInObject(fuzeon));
        StartCoroutine(UnPaintItBlack(cells[0]));
        cells[0].GetComponent<Rotate>().enabled = true;
        StartCoroutine(ShrinkOutObject(viruses[0]));
        yield return new WaitForSeconds(1);
        defDes.text = "Defense: 5";
        yield return new WaitForSeconds(1);

        //If you are lucky enough you can get the Defense event.
        StartCoroutine(FadeOutObject(fuzeon));
        StartCoroutine(TurnTextOn(12));
        yield return new WaitForSeconds(2);

        //It raises all your cells defense to the max defense in your colony +5. 
        StartCoroutine(TurnTextOn(13));
        yield return new WaitForSeconds(1);
        defDes.text = "Defense: 10";
        yield return new WaitForSeconds(1);

        //Be careful, different viruses have different attack values. 
        StartCoroutine(TurnTextOn(14));
        foreach (GameObject item in def)
        {
            StartCoroutine(FadeOutObject(item));
            StartCoroutine(FadeOutText(item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        }
        StartCoroutine(FadeInObject(viruses[1]));
        StartCoroutine(FadeInObject(viruses[2]));
        StartCoroutine(FadeInObject(viruses[3]));
        yield return new WaitForSeconds(2);

        //The attack values will also increase as the game continues. 
        StartCoroutine(TurnTextOn(15));
        yield return new WaitForSeconds(2);

        StartCoroutine(EndText());
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
            cell.SetActive(false);
        }
        foreach (GameObject cell in viruses)
        {
            cell.SetActive(false);
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

    IEnumerator GrowInObject(GameObject g)
    {
        g.SetActive(true);
        g.transform.localScale = Vector3.zero;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            g.transform.localScale = new Vector3(t, t, t);
            yield return 0;
        }
    }

    IEnumerator ShrinkOutObject(GameObject g)
    {
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            g.transform.localScale = new Vector3(1.0f - t, 1.0f - t, 1.0f - t);
            yield return 0;
        }
        g.SetActive(false);
    }

    IEnumerator PaintItBlack(GameObject g)
    {
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

    IEnumerator UnPaintItBlack(GameObject g)
    {
        Color c = g.GetComponent<Renderer>().material.color;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            g.GetComponent<Renderer>().material.color = Color.Lerp(c, Color.grey, t);
            yield return 0;
        }
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

    #region Viruses

    IEnumerator MoveVirus()
    {
        Vector3 startPos = viruses[0].transform.position;
        Vector3 endPos = new Vector3(cells[0].transform.position.x, cells[0].transform.position.y + .9f, cells[0].transform.position.z);
        float startTime = Time.time;
        float t = 0;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            viruses[0].transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return 0;
        }
        viruses[0].GetComponent<Rotate>().enabled = false;
        cells[0].GetComponent<Rotate>().enabled = false;
    }
    #endregion
}