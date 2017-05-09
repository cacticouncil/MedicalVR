using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyTutorialImmunity : MonoBehaviour
{
    public GameObject cam;
    public GameObject eventSystem;
    public GameObject fadePrefab;
    public GameObject immunityParticles;
    public GameObject holder;
    public GameObject objects;
    public GameObject antigen, interferon, protein;
    public GameObject proOutline;
    public GameObject virusImm;
    public GameObject virus;
    public TMPro.TextMeshPro immDes;
    public TMPro.TextMeshPro proDes;
    public GameObject pro;
    public TMPro.TextMeshPro subtitles;
    public GameObject[] imm = new GameObject[0];
    public GameObject[] cells = new GameObject[7];
    public List<TMPro.TextMeshPro> texts = new List<TMPro.TextMeshPro>();

    private Vector3 prevPos;
    private GameObject fade;
    private bool last = false, text = false, finish = false, advance = false;
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
        immDes.text = "Immunity: 0";

        StartCoroutine(FadeOutObject(fade));
        yield return new WaitForSeconds(1.0f);

        //Immunity spreads from cell to cell and helps you fight back against viruses in multiple ways. 
        StartCoroutine(TurnTextOn(0));
        stop.Add(StartCoroutine(GrowInObject(cells[0])));
        stop.Add(StartCoroutine(GrowInObject(cells[1])));
        stop.Add(StartCoroutine(GrowInObject(cells[2])));
        stop.Add(StartCoroutine(GrowInObject(cells[3])));
        stop.Add(StartCoroutine(GrowInObject(cells[4])));
        stop.Add(StartCoroutine(GrowInObject(cells[5])));
        stop.Add(StartCoroutine(GrowInObject(cells[6])));
        foreach (GameObject item in imm)
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

        for (int i = 0; i < 7; i++)
        {
            cells[i].transform.localScale = Vector3.one;
        }
        foreach (GameObject item in imm)
        {
            Color c = item.GetComponent<Renderer>().material.color;
            c.a = 1;
            item.GetComponent<Renderer>().material.color = c;
            item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().color = Color.black;
        }

        //With at least 1 Immunity, cells will spread a portion of their Immunity to adjacent cells. 
        StartCoroutine(TurnTextOn(1));
        immDes.text = "Immunity: 1";
        for (int i = 1; i < cells.Length; i++)
        {
            GameObject p = Instantiate(immunityParticles, cells[0].transform.position, Quaternion.LookRotation(cells[i].transform.position - cells[0].transform.position), objects.transform) as GameObject;
            p.GetComponent<ImmunityParticles>().target = cells[i].transform;
            p.GetComponent<ImmunityParticles>().immunity = .01f;
            p.GetComponent<ImmunityParticles>().startSpeed = 15;
            p.GetComponent<ImmunityParticles>().enabled = true;
        }

        while (!advance)
            yield return 0;
        advance = false;

        //With only 1 Immunity, adjacent cells will receive 1 Immunity each in 100 turns. 
        StartCoroutine(TurnTextOn(2));
        for (int i = 1; i < cells.Length; i++)
        {
            GameObject p = Instantiate(immunityParticles, cells[0].transform.position, Quaternion.LookRotation(cells[i].transform.position - cells[0].transform.position), objects.transform) as GameObject;
            p.GetComponent<ImmunityParticles>().target = cells[i].transform;
            p.GetComponent<ImmunityParticles>().immunity = .01f;
            p.GetComponent<ImmunityParticles>().startSpeed = 15;
            p.GetComponent<ImmunityParticles>().enabled = true;
        }

        while (!advance)
            yield return 0;
        advance = false;

        //1 cell surrounded by 6 cells with 1 immunity takes 16 turns to receive 1 Immunity. 
        StartCoroutine(TurnTextOn(3));
        yield return new WaitForSeconds(1.0f);
        for (int i = 1; i < cells.Length; i++)
        {
            GameObject p = Instantiate(immunityParticles, cells[i].transform.position, Quaternion.LookRotation(cells[0].transform.position - cells[i].transform.position), objects.transform) as GameObject;
            p.GetComponent<ImmunityParticles>().target = cells[0].transform;
            p.GetComponent<ImmunityParticles>().immunity = .01f;
            p.GetComponent<ImmunityParticles>().startSpeed = 15;
            p.GetComponent<ImmunityParticles>().enabled = true;
        }

        while (!advance)
            yield return 0;
        advance = false;

        //At 10 Immunity, a cell spawns a random protein. 
        StartCoroutine(TurnTextOn(4));
        proDes.text = "Protein: RNase L";
        stop.Add(StartCoroutine(FadeInObject(pro)));
        stop.Add(StartCoroutine(FadeInText(pro.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>())));
        immDes.text = "Immunity: 10";

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();

        Color col = pro.GetComponent<Renderer>().material.color;
        col.a = 1;
        pro.GetComponent<Renderer>().material.color = col;
        pro.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().color = Color.black;

        //Most stop the virus from replicating when it kills the cell.
        StartCoroutine(TurnTextOn(5));

        while (!advance)
            yield return 0;
        advance = false;

        proDes.text = "Protein: PKR";

        while (!advance)
            yield return 0;
        advance = false;

        proDes.text = "Protein: TRIM22";

        while (!advance)
            yield return 0;
        advance = false;

        proDes.text = "Protein: IFIT";

        while (!advance)
            yield return 0;
        advance = false;

        //But, you can get some that attempt to stop the virus before it kills the cell. 
        StartCoroutine(TurnTextOn(6));
        proDes.text = "Protein: CH25H";

        while (!advance)
            yield return 0;
        advance = false;

        proDes.text = "Protein: Mx1";

        while (!advance)
            yield return 0;
        advance = false;

        //You can click on the Protein tab to see the list of proteins and what each does. 
        StartCoroutine(TurnTextOn(7));
        stop.Add(StartCoroutine(FadeInObject(proOutline)));

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();

        proOutline.GetComponent<Renderer>().material.color = Color.red;

        //Each virus has an Immunity value that the cell’s immunity value must be higher than to kill the virus. 
        StartCoroutine(TurnTextOn(8));
        stop.Add(StartCoroutine(FadeOutObject(proOutline)));
        stop.Add(StartCoroutine(FadeInObject(virus)));
        stop.Add(StartCoroutine(FadeInObject(virusImm)));
        immDes.text = "Immunity: 20";
        
        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();

        proOutline.SetActive(false);
        col = virus.GetComponent<Renderer>().material.color;
        col.a = 1;
        virus.GetComponent<Renderer>().material.color = col;
        virus.GetComponent<Renderer>().material.SetColor("_Outline", Color.black);
        col = virusImm.GetComponent<Renderer>().material.color;
        col.a = 1;
        virusImm.GetComponent<Renderer>().material.color = col;

        //When a virus is killed with Immunity that value is subtracted from the cell’s Immunity. 
        StartCoroutine(TurnTextOn(9));

        while (!advance)
            yield return 0;
        advance = false;

        stop.Add(StartCoroutine(FadeOutObject(virus)));
        stop.Add(StartCoroutine(FadeOutObject(virusImm)));
        for (int i = 20; i > -1; i--)
        {
            immDes.text = "Immunity: " + i;
            yield return new WaitForSeconds(.1f);
            if (advance)
                break;
        }


        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();
        
        immDes.text = "Immunity: 0";
        virus.SetActive(false);
        virusImm.SetActive(false);

        //The Immunity required to kill each virus increases as the game continues. 
        StartCoroutine(TurnTextOn(10));

        while (!advance)
            yield return 0;
        advance = false;

        //The Antigen powerup increases Immunity by 10. 
        StartCoroutine(TurnTextOn(11));
        stop.Add(StartCoroutine(FadeInObject(antigen)));
        immDes.text = "Immunity: 10";

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();

        col = antigen.GetComponent<Renderer>().material.color;
        col.a = 1;
        antigen.GetComponent<Renderer>().material.color = col;


        //The Interferon powerup doubles Immunity spread by that cell. 
        StartCoroutine(TurnTextOn(12));
        stop.Add(StartCoroutine(FadeOutObject(antigen)));
        stop.Add(StartCoroutine(FadeInObject(interferon)));

        for (int i = 1; i < cells.Length; i++)
        {
            GameObject p = Instantiate(immunityParticles, cells[0].transform.position, Quaternion.LookRotation(cells[i].transform.position - cells[0].transform.position), objects.transform) as GameObject;
            p.GetComponent<ImmunityParticles>().target = cells[i].transform;
            p.GetComponent<ImmunityParticles>().immunity = .2f;
            p.GetComponent<ImmunityParticles>().startSpeed = 15;
            p.GetComponent<ImmunityParticles>().enabled = true;
        }

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();

        antigen.SetActive(false);
        col = interferon.GetComponent<Renderer>().material.color;
        col.a = 1;
        interferon.GetComponent<Renderer>().material.color = col;

        //The Protein powerup changes the protein to a random different protein. 
        StartCoroutine(TurnTextOn(13));
        stop.Add(StartCoroutine(FadeOutObject(interferon)));
        stop.Add(StartCoroutine(FadeInObject(protein)));
        proDes.text = "Protein: RNase L";

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();

        interferon.SetActive(false);
        col = protein.GetComponent<Renderer>().material.color;
        col.a = 1;
        protein.GetComponent<Renderer>().material.color = col;

        //Immunity is a very versatile stat and should be leveled in a variety of situations.  
        StartCoroutine(TurnTextOn(14));
        stop.Add(StartCoroutine(FadeOutObject(protein)));

        while (!advance)
            yield return 0;
        advance = false;

        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();
        protein.SetActive(false);

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
        virus.SetActive(false);
        foreach (GameObject item in imm)
        {
            item.SetActive(false);
        }
        pro.SetActive(false);

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