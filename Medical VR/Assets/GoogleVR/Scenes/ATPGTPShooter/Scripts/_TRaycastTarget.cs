using UnityEngine;
using System.Collections;

public class _TRaycastTarget : MonoBehaviour
{
    public GameObject gun;
    public GameObject LineSegments;
    public float LineSeparation;
    //    public GameObject oldReticle;
    //    public GameObject newReticle;

    private Transform targetTransform;
    private Ray theRay;

    //    private LineRenderer lineRenderer;

    int DebugStuff;

    // Use this for initialization
    void Start()
    {
        if (!gun)
            Debug.Log("failed to load Gun");
    }

    // Update is called once per frame
    void Update()
    {
        theRay.origin = gun.transform.position;
        theRay.direction = gun.transform.TransformDirection(Vector3.forward);
        //PlotTrajectory(gun.transform.position, gun.transform.forward * 10, LineSeparation, 2.0f);
        //PlotTrajectory(gun.transform.position, gun.transform.forward * 10, .2f, 1.0f);

    }

    public Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time)
    {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    public void PlotTrajectory(Vector3 start, Vector3 startVelocity, float timestep, float maxTime)
    {
        Vector3 prev = Vector3.zero;
        int i = 0;
        for (; ; i++)
        {
            if (i > 19)
                break;
            float t = timestep * i * 0.5f;
            if (t > maxTime) break;
            Vector3 pos = PlotTrajectoryAtTime(start, startVelocity, t);
            if (Physics.Linecast(prev, pos)) break;

            LineSegments.transform.GetChild(i).transform.position = pos;
            LineSegments.transform.GetChild(i).transform.LookAt(prev);

            prev = pos;
        }
        for (; i < 20; i++)
        {
            LineSegments.transform.GetChild(i).transform.position = Vector3.zero;
            //            lineRenderer.SetPosition(i, prev);
        }
    }


    private void FixedUpdate()
    {
        PlotTrajectory(gun.transform.position, gun.transform.forward * 10, .2f, 1.0f);

    }
}
