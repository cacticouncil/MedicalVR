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
    public GameObject[] imm = new GameObject[0];
    public GameObject[] cells = new GameObject[7];
    public List<TMPro.TextMeshPro> text = new List<TMPro.TextMeshPro>();

    private Vector3 prevPos;
    private GameObject fade;

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
        immDes.text = "Immunity: 0";
        Vector3 offset = cells[0].transform.localPosition;
        cells[0].transform.localPosition = Vector3.zero;
        cells[1].transform.localPosition -= offset;
        cells[2].transform.localPosition -= offset;
        cells[3].transform.localPosition -= offset;
        cells[4].transform.localPosition -= offset;
        cells[5].transform.localPosition -= offset;
        cells[6].transform.localPosition -= offset;

        StartCoroutine(FadeOutObject(fade));
        yield return new WaitForSeconds(1.0f);

        //Immunity spreads from cell to cell and helps you fight back against viruses in multiple ways. 
        StartCoroutine(StartText());
        StartCoroutine(GrowInObject(cells[0]));
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
        yield return new WaitForSeconds(3.0f);

        //With at least 1 Immunity, cells will spread a portion of their Immunity to adjacent cells. 
        StartCoroutine(TurnTextOn(1));
        yield return new WaitForSeconds(1.0f);
        immDes.text = "Immunity: 1";
        for (int i = 1; i < cells.Length; i++)
        {
            GameObject p = Instantiate(immunityParticles, cells[0].transform.position, Quaternion.LookRotation(cells[i].transform.position - cells[0].transform.position), objects.transform) as GameObject;
            p.GetComponent<ImmunityParticles>().target = cells[i].transform;
            p.GetComponent<ImmunityParticles>().immunity = .01f;
            p.GetComponent<ImmunityParticles>().startSpeed = 15;
            p.GetComponent<ImmunityParticles>().enabled = true;
        }
        yield return new WaitForSeconds(2.0f);

        //With only 1 Immunity, adjacent cells will receive 1 Immunity each in 100 turns. 
        StartCoroutine(TurnTextOn(2));
        yield return new WaitForSeconds(1.0f);
        for (int i = 1; i < cells.Length; i++)
        {
            GameObject p = Instantiate(immunityParticles, cells[0].transform.position, Quaternion.LookRotation(cells[i].transform.position - cells[0].transform.position), objects.transform) as GameObject;
            p.GetComponent<ImmunityParticles>().target = cells[i].transform;
            p.GetComponent<ImmunityParticles>().immunity = .01f;
            p.GetComponent<ImmunityParticles>().startSpeed = 15;
            p.GetComponent<ImmunityParticles>().enabled = true;
        }
        yield return new WaitForSeconds(2.0f);

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
        yield return new WaitForSeconds(2.0f);

        //At 10 Immunity, a cell spawns a random protein. 
        StartCoroutine(TurnTextOn(4));
        proDes.text = "Protein: RNase L";
        StartCoroutine(FadeInObject(pro));
        StartCoroutine(FadeInText(pro.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>()));
        yield return new WaitForSeconds(1);
        immDes.text = "Immunity: 10";
        yield return new WaitForSeconds(2);

        //Most stop the virus from replicating when it kills the cell.
        StartCoroutine(TurnTextOn(5));
        yield return new WaitForSeconds(1);
        proDes.text = "Protein: PKR";
        yield return new WaitForSeconds(.66f);
        proDes.text = "Protein: TRIM22";
        yield return new WaitForSeconds(.66f);
        proDes.text = "Protein: IFIT";
        yield return new WaitForSeconds(.68f);

        //But, you can get some that attempt to stop the virus before it kills the cell. 
        StartCoroutine(TurnTextOn(6));
        yield return new WaitForSeconds(.5f);
        proDes.text = "Protein: CH25H";
        yield return new WaitForSeconds(1.5f);
        proDes.text = "Protein: Mx1";
        yield return new WaitForSeconds(1.5f);

        //You can click on the Protein tab to see the list of proteins and what each does. 
        StartCoroutine(TurnTextOn(7));
        StartCoroutine(FadeInObject(proOutline));
        yield return new WaitForSeconds(3);

        //Each virus has an Immunity value that the cell’s immunity value must be higher than to kill the virus. 
        StartCoroutine(TurnTextOn(8));
        StartCoroutine(FadeOutObject(proOutline));
        StartCoroutine(FadeInObject(virus));
        StartCoroutine(FadeInObject(virusImm));
        yield return new WaitForSeconds(1);
        immDes.text = "Immunity: 20";
        yield return new WaitForSeconds(2);

        //When a virus is killed with Immunity that value is subtracted from the cell’s Immunity. 
        StartCoroutine(TurnTextOn(9));
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeOutObject(virus));
        StartCoroutine(FadeOutObject(virusImm));
        for (int i = 20; i > -1; i--)
        {
            immDes.text = "Immunity: " + i;
            yield return new WaitForSeconds(.1f);
        }

        //The Immunity required to kill each virus increases as the game continues. 
        StartCoroutine(TurnTextOn(10));
        yield return new WaitForSeconds(3);

        //The Antigen powerup increases Immunity by 10. 
        StartCoroutine(TurnTextOn(11));
        StartCoroutine(FadeInObject(antigen));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOutObject(antigen));
        for (int i = 0; i < 11; i++)
        {
            immDes.text = "Immunity: " + i;
            yield return new WaitForSeconds(.2f);
        }

        //The Interferon powerup doubles Immunity spread by that cell. 
        StartCoroutine(TurnTextOn(12));
        StartCoroutine(FadeInObject(interferon));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOutObject(interferon));
        for (int i = 1; i < cells.Length; i++)
        {
            GameObject p = Instantiate(immunityParticles, cells[0].transform.position, Quaternion.LookRotation(cells[i].transform.position - cells[0].transform.position), objects.transform) as GameObject;
            p.GetComponent<ImmunityParticles>().target = cells[i].transform;
            p.GetComponent<ImmunityParticles>().immunity = .2f;
            p.GetComponent<ImmunityParticles>().startSpeed = 15;
            p.GetComponent<ImmunityParticles>().enabled = true;
        }
        yield return new WaitForSeconds(2);

        //The Protein powerup changes the protein to a random different protein. 
        StartCoroutine(TurnTextOn(13));
        StartCoroutine(FadeInObject(protein));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOutObject(protein));
        proDes.text = "Protein: RNase L";
        yield return new WaitForSeconds(2);

        //Immunity is a very versatile stat and should be leveled in a variety of situations.  
        StartCoroutine(TurnTextOn(14));
        yield return new WaitForSeconds(3);

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
        virus.SetActive(false);
        
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
}