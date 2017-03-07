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
    public float speed = 1.0f;
    public float speedScalar = 1.0f;
    public float distance = 1.0f;

    private float startx;
    private float startTime;

    // Use this for initialization
    void Start()
    {
        startx = transform.localPosition.x;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = (Time.time - startTime) * (distance * speed);
        switch (lor)
        {
            case LeftOrRight.Left:
                {
                    transform.localPosition -= new Vector3(delta, 0.0f, 0.0f);
                }
                break;
            case LeftOrRight.Right:
                {
                    transform.localPosition += new Vector3(delta, 0.0f, 0.0f);
                }
                break;
            default:
                break;
        }

        if (Mathf.Abs(startx - transform.localPosition.x) > distance)
        {
            startTime = Time.time;
            transform.localPosition = new Vector3(startx, transform.localPosition.y, transform.localPosition.z);
        }
    }

    public void IncreaseSpeed()
    {
        speed += speedScalar;
    }
}
