using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class _TTimerUI : MonoBehaviour
{
    private Camera cam;
    public GameObject gameController;

    private const string DISPLAY_TEXT_FORMAT = "Time\n{0}";

    private TMPro.TextMeshPro tmPro;

    void Awake()
    {
        tmPro = GetComponent<TextMeshPro>();
    }

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (cam != null)
        {
            transform.SetParent(cam.GetComponent<Transform>(), true);
        }
    }

    void LateUpdate()
    {
        tmPro.text = string.Format(DISPLAY_TEXT_FORMAT, Mathf.Ceil(gameController.GetComponent<_TGameController>().remainingTime));
    }
}