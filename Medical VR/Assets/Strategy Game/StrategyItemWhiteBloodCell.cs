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

    void DestroyTarget()
    {
        Destroy(target.gameObject);
        StartCoroutine(Leave());
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
            transform.position = Vector3.Lerp(spawnLocation, cell.transform.position, t);
            yield return 0;
        }
        transform.localScale = scale;
        transform.position = cell.transform.position;
        Invoke("DestroyTarget", 1);
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
        Camera.main.GetComponent<MoveCamera>().SetDestination(camPos + dir);

        cell.RefreshUI();
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