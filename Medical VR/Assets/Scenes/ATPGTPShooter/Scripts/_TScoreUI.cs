using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class _TScoreUI : MonoBehaviour
{
    private Camera cam;
    public GameObject gameController;
    private string DISPLAY_TEXT_FORMAT;
    private TMPro.TextMeshPro tmPro;

    void Awake()
    {
        if (GlobalVariables.arcadeMode)
        {
            DISPLAY_TEXT_FORMAT = "Score\n{0}";
        }
        else
        {
            DISPLAY_TEXT_FORMAT = "Score\n{0} / " + gameController.GetComponent<_TGameController>().winScore.ToString();
        }
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
        tmPro.text = string.Format(DISPLAY_TEXT_FORMAT, gameController.GetComponent<_TGameController>().score);
    }
}