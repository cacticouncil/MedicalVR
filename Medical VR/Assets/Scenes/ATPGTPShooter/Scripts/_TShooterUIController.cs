using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using TMPro;
public class _TShooterUIController : MonoBehaviour
{

    ShotNumber currentShot;
    public GameObject player;

    private const string DISPLAY_TEXT_FORMAT = "{0} Loaded\n{1} : Rounds Left";
    private const string MSF_FORMAT = "#.#";
    //    private const float MS_PER_SEC = 1000f;

   // private TextMesh textField;
    private TMPro.TextMeshPro tmPro;
    //    private float fps = 60;

    public Camera cam;
    bool isInit = false;
    bool firstShot;

    void Awake()
    {
        Initialize();
    }
    void Initialize()
    {
        if (isInit)
            return;
        isInit = true;
        tmPro = GetComponent<TextMeshPro>();
        firstShot = false;
        foreach(Transform child in transform)
        {
            
            child.GetComponent<_TSizeChange>().Inititalize();
            child.GetComponent<_TSizeChange>().ResetToSmall();
        }
    }

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (cam != null)
        {
            // Tie this to the camera, and do not keep the local orientation.
            transform.SetParent(cam.GetComponent<Transform>(), true);
        }
    }

    void LateUpdate()
    {
        if (!isInit || (currentShot == (ShotNumber)player.GetComponent<_TPlayerController>().GetShotNumber() && firstShot))
            return;
        firstShot = true;

        if (currentShot == ShotNumber.ATPOne || currentShot == ShotNumber.GTPOne)
            Invoke("SetText", 1);
        else
            SetText();
        currentShot = (ShotNumber)player.GetComponent<_TPlayerController>().GetShotNumber();

    }
    void SetText()
    {
        
        Debug.Log("We are here");

        switch (currentShot)
        {
            case ShotNumber.ATPOne:
                transform.GetChild(3).GetComponent<_TSizeChange>().StartShrink();
                transform.GetChild(0).GetComponent<_TSizeChange>().StartGrow();
                transform.GetChild(1).GetComponent<_TSizeChange>().StartGrow();
                transform.GetChild(2).GetComponent<_TSizeChange>().StartGrow();
                break;
            case ShotNumber.ATPTwo:
                transform.GetChild(2).GetComponent<_TSizeChange>().StartShrink();
                break;
            case ShotNumber.ATPThree:
                transform.GetChild(1).GetComponent<_TSizeChange>().StartShrink();
                break;
            case ShotNumber.GTPOne:
                transform.GetChild(0).GetComponent<_TSizeChange>().StartShrink();
                transform.GetChild(3).GetComponent<_TSizeChange>().StartGrow();
                transform.GetChild(4).GetComponent<_TSizeChange>().StartGrow();
                transform.GetChild(5).GetComponent<_TSizeChange>().StartGrow();
                break;
            case ShotNumber.GTPTwo:
                transform.GetChild(3).GetComponent<_TSizeChange>().StartShrink();
                break;
            case ShotNumber.GTPThree:
                transform.GetChild(4).GetComponent<_TSizeChange>().StartShrink();
                break;

        }
        if ((int)currentShot < 3)
        {            
            tmPro.text = string.Format(DISPLAY_TEXT_FORMAT,
                "ATP ", Mathf.Abs((int)currentShot - 3));
        }
        else
        {
            tmPro.text = string.Format(DISPLAY_TEXT_FORMAT,
                "GTP ", Mathf.Abs((int)currentShot - 6));
        }
    }
}
