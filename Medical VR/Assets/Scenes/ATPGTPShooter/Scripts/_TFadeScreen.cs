using UnityEngine;
using System.Collections;

public class _TFadeScreen : MonoBehaviour
{
    public float fadeTime = 2;
    public Transform cameraPosition;
    private Vector3 camPos;
    private Vector3 startPosition;
    public bool startDarkScreen;

    bool turnToBlack;
    bool turnToScene;
    Color startColor;
    float curfade;

    void Start()
    {
        if (cameraPosition)
            camPos = cameraPosition.position;
        else
            camPos = new Vector3(0, 1, 0);

        startPosition = transform.position;
        transform.position = camPos;

        startColor = GetComponent<Renderer>().material.color;
        if (startDarkScreen)
            ResetToBlack();
        else
            ResetToScene();
    }

    void Update()
    {
        if (turnToBlack)
            TurnToBlack();
        else if (turnToScene)
            TurnToScene();
    }

    public void StartBlackScreen()
    {
        Invoke("InvokeBlack", .1f);
    }
    public void StartScene()
    {
        Invoke("InvokeStart", .1f);
    }
    void InvokeBlack()
    {
        turnToBlack = true;
        turnToScene = false;
    }
    void InvokeStart()
    {
        turnToScene = true;
        turnToBlack = false;
    }
    public void ResetToBlack()
    {
        turnToScene = false;
        turnToBlack = false;
        transform.position = camPos;
        GetComponent<Renderer>().material.color = startColor;
        curfade = fadeTime;
    }
    public void ResetToScene()
    {
        Color tmp = startColor;
        tmp.a = 0;

        turnToScene = false;
        turnToBlack = false;
        transform.position = startPosition;
        GetComponent<Renderer>().material.color = tmp;
        curfade = 0;
    }

    void TurnToBlack()
    {
        float curValue = curfade / fadeTime;
        curfade += Time.deltaTime;
        if (curfade >= fadeTime)
        {
            ResetToBlack();
            return;
        }
        Color tmp = startColor;
        tmp.a = curValue;
        GetComponent<Renderer>().material.color = tmp;
    }
    void TurnToScene()
    {
        float curValue = curfade / fadeTime;
        curfade -= Time.deltaTime;
        if (curfade <= 0)
        {
            ResetToScene();
            return;
        }
        Color tmp = startColor;
        tmp.a = curValue;
        GetComponent<Renderer>().material.color = tmp;
    }
}
