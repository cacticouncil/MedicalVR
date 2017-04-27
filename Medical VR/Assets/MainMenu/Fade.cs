using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
    public void FadeIn()
    {
        StartCoroutine(TurnOn());
    }

    public void FadeOut()
    {
        foreach (Renderer item in GetComponentsInChildren<Renderer>(true))
        {
            StartCoroutine(FadeOutObject(item));
        }

        foreach (TMPro.TextMeshPro item in GetComponentsInChildren<TMPro.TextMeshPro>(true))
        {
            StartCoroutine(FadeOutText(item));
        }

        StartCoroutine(TurnOff());
    }

    public void TypeIn()
    {
        StartCoroutine(Draw());
    }

    public void DeleteOut(Fade next)
    {
        StartCoroutine(Erase(next));
    }

    IEnumerator Draw()
    {
        foreach (Transform item in GetComponentsInChildren<Transform>(true))
        {
            if (item != transform)
                item.gameObject.SetActive(true);
        }

        TMPro.TextMeshPro[] texts = GetComponentsInChildren<TMPro.TextMeshPro>(true);
        string[] org = new string[texts.Length];
        for (int i = 0; i < texts.Length; i++)
        {
            org[i] = texts[i].text;
        }

        string[] strs = new string[texts.Length];

        for (int i = 0; i < texts.Length; i++)
        {
            strs[i] = "_";
        }

        int left = texts.Length;

        while (left > 0)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].text = strs[i];
            }

            yield return new WaitForSeconds(.1f);

            left = texts.Length;
            for (int i = 0; i < texts.Length; i++)
            {
                if (strs[i].Length - 1 == org[i].Length)
                {
                    strs[i] = strs[i].Remove(strs[i].Length - 1);
                }
                else if (strs[i] == org[i])
                {
                    left--;
                }
                else
                {
                    strs[i] = strs[i].Insert(strs[i].Length - 1, org[i][strs[i].Length - 1].ToString());
                }
            }
        }
    }

    IEnumerator Erase(Fade next)
    {
        TMPro.TextMeshPro[] texts = GetComponentsInChildren<TMPro.TextMeshPro>(true);
        string[] strs = new string[texts.Length];
        for (int i = 0; i < texts.Length; i++)
        {
            strs[i] = texts[i].text;
        }

        string[] org = strs;

        for (int i = 0; i < texts.Length; i++)
        {
            strs[i] = strs[i] + "_";
        }

        int left = texts.Length;

        while (left > 0)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].text = strs[i];
            }

            yield return new WaitForSeconds(.1f);

            left = texts.Length;
            for (int i = 0; i < texts.Length; i++)
            {
                if (strs[i].Length > 1)
                {
                    strs[i] = strs[i].Remove(strs[i].Length - 2, 1);
                }
                else
                {
                    left--;
                }
            }
        }

        foreach (Transform item in GetComponentsInChildren<Transform>(true))
        {
            if (item != transform)
                item.gameObject.SetActive(false);
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = org[i];
        }

        next.TypeIn();
    }

    IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(1.0f);
        foreach (Renderer item in GetComponentsInChildren<Renderer>(true))
        {
            StartCoroutine(FadeInObject(item));
        }
        foreach (TMPro.TextMeshPro item in GetComponentsInChildren<TMPro.TextMeshPro>(true))
        {
            StartCoroutine(FadeInText(item));
        }

        yield return new WaitForSeconds(1.0f);
        foreach (Transform item in GetComponentsInChildren<Transform>(true))
        {
            if (item != transform)
                item.gameObject.SetActive(true);
        }
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(1.0f);
        foreach (Transform item in GetComponentsInChildren<Transform>(true))
        {
            if (item != transform)
                item.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeInObject(Renderer g)
    {
        if (g.material.HasProperty("_Color"))
        {
            g.gameObject.SetActive(true);
            Color c = g.material.color;
            c.a = 0.0f;
            float startTime = Time.time;
            float t = 0.0f;
            while (t < 1.0f)
            {
                t = Time.time - startTime;
                c.a = t;
                g.material.color = c;
                yield return 0;
            }
        }
    }

    IEnumerator FadeOutObject(Renderer g)
    {
        if (g.material.HasProperty("_Color"))
        {
            Color c = g.material.color;
            c.a = 1.0f;
            float startTime = Time.time;
            float t = 0.0f;
            while (t < 1.0f)
            {
                t = Time.time - startTime;
                c.a = 1.0f - t;
                g.material.color = c;
                yield return 0;
            }
            g.gameObject.SetActive(false);
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
}
