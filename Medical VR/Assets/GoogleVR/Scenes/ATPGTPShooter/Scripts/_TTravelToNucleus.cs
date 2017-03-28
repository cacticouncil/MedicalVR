using UnityEngine;
using System.Collections;

public class _TTravelToNucleus : MonoBehaviour
{
    enum TravelPhase { run, longPath, travelToER,  destroy }
    enum nucleusChild { MainNucleus, ER, MovementPath }
    enum nucleusPathChild { Top, Front, Back, FarPoint, EnterOne, EnterTwo, EnterThree, End }
    
    private TravelPhase phase;
    private float speed = 3;

    [HideInInspector]
    public GameObject nucleus;
    [HideInInspector]
    public bool hasATP, hasGTP;

    private nucleusPathChild targetPath;
   // private GameObject nearestPath;

    private void Start()
    {
        phase = TravelPhase.run;
        hasATP = false;
        hasGTP = false;
    }

    private void FixedUpdate()
    {
        switch (phase)
        {
            case TravelPhase.longPath:
                if (TravelToPosition())
                {
                    phase = TravelPhase.travelToER;
                    targetPath = nucleusPathChild.EnterOne;
                }
                break;
            case TravelPhase.travelToER:
                if (TravelToPosition())
                {
                    TurnOffColliders();
                    if (++targetPath == nucleusPathChild.End)
                        phase = TravelPhase.destroy;
                }
                    break;
            case TravelPhase.destroy:
                Destroy(gameObject);
                break;
        }
    }
    public void StartTravel()
    {
        Vector3 farPoint = nucleus.transform.GetChild((int)nucleusChild.MovementPath).GetChild((int)nucleusPathChild.FarPoint).transform.position;
        Vector3 nearPoint = nucleus.transform.GetChild((int)nucleusChild.MovementPath).GetChild((int)nucleusPathChild.EnterOne).transform.position;
        float farDist = Vector3.Distance(transform.position, farPoint);
        float nearDist = Vector3.Distance(transform.position, nearPoint);

        if (farDist < nearDist)
        {
            targetPath = nucleusPathChild.Back;
            targetPath = GetPath(targetPath, nucleusPathChild.Front);
            targetPath = GetPath(targetPath, nucleusPathChild.Top);

            phase = TravelPhase.longPath;
        }
        else
        {
            targetPath = nucleusPathChild.EnterOne;
            phase = TravelPhase.travelToER;
        }
    }
    nucleusPathChild GetPath(nucleusPathChild curNearPath, nucleusPathChild compPath)
    {
        Vector3 curP = nucleus.transform.GetChild((int)nucleusChild.MovementPath).GetChild((int)curNearPath).transform.position;
        Vector3 comP = nucleus.transform.GetChild((int)nucleusChild.MovementPath).GetChild((int)compPath).transform.position;
        if (Vector3.Distance(transform.position, curP) < Vector3.Distance(transform.position, comP))
            return curNearPath;
        else
            return compPath;        
    }
    bool TravelToPosition()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nucleus.transform.GetChild((int)nucleusChild.MovementPath).GetChild((int)targetPath).transform.position, step);
        if (Vector3.Distance(nucleus.transform.GetChild((int)nucleusChild.MovementPath).GetChild((int)targetPath).transform.position, transform.position) < 0.1f)
            return true;
        else return false;
    }
    void TurnOffColliders()
    {
        transform.FindChild("Colliders").gameObject.SetActive(false);
    }
}