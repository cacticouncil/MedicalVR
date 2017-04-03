using UnityEngine;
using System.Collections;

public class _TRaycastTarget : MonoBehaviour
{
    public GameObject gun;
    public GameObject oldReticle;
    public GameObject newReticle;


    private Transform targetTransform;
    private Ray theRay;

    public Material mat;
    private LineRenderer lineRenderer;

    int DebugStuff;

    // Use this for initialization
    void Start()
    {
        if (!gun)
            Debug.Log("failed to load Gun");
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetVertexCount(20);
        lineRenderer.SetColors(Color.green, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        theRay.origin = gun.transform.position;
        theRay.direction = gun.transform.TransformDirection(Vector3.forward);
        PlotTrajectory(gun.transform.position, gun.transform.forward * 10, .2f, 1.0f);


    }

    public Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time)
    {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    public void PlotTrajectory(Vector3 start, Vector3 startVelocity, float timestep, float maxTime)
    {
        Vector3 prev = start;
        int i = 1;
        for (;; i++)
        {
            if (i > 21)
                break;
            float t = timestep * i;
            if (t > maxTime) break;
            Vector3 pos = PlotTrajectoryAtTime(start, startVelocity, t);
            if (Physics.Linecast(prev, pos)) break;
        //    Debug.DrawLine(prev, pos, Color.yellow);
            lineRenderer.SetPosition(i, prev);
            //lineRenderer.SetPosition(i+1, start);

            prev = pos;
        }
        for(;i < 20; i++)
        {
            lineRenderer.SetPosition(i, prev);
        }
    }

    private void FixedUpdate()
    {
        PlotTrajectory(gun.transform.position, gun.transform.forward * 10, .2f, 1.0f);

    }

    private void LateUpdate()
    {
        PlotTrajectory(gun.transform.position, gun.transform.forward * 10, .2f, 1.0f);

    }
}
