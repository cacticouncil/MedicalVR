using UnityEngine;
using System.Collections;

public class StrategyBoxAnimation : MonoBehaviour
{
    public Vector3 midScale = new Vector3(1, 1, 1);
    public Vector3 midPosition = new Vector3(1, 1, 1);
    public Vector3 endScale = new Vector3(1, 1, 1);
    public Vector3 endPosition = new Vector3(1, 1, 1);

    public GameObject text;
    public Transform rotateMe;
    public Vector3 startRotation, endRotation;

    private bool chestAnimation = true;
    private Vector3 start;
    private float startTime;

    // Use this for initialization
    void Start()
    {
        start = transform.localPosition;
        startRotation = rotateMe.rotation.eulerAngles;
    }

    void OnEnable()
    {
        Debug.Log("On Enable Called");
        text.SetActive(false);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (chestAnimation)
        {
            //Chest Animation
            float percent = (Time.time - startTime);

            if (percent < 1.0f)
            {
                rotateMe.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, endRotation, percent));
            }
            else
            {
                rotateMe.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, endRotation, 1.0f));
                chestAnimation = false;
                startTime = Time.time;
            }
        }
        else
        {
            //Potion Animation
            float percent = (Time.time - startTime);
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
                if (!GetComponent<Renderer>().enabled)
                    GetComponent<Renderer>().enabled = true;
            }
        }
    }

    public void Reset()
    {
        transform.position = start;
        rotateMe.rotation = Quaternion.Euler(startRotation);
        chestAnimation = true;
        GetComponent<Renderer>().enabled = false;
    }
}
