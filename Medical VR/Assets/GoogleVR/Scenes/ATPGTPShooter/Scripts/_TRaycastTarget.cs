using UnityEngine;
using System.Collections;

public class _TRaycastTarget : MonoBehaviour
{
    public GameObject gun;
    public GameObject LineSegments;
    public GameObject GVRReticle;
    public float LineSeparation;

    private Transform targetTransform;
    private bool rayOn;

    int DebugStuff;

    void Start()
    {
        if (!gun)
            Debug.Log("failed to load Gun");
    }

    //    void Update()
    //    {
    //       RaycastHit hit = new RaycastHit();
    //       theRay.origin = gun.transform.position;
    //       theRay.direction = gun.transform.TransformDirection(Vector3.forward);
    //
    //       if (Physics.Raycast(theRay, out hit) && )
    //    }

    public Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time)
    {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    public void PlotTrajectory(Vector3 start, Vector3 startVelocity, float timestep, float maxTime)
    {
        Vector3 prev = Vector3.zero;
        int i = 0;
        if (rayOn)
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
        }
    }

    private void FixedUpdate()
    {
        PlotTrajectory(gun.transform.position, gun.transform.forward * 10, .2f, 1.0f);

        RaycastHit hit;

        Physics.Raycast(gun.transform.position, gun.transform.TransformDirection(Vector3.forward), out hit);

        if (hit.transform.CompareTag("Finish"))
            rayOn = false;
        else
            rayOn = true;
        GVRReticle.SetActive(!rayOn);
    }
}