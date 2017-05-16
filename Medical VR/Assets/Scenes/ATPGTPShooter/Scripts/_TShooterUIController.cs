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
    float changeRounds = .75f;

    public Camera cam;
    bool isInit = false;
    bool firstShot = false;

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

        currentShot = (ShotNumber)player.GetComponent<_TPlayerController>().GetShotNumber();

        if (currentShot == ShotNumber.ATPOne)
        {
            selectShot("GTP ");
            transform.GetChild((int)ShotNumber.GTPOne).GetComponent<_TSizeChange>().StartShrink();
            Invoke("SetText", changeRounds);
        }
        else if(currentShot == ShotNumber.GTPOne)
        {
            selectShot("ATP ");
            transform.GetChild((int)ShotNumber.ATPOne).GetComponent<_TSizeChange>().StartShrink();
            Invoke("SetText", changeRounds);
        }
        else
            SetText();
    }
    void ShrinkFirstOnes()
    {
       
    }
    void selectShot(string mol)
    {
        tmPro.text = string.Format(DISPLAY_TEXT_FORMAT,
                mol, 0);
    }
    void SetText()
    {
        switch (currentShot)
        {
            case ShotNumber.ATPOne:
                transform.GetChild((int)ShotNumber.ATPOne).GetComponent<_TSizeChange>().StartGrow();
                transform.GetChild((int)ShotNumber.ATPTwo).GetComponent<_TSizeChange>().StartGrow();
                transform.GetChild((int)ShotNumber.ATPThree).GetComponent<_TSizeChange>().StartGrow();
                break;
            case ShotNumber.ATPTwo:
                transform.GetChild((int)ShotNumber.ATPThree).GetComponent<_TSizeChange>().StartShrink();
                break;
            case ShotNumber.ATPThree:
                transform.GetChild((int)ShotNumber.ATPTwo).GetComponent<_TSizeChange>().StartShrink();
                break;
            case ShotNumber.GTPOne:
                transform.GetChild((int)ShotNumber.GTPOne).GetComponent<_TSizeChange>().StartGrow();
                transform.GetChild((int)ShotNumber.GTPTwo).GetComponent<_TSizeChange>().StartGrow();
                transform.GetChild((int)ShotNumber.GTPThree).GetComponent<_TSizeChange>().StartGrow();
                break;
            case ShotNumber.GTPTwo:
                transform.GetChild((int)ShotNumber.GTPThree).GetComponent<_TSizeChange>().StartShrink();
                break;
            case ShotNumber.GTPThree:
                transform.GetChild((int)ShotNumber.GTPTwo).GetComponent<_TSizeChange>().StartShrink();
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
