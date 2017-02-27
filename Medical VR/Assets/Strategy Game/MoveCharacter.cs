using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class MoveCharacter : MonoBehaviour, IGvrGazeResponder
{
    private Vector3 startingPosition;
    public MoveCamera mainCamera;
    public float camOffset = 2.0f;

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

    public void MoveTo()
    {
        if ((transform.GetComponentInParent<StrategyCellManagerScript>().viewingStats && transform.GetComponentInParent<StrategyCellManagerScript>().selected != transform.GetComponent<StrategyCellScript>().key) || (!transform.GetComponentInParent<StrategyCellManagerScript>().viewingStats && transform.GetComponentInParent<StrategyCellManagerScript>().selected == transform.GetComponent<StrategyCellScript>().key))
        {
            //Get the direction of the player from the cell
            Vector3 heading = mainCamera.transform.position - transform.position;
            //Don't change y value
            heading.y = 0;
            //Find normalized direction
            float distance = Mathf.Max(heading.magnitude, .001f);
            Vector3 direction = heading / distance;
            if (direction.magnitude < 0.01f)
            {
                direction = new Vector3(0.0f, 0.0f, 1.0f);
            }
            //Scale it to 1.5
            direction *= 1.3f;

            Vector3 finalPos = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);

            transform.GetChild(0).transform.LookAt(finalPos);

            //This is the new target position
            mainCamera.SetDestination(finalPos);
            transform.GetComponentInParent<StrategyCellManagerScript>().SetSelected(transform.GetComponent<StrategyCellScript>().key);
             gameObject.GetComponent<StrategyCellScript>().ToggleUI(true);
            transform.GetComponentInParent<StrategyCellManagerScript>().viewingStats = true;
        }
        else if (!transform.GetComponentInParent<StrategyCellManagerScript>().viewingStats)// && transform.GetComponentInParent<StrategyCellManagerScript>().selected != transform.GetComponent<StrategyCellScript>().key)
        {
            mainCamera.SetDestination(new Vector3(transform.position.x, transform.position.y + camOffset, transform.position.z));
            //gameObject.GetComponent<StrategyCellScript>().ToggleUI(false);
            transform.GetComponentInParent<StrategyCellManagerScript>().SetSelected(transform.GetComponent<StrategyCellScript>().key);
            transform.GetComponentInParent<StrategyCellManagerScript>().viewingStats = false;
        }
    }

    public void Back()
    {
        mainCamera.SetDestination(new Vector3(transform.position.x, transform.position.y + camOffset, transform.position.z));
        //gameObject.GetComponent<StrategyCellScript>().ToggleUI(true);
        transform.GetComponentInParent<StrategyCellManagerScript>().SetSelected(transform.GetComponent<StrategyCellScript>().key);
        transform.GetComponentInParent<StrategyCellManagerScript>().viewingStats = false;
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
        MoveTo();
    }

    #endregion
}

