using UnityEngine;
using System.Collections;

public class _TRaycastTarget : MonoBehaviour
{
    public GameObject gameController;
    public GameObject gun;
    public GameObject LineSegments;
    public GameObject GVRReticle;
    public float LineSeparation;
    public bool modifyReticle;
    [HideInInspector]
    public bool hasWon;

    private Transform targetTransform;
    private bool rayOn;

    float movementOffset;

    int DebugStuff;
    private bool isInit = false;

    void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        if (isInit)
            return;
        isInit = true;
        hasWon = false;
        if (!gun)
            Debug.Log("failed to load Gun");
        movementOffset = 0;
    }



    public Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time)
    {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    public void PlotTrajectory(Vector3 start, Vector3 startVelocity, float timestep, float maxTime)
    {
        int LineSegmentCount = LineSegments.transform.childCount;

        Vector3 prev = gun.transform.position;
        int i = 0;
        if (rayOn)
            while (i < LineSegmentCount)
            {
                LineSegments.transform.GetChild(i).gameObject.SetActive(true);
                float t = timestep * i * 0.5f + movementOffset;
                if (t > maxTime) break;
                Vector3 pos = PlotTrajectoryAtTime(start, startVelocity, t);

                if (Physics.Linecast(prev, pos)) break;

                LineSegments.transform.GetChild(i).transform.position = pos;

                LineSegments.transform.GetChild(i).transform.LookAt(prev);

                prev = pos;
                ++i;
            }
        for (; i < LineSegmentCount; i++)
        {
            //    LineSegments.transform.GetChild(i).transform.position = Vector3.zero;
            LineSegments.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        movementOffset += Time.fixedDeltaTime * .05f;
        if (movementOffset >= .1f)
            movementOffset = 0;
        PlotTrajectory(gun.transform.position, gun.transform.forward * 10, .2f, 1.0f);


        RaycastHit hit;
        if (gun)
        {
            Physics.Raycast(gun.transform.position, gun.transform.TransformDirection(Vector3.forward), out hit);

            if ((hit.collider && hit.transform.CompareTag("Finish")) || hasWon)
                rayOn = false;
            else
                rayOn = true;
        }
        else
            rayOn = false;
        if (modifyReticle)
            if (GVRReticle)
                GVRReticle.SetActive(!rayOn);

    }
}