using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyTutorialImmunity : MonoBehaviour
{
    public Transform cam;
    public GameObject eventSystem;
    public GameObject fade;
    public LookCamera lc;
    public GameObject reticle;
    public GameObject immunityParticles;
    public GameObject objects;
    public GameObject antigen, interferon, protein;
    public GameObject proOutline;
    public GameObject virusImm;
    public GameObject virus;
    public TMPro.TextMeshPro immDes;
    public TMPro.TextMeshPro proDes;
    public GameObject pro;
    public TMPro.TextMeshPro subtitles;
    public GameObject[] imm;
    public GameObject[] cells;

    private string[] texts =
        {
        "Immunity spreads from cell to cell and helps you fight back against viruses in multiple ways.",
        "With at least 1 Immunity, cells will spread Immunity to adjacent cells.",
        "With only 1 Immunity, adjacent cells will receive 1 Immunity each in 100 turns.",
        "1 cell surrounded by 6 cells with 1 immunity takes 16 turns to receive 1 Immunity.",
        "At 10 Immunity, a cell spawns a random protein.",
        "Most stop the virus from replicating when it kills the cell.",
        "But, you can get some that attempt to stop the virus before it kills the cell.",
        "You can click on the Protein tab to see the list of proteins and what each does.",
        "Each virus has an Immunity value that the cell's immunity value must be higher than to kill the virus.",
        "When a virus is killed with Immunity that value is subtracted from the cell's Immunity.",
        "The Immunity required to kill each virus increases as the game continues.",
        "The Antigen power-up increases Immunity by 10.",
        "The Interferon power-up doubles Immunity spread by that cell for 25 turns.",
        "The Protein power-up changes the protein to a different protein.",
        "Immunity is a very versatile stat and should be leveled in a variety of situations."
        };
    private Vector3 prevPos;
    private Quaternion prevRotation;
    private bool last = false, text = false, finish = false, clickable = false;
    private List<Coroutine> stop = new List<Coroutine>();
    private int index = 1;
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
        index = 1;
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

    void Click()
    {
        switch (index)
        {
            case 1:
                //Fade Out
                objects.SetActive(true);
                reticle.SetActive(false);
                cam.position = transform.position;
                lc.target = cells[0].transform;
                lc.enabled = true;
                immDes.text = "Immunity: 0";
                fade.GetComponent<FadeOut>().enabled = true;
                Invoke("Click", 1);
                break;
            case 2:
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
                clickable = true;
                break;
            case 3:
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
                    c = item.GetComponent<Renderer>().material.color;
                    c.a = 1;
                    item.GetComponent<Renderer>().material.color = c;
                    item.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().color = Color.black;
                }

                //With at least 1 Immunity, cells will spread Immunity to adjacent cells.
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
                break;
            case 4:
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
                break;
            case 5:
                //1 cell surrounded by 6 cells with 1 immunity takes 16 turns to receive 1 Immunity.
                StartCoroutine(TurnTextOn(3));
                for (int i = 1; i < cells.Length; i++)
                {
                    GameObject p = Instantiate(immunityParticles, cells[i].transform.position, Quaternion.LookRotation(cells[0].transform.position - cells[i].transform.position), objects.transform) as GameObject;
                    p.GetComponent<ImmunityParticles>().target = cells[0].transform;
                    p.GetComponent<ImmunityParticles>().immunity = .01f;
                    p.GetComponent<ImmunityParticles>().startSpeed = 15;
                    p.GetComponent<ImmunityParticles>().enabled = true;
                }
                break;
            case 6:
                //At 10 Immunity, a cell spawns a random protein.
                StartCoroutine(TurnTextOn(4));
                proDes.text = "Protein: RNase L";
                stop.Add(StartCoroutine(FadeInObject(pro)));
                stop.Add(StartCoroutine(FadeInText(pro.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>())));
                immDes.text = "Immunity: 10";
                break;
            case 7:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();

                c = pro.GetComponent<Renderer>().material.color;
                c.a = 1;
                pro.GetComponent<Renderer>().material.color = c;
                pro.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().color = Color.black;

                //Most stop the virus from replicating when it kills the cell.
                StartCoroutine(TurnTextOn(5));
                break;
            case 8:
                proDes.text = "Protein: PKR";
                break;
            case 9:
                proDes.text = "Protein: TRIM22";
                break;
            case 10:
                proDes.text = "Protein: IFIT";
                break;
            case 11:
                //But, you can get some that attempt to stop the virus before it kills the cell.
                StartCoroutine(TurnTextOn(6));
                proDes.text = "Protein: CH25H";
                break;
            case 13:
                proDes.text = "Protein: Mx1";
                break;
            case 14:
                //You can click on the Protein tab to see the list of proteins and what each does.
                StartCoroutine(TurnTextOn(7));
                stop.Add(StartCoroutine(FadeInObject(proOutline)));
                break;
            case 15:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                proOutline.GetComponent<Renderer>().material.color = Color.red;

                //Each virus has an Immunity value that the cell's immunity value must be higher than to kill the virus.
                StartCoroutine(TurnTextOn(8));
                stop.Add(StartCoroutine(FadeOutObject(proOutline)));
                stop.Add(StartCoroutine(FadeInObject(virus)));
                stop.Add(StartCoroutine(FadeInObject(virusImm)));
                cells[0].GetComponent<Rotate>().enabled = false;
                stop.Add(StartCoroutine(PaintItBlack(cells[0])));
                immDes.text = "Immunity: 20";
                break;
            case 16:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                proOutline.SetActive(false);
                cells[0].GetComponent<Renderer>().material.color = Color.black;
                c = virus.GetComponent<Renderer>().material.color;
                c.a = 1;
                virus.GetComponent<Renderer>().material.color = c;
                virus.GetComponent<Renderer>().material.SetColor("_Outline", Color.black);
                c = virusImm.GetComponent<Renderer>().material.color;
                c.a = 1;
                virusImm.GetComponent<Renderer>().material.color = c;

                //When a virus is killed with Immunity that value is subtracted from the cell's Immunity.
                StartCoroutine(TurnTextOn(9));
                stop.Add(StartCoroutine(FadeOutObject(virus)));
                stop.Add(StartCoroutine(FadeOutObject(virusImm)));
                Invoke("Immunity0", 1);
                break;
            case 17:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                virus.SetActive(false);
                virusImm.SetActive(false);
                immDes.text = "Immunity: 0";
                cells[0].GetComponent<Renderer>().material.color = Color.grey;
                cells[0].GetComponent<Rotate>().enabled = true;

                //The Immunity required to kill each virus increases as the game continues.
                StartCoroutine(TurnTextOn(10));
                break;
            case 18:
                //The Antigen power-up increases Immunity by 10.
                StartCoroutine(TurnTextOn(11));
                stop.Add(StartCoroutine(FadeInObject(antigen)));
                immDes.text = "Immunity: 10";
                break;
            case 19:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                c = antigen.GetComponent<Renderer>().material.color;
                c.a = 1;
                antigen.GetComponent<Renderer>().material.color = c;

                //The Interferon power-up doubles Immunity spread by that cell for 25 turns.
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
                break;
            case 20:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                antigen.SetActive(false);
                c = interferon.GetComponent<Renderer>().material.color;
                c.a = 1;
                interferon.GetComponent<Renderer>().material.color = c;

                //The Protein power-up changes the protein to a different protein.
                StartCoroutine(TurnTextOn(13));
                stop.Add(StartCoroutine(FadeOutObject(interferon)));
                stop.Add(StartCoroutine(FadeInObject(protein)));
                proDes.text = "Protein: RNase L";
                break;
            case 21:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();

                interferon.SetActive(false);
                c = protein.GetComponent<Renderer>().material.color;
                c.a = 1;
                protein.GetComponent<Renderer>().material.color = c;

                //Immunity is a very versatile stat and should be leveled in a variety of situations.
                StartCoroutine(TurnTextOn(14));
                stop.Add(StartCoroutine(FadeOutObject(protein)));
                break;
            case 22:
                foreach (Coroutine co in stop)
                {
                    StopCoroutine(co);
                }
                stop.Clear();
                protein.SetActive(false);

                //Fade In
                fade.GetComponent<FadeIn>().enabled = true;
                subtitles.text = "";
                clickable = false;
                Invoke("Click", 1);
                break;
            case 23:
                //Fade Out
                reticle.SetActive(true);
                cam.position = prevPos;
                lc.transform.rotation = prevRotation;
                fade.GetComponent<FadeOut>().enabled = true;
                Invoke("Click", 1);
                break;
            case 24:
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
    #endregion
    void Immunity0()
    {
        immDes.text = "Immunity: 0";
    }
}