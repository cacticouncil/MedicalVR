using UnityEngine;
using UnityEngine.UI;


public class _TShooterUIController : MonoBehaviour {

    ShotNumber currentShot;
    public GameObject player;

    private const string DISPLAY_TEXT_FORMAT = "{0} Loaded\nRounds Left : {1}";
    private const string MSF_FORMAT = "#.#";
//    private const float MS_PER_SEC = 1000f;

    private TextMesh textField;
//    private float fps = 60;

    public Camera cam;

    void Awake()
    {
        textField = GetComponent<TextMesh>();
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

        switch(currentShot)
        {
            case ShotNumber.ATPOne:
                textField.text = string.Format(DISPLAY_TEXT_FORMAT,
                "ATP ", "3");
                break;
            case ShotNumber.ATPTwo:
                textField.text = string.Format(DISPLAY_TEXT_FORMAT,
                "ATP ", "2");
                break;
            case ShotNumber.ATPThree:
                textField.text = string.Format(DISPLAY_TEXT_FORMAT,
                "ATP ", "1");
                break;
            case ShotNumber.GTPOne:
                textField.text = string.Format(DISPLAY_TEXT_FORMAT,
                "GTP ", "3");
                break;
            case ShotNumber.GTPTwo:
                textField.text = string.Format(DISPLAY_TEXT_FORMAT,
                "GTP ", "2");
                break;
            case ShotNumber.GTPThree:
                textField.text = string.Format(DISPLAY_TEXT_FORMAT,
                "GTP ", "1");
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
}
