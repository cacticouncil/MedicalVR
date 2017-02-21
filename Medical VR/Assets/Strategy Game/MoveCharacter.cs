using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class MoveCharacter : MonoBehaviour, IGvrGazeResponder
{
    private Vector3 startingPosition;
    public MoveCamera mainCamera;

    void Start()
    {
        startingPosition = transform.localPosition;
        if (mainCamera == null)
        {
            mainCamera = Camera.main.GetComponent<MoveCamera>();
        }
    }

    void LateUpdate()
    {
        GvrViewer.Instance.UpdateState();
        if (GvrViewer.Instance.BackButtonPressed)
        {
            Application.Quit();
        }
    }

    public void Reset()
    {
        transform.localPosition = startingPosition;
    }

    public void ToggleVRMode()
    {
        GvrViewer.Instance.VRModeEnabled = !GvrViewer.Instance.VRModeEnabled;
    }

    public void ToggleDistortionCorrection()
    {
        GvrViewer.Instance.DistortionCorrectionEnabled =
          !GvrViewer.Instance.DistortionCorrectionEnabled;
    }

#if !UNITY_HAS_GOOGLEVR || UNITY_EDITOR
    public void ToggleDirectRender()
    {
        GvrViewer.Controller.directRender = !GvrViewer.Controller.directRender;
    }
#endif  //  !UNITY_HAS_GOOGLEVR || UNITY_EDITOR

    public void MoveChar()
    {
        mainCamera.SetDestination(transform.position);
        gameObject.GetComponent<StrategyCellScript>().ToggleUI(true);
    }

    #region IGvrGazeResponder implementation

    //Called when the user is looking on a GameObject with this script,
    // as long as it is set to an appropriate layer (see GvrGaze).
    public void OnGazeEnter()
    {
    }

    //Called when the user stops looking on the GameObject, after OnGazeEnter
    // was already called.
    public void OnGazeExit()
    {
    }

    //Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
    public void OnGazeTrigger()
    {
        MoveChar();
    }

    #endregion
}

