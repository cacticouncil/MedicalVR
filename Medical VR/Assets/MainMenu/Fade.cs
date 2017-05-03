using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
    private static Fade localInstance;
    public static Fade fade { get { return localInstance; } }

    private void Awake()
    {
        if (localInstance != null && localInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            localInstance = this;
        }
    }


    public static void FadeIn(GameObject @in)
    {
        if (fade == null)
        {
            GameObject o = new GameObject("Fade");
            localInstance = o.AddComponent<Fade>();
        }

        foreach (Transform item in @in.GetComponentsInChildren<Transform>(true))
        {
            item.gameObject.SetActive(true);
        }
        foreach (Renderer item in @in.GetComponentsInChildren<Renderer>(true))
        {
            fade.StartCoroutine(fade.FadeInObject(item));
        }
        foreach (TMPro.TextMeshPro item in @in.GetComponentsInChildren<TMPro.TextMeshPro>(true))
        {
            fade.StartCoroutine(fade.FadeInText(item));
        }
    }

    public static void FadeOut(GameObject @out, GameObject @in)
    {
        if (fade == null)
        {
            GameObject o = new GameObject("Fade");
            localInstance = o.AddComponent<Fade>();
        }

        SoundManager.PlaySFX("MenuEnter");

        foreach (Renderer item in @out.GetComponentsInChildren<Renderer>(true))
        {
            fade.StartCoroutine(fade.FadeOutObject(item));
        }
        foreach (TMPro.TextMeshPro item in @out.GetComponentsInChildren<TMPro.TextMeshPro>(true))
        {
            fade.StartCoroutine(fade.FadeOutText(item));
        }

        fade.StartCoroutine(fade.TurnOff(@out, @in));
    }

    public void FadeOut(OutIn outin)
    {
        if (fade == null)
        {
            GameObject o = new GameObject("Fade");
            localInstance = o.AddComponent<Fade>();
        }

        SoundManager.PlaySFX("MenuEnter");

        foreach (Renderer item in outin.@out.GetComponentsInChildren<Renderer>(true))
        {
            fade.StartCoroutine(fade.FadeOutObject(item));
        }
        foreach (TMPro.TextMeshPro item in outin.@out.GetComponentsInChildren<TMPro.TextMeshPro>(true))
        {
            fade.StartCoroutine(fade.FadeOutText(item));
        }

        fade.StartCoroutine(fade.TurnOff(outin.@out, outin.@in));
    }

    public static void TypeIn(GameObject @in)
    {
        if (fade == null)
        {
            GameObject o = new GameObject("Fade");
            localInstance = o.AddComponent<Fade>();
        }

        fade.StartCoroutine(fade.Draw(@in));
    }

    public static void DeleteOut(GameObject @out, GameObject @in)
    {
        if (fade == null)
        {
            GameObject o = new GameObject("Fade");
            localInstance = o.AddComponent<Fade>();
        }

        fade.StartCoroutine(fade.Erase(@out, @in));
    }

    public void DeleteOut(OutIn outin)
    {
        if (fade == null)
        {
            GameObject o = new GameObject("Fade");
            localInstance = o.AddComponent<Fade>();
        }

        fade.StartCoroutine(fade.Erase(outin.@out, outin.@in));
    }

    #region Enumerators
    IEnumerator Draw(GameObject @in)
    {
        foreach (Transform item in @in.GetComponentsInChildren<Transform>(true))
        {
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

            yield return new WaitForSeconds(GlobalVariables.textDelay);

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

    IEnumerator Erase(GameObject @out, GameObject @in)
    {
        TMPro.TextMeshPro[] texts = @out.GetComponentsInChildren<TMPro.TextMeshPro>(true);
        string[] strs = new string[texts.Length];
        string[] org = new string[texts.Length];
        for (int i = 0; i < texts.Length; i++)
        {
            strs[i] = texts[i].text;
            org[i] = texts[i].text;
        }

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

            yield return new WaitForSeconds(GlobalVariables.textDelay * .5f);

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

        foreach (Transform item in @out.GetComponentsInChildren<Transform>(true))
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = org[i];
        }

        StartCoroutine(Draw(@in));
    }

    IEnumerator TurnOff(GameObject o, GameObject @in)
    {
        yield return new WaitForSeconds(.5f);
        foreach (Transform item in o.GetComponentsInChildren<Transform>(true))
        {
            item.gameObject.SetActive(false);
        }

        FadeIn(@in);
    }


    IEnumerator FadeInObject(Renderer g)
    {
        g.gameObject.SetActive(true);
        if (g.material.HasProperty("_Color"))
        {
            Color c = g.material.color;
            c.a = 0.0f;
            float startTime = Time.time;
            float t = 0.0f;
            while (t < .5f)
            {
                t = Time.time - startTime;
                c.a = t * 2.0f;
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
            while (t < .5f)
            {
                t = Time.time - startTime;
                c.a = 1.0f - t * 2.0f;
                g.material.color = c;
                yield return 0;
            }
            c.a = 1.0f;
            g.material.color = c;
        }
        g.gameObject.SetActive(false);
    }


    IEnumerator FadeInText(TMPro.TextMeshPro text)
    {
        text.gameObject.SetActive(true);
        Color a = text.color;
        a.a = 0.0f;
        text.color = a;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < .5f)
        {
            t = Time.time - startTime;
            a.a = t * 2.0f;
            text.color = a;
            yield return 0;
        }
    }

    IEnumerator FadeOutText(TMPro.TextMeshPro text)
    {
        Color c = text.color;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < .5f)
        {
            t = Time.time - startTime;
            c.a = 1.0f - t * 2.0f;
            text.color = c;
            yield return 0;
        }
        c.a = 1.0f;
        text.color = c;
        text.gameObject.SetActive(false);
    }
    #endregion
}
