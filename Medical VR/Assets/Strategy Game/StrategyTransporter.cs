using UnityEngine;
using System.Collections;

public class StrategyTransporter : MonoBehaviour
{
    public Vector3 destination;

    private Vector3 startPos;
    private float startTime;
    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        transform.position = Vector3.Lerp(startPos, destination, t);
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
        if (t >= 1.0f)
        {
            if (transform.childCount > 0)
            {
                if (transform.GetChild(0).GetComponent<StrategyVirusScript>())
                {
                    transform.GetChild(0).GetComponent<StrategyVirusScript>().enabled = true;
                }
                if (transform.GetChild(0).GetComponent<Collider>())
                {
                    transform.GetChild(0).GetComponent<Collider>().enabled = true;
                }
                if (transform.GetChild(0).transform.childCount > 0)
                {
                    for (int i = 0; i < transform.GetChild(0).transform.childCount; i++)
                    {
                        if (transform.GetChild(0).transform.GetChild(i).GetComponent<Collider>())
                        {
                            transform.GetChild(0).transform.GetChild(i).GetComponent<Collider>().enabled = true;
                        }
                    }
                }
                transform.GetChild(0).parent = transform.parent;
            }
            Destroy(gameObject);
        }
    }
}
