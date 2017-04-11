using UnityEngine;
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

    void Awake()
    {
        //textField = GetComponent<TextMesh>();
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
            // Tie this to the camera, and do not keep the local orientation.
            transform.SetParent(cam.GetComponent<Transform>(), true);
        }
    }

    void LateUpdate()
    {
        currentShot = (ShotNumber)player.GetComponent<_TPlayerController>().GetShotNumber();

        switch (currentShot)
        {
            case ShotNumber.ATPOne:
                SetText(true, "3");
                break;
            case ShotNumber.ATPTwo:
                SetText(true, "2");
                break;
            case ShotNumber.ATPThree:
                SetText(true, "1");
                break;
            case ShotNumber.GTPOne:
                SetText(false, "3");
                break;
            case ShotNumber.GTPTwo:
                SetText(false, "2");
                break;
            case ShotNumber.GTPThree:
                SetText(false, "1");
                break;

        }



        //   float deltaTime = Time.unscaledDeltaTime;
        //   float interp = deltaTime / (0.5f + deltaTime);
        //   float currentFPS = 1.0f / deltaTime;
        //   fps = Mathf.Lerp(fps, currentFPS, interp);
        //   float msf = MS_PER_SEC / fps;
        //   textField.text = string.Format(DISPLAY_TEXT_FORMAT,
        //       msf.ToString(MSF_FORMAT), Mathf.RoundToInt(fps));
    }

    void SetText(bool _isATP, string numRound)
    {
        transform.GetChild(0).gameObject.SetActive(_isATP);
        transform.GetChild(1).gameObject.SetActive(!_isATP);
        if (_isATP)
        {            
            tmPro.text = string.Format(DISPLAY_TEXT_FORMAT,
                "ATP ", numRound);
        }
        else
        {
            tmPro.text = string.Format(DISPLAY_TEXT_FORMAT,
                "GTP ", numRound);
        }
    }
}
