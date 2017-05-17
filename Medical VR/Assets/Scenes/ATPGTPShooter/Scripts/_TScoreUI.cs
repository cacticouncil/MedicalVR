using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class _TScoreUI : MonoBehaviour
{
    private Camera cam;
    public GameObject gameController;

    private const string DISPLAY_TEXT_FORMAT = "Score\n{0}";
  //  private TextMesh textField;

    private TMPro.TextMeshPro tmPro;

    void Awake()
    {
    //    textField = GetComponent<TextMesh>();
        tmPro = GetComponent<TextMeshPro>();
 //       TextMeshPro
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
        // currentShot = (ShotNumber)player.GetComponent<_TPlayerController>().GetShotNumber();
     //   textField.text = string.Format(DISPLAY_TEXT_FORMAT, gameController.GetComponent<_TGameController>().score);
        tmPro.text = string.Format(DISPLAY_TEXT_FORMAT, gameController.GetComponent<_TGameController>().score);
    }
}