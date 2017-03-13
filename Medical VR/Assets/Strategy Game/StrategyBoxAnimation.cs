using UnityEngine;
using System.Collections;

public class StrategyBoxAnimation : MonoBehaviour
{
    public Vector3 midScale = new Vector3(1, 1, 1);
    public Vector3 midPosition = new Vector3(1, 1, 1);
    public Vector3 endScale = new Vector3(1, 1, 1);
    public Vector3 endPosition = new Vector3(1, 1, 1);
    
    public GameObject text;

    private Vector3 start;
    private float startTime;

    // Use this for initialization
    void Start()
    {
        start = transform.localPosition;
    }

    void OnEnable()
    {
        Debug.Log("On Enable Called");
        GetComponent<SpriteRenderer>().enabled = true;
        text.SetActive(false);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float percent = (Time.time - startTime) * 1f;
        if (percent > 2f)
        {
            transform.localPosition = endPosition;
            transform.localScale = endScale;
            text.SetActive(true);
        }
        else if (percent > 1f)
        {
            transform.localPosition = Vector3.Lerp(midPosition, endPosition, percent - 1.0f);
            transform.localScale = Vector3.Lerp(midScale, endScale, percent - 1.0f);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(start, midPosition, percent);
            transform.localScale = Vector3.Lerp(Vector3.zero, midScale, percent);
        }
    }

    void Reset()
    {
        transform.position = start;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
    }
}
