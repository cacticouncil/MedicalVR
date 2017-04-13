using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class MoveCharacter : MonoBehaviour
{
    private MoveCamera mainCamera;
    private float camOffset = 5.0f;
    private float scaledDistance = 1.3f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.GetComponent<MoveCamera>();
        }
    }

    public void MoveTo()
    {
        if ((transform.parent.GetComponent<StrategyCellManagerScript>().viewingStats && transform.parent.GetComponent<StrategyCellManagerScript>().selected != transform.GetComponent<StrategyCellScript>().key) || (!transform.parent.GetComponent<StrategyCellManagerScript>().viewingStats && transform.parent.GetComponent<StrategyCellManagerScript>().selected == transform.GetComponent<StrategyCellScript>().key))
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
            direction *= scaledDistance;

            Vector3 finalPos = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);

            transform.GetChild(0).transform.LookAt(finalPos);

            //This is the new target position
            mainCamera.SetDestination(finalPos);
            transform.parent.GetComponent<StrategyCellManagerScript>().SetSelected(transform.GetComponent<StrategyCellScript>().key);
            gameObject.GetComponent<StrategyCellScript>().ToggleUI(true);
            transform.parent.GetComponent<StrategyCellManagerScript>().viewingStats = true;
        }
        else if (!transform.parent.GetComponent<StrategyCellManagerScript>().viewingStats)
        {
            mainCamera.SetDestination(new Vector3(transform.position.x, transform.position.y + camOffset, transform.position.z));
            transform.parent.GetComponent<StrategyCellManagerScript>().SetSelected(transform.GetComponent<StrategyCellScript>().key);
            transform.parent.GetComponent<StrategyCellManagerScript>().viewingStats = false;
        }
    }

    public void Back()
    {
        mainCamera.SetDestination(new Vector3(transform.position.x, transform.position.y + camOffset, transform.position.z));
        transform.parent.GetComponent<StrategyCellManagerScript>().SetSelected(transform.GetComponent<StrategyCellScript>().key);
        transform.parent.GetComponent<StrategyCellManagerScript>().viewingStats = false;
    }
}

