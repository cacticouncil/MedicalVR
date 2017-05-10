using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    // Use this for initialization
    void OnEnable()
    {
        StartCoroutine(FadeInObject(gameObject.GetComponent<Renderer>()));
    }

    IEnumerator FadeInObject(Renderer g)
    {
        g.enabled = true;
        if (g.material.HasProperty("_Color"))
        {
            Color c = g.material.color;
            c.a = 0.0f;
            float startTime = Time.time;
            float t = 0.0f;
            while (t < 1)
            {
                t = Time.time - startTime;
                c.a = t;
                g.material.color = c;
                yield return 0;
            }
            c.a = 1.0f;
            g.material.color = c;
        }
        enabled = false;
    }
}
