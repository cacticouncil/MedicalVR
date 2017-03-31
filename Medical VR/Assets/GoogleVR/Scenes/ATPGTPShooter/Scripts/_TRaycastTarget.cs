using UnityEngine;
using System.Collections;

public class _TRaycastTarget : MonoBehaviour
{
    public GameObject gun;
    public GameObject oldReticle;
    public GameObject newReticle;


    private Transform targetTransform;
    private Ray theRay;

   

    int DebugStuff;

    // Use this for initialization
    void Start()
    {
        if (!gun)
            Debug.Log("failed to load Gun");
        //        if (!oldReticle)
        //            Debug.Log("failed to load Reticle");
    }

    // Update is called once per frame
    void Update()
    {
        theRay.origin = gun.transform.position;
        theRay.direction = gun.transform.TransformDirection(Vector3.forward);

        RaycastHit info;
     //   gun.transform.r
        PlotTrajectory(gun.transform.position, transform.forward * 10, .2f, 1.0f);
    //    if (Physics.Raycast(theRay, out info))
    //    {
    //        if (info.collider.CompareTag("Target"))
    //        {
    //            print("There is a virus in front of the object!");
    //        }
    //    }
    }

    public Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time)
    {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    public void PlotTrajectory(Vector3 start, Vector3 startVelocity, float timestep, float maxTime)
    {
        Vector3 prev = start;
        for (int i = 1; ; i++)
        {
            float t = timestep * i;
            if (t > maxTime) break;
            Vector3 pos = PlotTrajectoryAtTime(start, startVelocity, t);
            if (Physics.Linecast(prev, pos)) break;
            Debug.DrawLine(prev, pos, Color.red);
            prev = pos;
        }
    }
}
