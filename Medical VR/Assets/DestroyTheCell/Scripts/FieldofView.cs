using UnityEngine;
using System.Collections;

public class FieldofView : MonoBehaviour
{
    public float ViewRadius;

    [Range(0, 360)]
    public float ViewAngle;

    public Vector3 DirfromAngle(float AngleinDegrees, bool AngleisGlobal)
    {
        if (!AngleisGlobal)
        {
            AngleinDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(AngleinDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(AngleinDegrees * Mathf.Deg2Rad));
    }
}