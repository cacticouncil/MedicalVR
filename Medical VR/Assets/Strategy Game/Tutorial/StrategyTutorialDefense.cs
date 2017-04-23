using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrategyTutorialDefense : MonoBehaviour
{
    public GameObject cam;
    public GameObject eventSystem;
    public GameObject fadePrefab;
    public GameObject textHolder;
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
        textHolder.transform.position = forward + transform.position;
        yield return 0;
        textHolder.transform.LookAt(cam.transform);
        StartCoroutine(FadeOutObject(fade));
        yield return new WaitForSeconds(1.0f);


        //Start Text
        StartCoroutine(StartText());
        //Give it 1 sec to come in, then 1 sec to read
        yield return new WaitForSeconds(2.0f);

        for (int i = 1; i < text.Count; i++)
        {
            StartCoroutine(TurnTextOn(i));
            yield return new WaitForSeconds(2.0f);
        }

        StartCoroutine(EndText());
        yield return new WaitForSeconds(1.0f);


        //Fade to black
        StartCoroutine(FadeInObject(fade));
        yield return new WaitForSeconds(1.0f);

        //Fade In
        cam.transform.position = prevPos;
        StartCoroutine(FadeOutObject(fade));
        yield return new WaitForSeconds(1.5f);

        eventSystem.SetActive(true);
        Destroy(fade);
        enabled = false;
    }

    IEnumerator FadeInObject(GameObject g)
    {
        g.SetActive(true);
        Color start = g.GetComponent<Renderer>().material.color;
        start.a = 0;
        Color c = start;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.a = t;
            g.GetComponent<Renderer>().material.color = c;
            yield return 0;
        }
    }

    IEnumerator FadeOutObject(GameObject g)
    {
        Color start = g.GetComponent<Renderer>().material.color;
        Color c = start;
        float startTime = Time.time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            c.a = 1.0f - t;
            g.GetComponent<Renderer>().material.color = c;
            yield return 0;
        }
        g.SetActive(false);
    }

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