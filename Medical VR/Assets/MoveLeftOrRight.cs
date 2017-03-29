using UnityEngine;
using System.Collections;

public class MoveLeftOrRight : MonoBehaviour
{
    enum LeftOrRight
    {
        Left,
        Right
    }
    [SerializeField]
    private LeftOrRight lor;
    public float reproduction = 0.0f;
    public float distance = 1.0f;
    public float tReproduction = 50.0f;

    private Vector3 start;
    private float startTime;

    // Use this for initialization
    void Start()
    {
        start = transform.localPosition;
        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tReproduction += Mathf.Sqrt(reproduction * 10 + 1);
        float per = tReproduction / 50.0f;
        float delta = Mathf.Lerp(0, distance, per);
        switch (lor)
        {
            case LeftOrRight.Left:
                {
                    transform.localPosition = new Vector3(start.x + delta, start.y, start.z);
                }
                break;
            case LeftOrRight.Right:
                {
                    transform.localPosition = new Vector3(start.x - delta, start.y, start.z);
                }
                break;
            default:
                break;
        }

        if (per >= 1.0f)
        {
            tReproduction = 0.0f;
            startTime = Time.time;
        }
    }

    public void IncreaseSpeed()
    {
        reproduction++;   
    }
}
