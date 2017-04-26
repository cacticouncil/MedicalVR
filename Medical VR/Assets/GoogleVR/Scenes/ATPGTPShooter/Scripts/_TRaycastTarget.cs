using UnityEngine;
using System.Collections;

public class _TRaycastTarget : MonoBehaviour
{
    public GameObject gameController;
    public GameObject gun;
    public GameObject LineSegments;
    public GameObject GVRReticle;
    public float LineSeparation;
    [HideInInspector]
    public bool hasWon;

    private Transform targetTransform;
    private bool rayOn;

    int DebugStuff;

    void Start()
    {
        hasWon = false;
        if (!gun)
            Debug.Log("failed to load Gun");
    }

    

    public Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time)
    {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    public void PlotTrajectory(Vector3 start, Vector3 startVelocity, float timestep, float maxTime)
    {
        int LineSegmentCount = LineSegments.transform.childCount;
        Vector3 prev = Vector3.zero;
        int i = 0;
        if (rayOn)
            while (i < LineSegmentCount)
            {                
                LineSegments.transform.GetChild(i).gameObject.SetActive(true);
                float t = timestep * i * 0.5f;
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
        PlotTrajectory(gun.transform.position, gun.transform.forward * 10, .2f, 1.0f);

        RaycastHit hit;

        Physics.Raycast(gun.transform.position, gun.transform.TransformDirection(Vector3.forward), out hit);

        if (hit.transform.CompareTag("Finish") || hasWon)
            rayOn = false;
        else
            rayOn = true;
        if(GVRReticle)
            GVRReticle.SetActive(!rayOn);
    }
}