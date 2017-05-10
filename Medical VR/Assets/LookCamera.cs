using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    public Transform target;

    // Use this for initialization
    void OnEnable()
    {
        StartCoroutine(Rotate());
    }

    public IEnumerator Rotate()
    {
        yield return 0;
        Vector3 angles = transform.GetChild(0).localEulerAngles;
        Quaternion m_parentToChild = Quaternion.Euler(-angles.x, -angles.y, -angles.z);
        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        Vector3 euler = (lookRotation * m_parentToChild).eulerAngles;
        euler.x = euler.z = 0.0f;
        transform.rotation = Quaternion.Euler(euler);
        enabled = false;
    }
}
