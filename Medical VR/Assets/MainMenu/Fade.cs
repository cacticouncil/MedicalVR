using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
    private static Fade localInstance;
    public static Fade fade { get { return localInstance; } }

    public static UnityEngine.EventSystems.EventSystem eventSystem;

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

        @in.SetActive(true);
        foreach (Renderer item in @in.GetComponentsInChildren<Renderer>(true))
        {
            if (item.material.HasProperty("_Color"))
                fade.StartCoroutine(fade.FadeInObject(item));
        }
        foreach (TMPro.TextMeshPro item in @in.GetComponentsInChildren<TMPro.TextMeshPro>(true))
        {
            fade.StartCoroutine(fade.FadeInText(item));
        }

        fade.Invoke("EnableEventSystem", 1.0f);
    }

    public static void FadeOut(GameObject @out, GameObject @in)
    {
        eventSystem = UnityEngine.EventSystems.EventSystem.current;
        UnityEngine.EventSystems.EventSystem.current.enabled = false;
        if (fade == null)
        {
            GameObject o = new GameObject("Fade");
            localInstance = o.AddComponent<Fade>();
        }

        SoundManager.PlaySFX("MenuEnter");

        foreach (Renderer item in @out.GetComponentsInChildren<Renderer>(true))
        {
            if (item.material.HasProperty("_Color"))
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
        eventSystem = UnityEngine.EventSystems.EventSystem.current;
        UnityEngine.EventSystems.EventSystem.current.enabled = false;
        if (fade == null)
        {
            GameObject o = new GameObject("Fade");
            localInstance = o.AddComponent<Fade>();
        }

        SoundManager.PlaySFX("MenuEnter");

        foreach (Renderer item in outin.@out.GetComponentsInChildren<Renderer>(true))
        {
            if (item.material.HasProperty("_Color"))
                fade.StartCoroutine(fade.FadeOutObject(item));
        }
        foreach (TMPro.TextMeshPro item in outin.@out.GetComponentsInChildren<TMPro.TextMeshPro>(true))
        {
            fade.StartCoroutine(fade.FadeOutText(item));
        }

        fade.StartCoroutine(fade.TurnOff(outin.@out, outin.@in));
    }

    public void EnableEventSystem()
    {
        if (eventSystem)
            eventSystem.enabled = true;
    }

    #region Enumerators
    IEnumerator TurnOff(GameObject @out, GameObject @in)
    {
        yield return new WaitForSeconds(.5f);
        @out.SetActive(false);

        FadeIn(@in);
    }

    IEnumerator FadeInObject(Renderer g)
    {
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
    }
    #endregion
}
