using UnityEngine;
using System.Collections;

public class Shrink : MonoBehaviour
{
    public Vector3 startingSize;
    private float startTime;
    // Use this for initialization
    void Start()
    {
        startingSize = transform.localScale;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(startingSize, Vector3.zero, Time.time - startTime);
    }
}
