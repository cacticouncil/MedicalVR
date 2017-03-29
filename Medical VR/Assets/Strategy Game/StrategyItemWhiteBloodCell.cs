﻿using UnityEngine;
using System.Collections;

public class StrategyItemWhiteBloodCell : MonoBehaviour
{
    public StrategyVirusScript target;
    public GameObject transporter;
    private StrategyCellScript cell;

    void Start()
    {
        cell = target.target;
        target.enabled = false;
        GameObject t = Instantiate(transporter, transform.position, Quaternion.identity) as GameObject;
        t.GetComponent<StrategyTransporter>().destination = target.transform.position;
        transform.parent = t.transform;
        t.GetComponent<StrategyTransporter>().enabled = true;
        Invoke("DestroyTarget", 1);
    }

    void DestroyTarget()
    {
        Destroy(target.gameObject);
        StartCoroutine(Leave());
    }

    IEnumerator Leave()
    {
        float startTime = Time.time;
        Vector3 scale = transform.localScale;
        float t = Time.time - startTime;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            transform.localScale = Vector3.Lerp(scale, Vector3.zero, t);
            yield return new WaitForFixedUpdate();
        }

        Vector3 camPos = Camera.main.GetComponent<Transform>().position;
        Vector3 dir = transform.position - camPos;
        dir.Normalize();
        Camera.main.GetComponent<MoveCamera>().SetDestination(camPos + dir);

        cell.RefreshUI();
        cell.ToggleUI(true);
        Destroy(gameObject);
    }
}