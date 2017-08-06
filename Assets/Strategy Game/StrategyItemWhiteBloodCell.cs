using UnityEngine;
using System.Collections;

public class StrategyItemWhiteBloodCell : MonoBehaviour
{
    public StrategyVirusScript target;
    private StrategyCellScript cell;
    private Vector3 spawnLocation;

    void Start()
    {
        cell = target.target;
        spawnLocation = transform.position;
        target.enabled = false;
        StartCoroutine(Arrive());
    }

    IEnumerator Arrive()
    {
        float startTime = Time.time;
        Vector3 scale = transform.localScale;
        float t = Time.time - startTime;
        while (t < 1.0f)
        {
            t = Time.time - startTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, scale, t);
            transform.position = Vector3.Lerp(spawnLocation, target.transform.position, t);
            yield return 0;
        }
        transform.localScale = scale;
        transform.position = target.transform.position;
        target.StartCoroutine(target.Die());
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Leave());
    }

    IEnumerator Leave()
    {
        float startTime = Time.time;
        Vector3 pos = transform.position;
        Vector3 direction = transform.position - spawnLocation;
        direction *= 2.0f;
        direction.y = 10.0f;
        Vector3 camPos = Camera.main.GetComponent<Transform>().position;
        Vector3 dir = transform.position - camPos;
        dir.Normalize();
        MoveCamera.instance.SetDestination(camPos + dir);

        if (transform.parent.GetComponent<StrategyCellManagerScript>().selected == cell.key)
            cell.ToggleUI(true);
        float t = 0;
        while (t < 10.0f)
        {
            t = Time.time - startTime;
            transform.position = pos + direction * t;
            yield return 0;
        }

        Destroy(gameObject);
    }
}