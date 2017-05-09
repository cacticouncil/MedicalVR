using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    public Transform target;

    // Use this for initialization
    void Start()
    {
        //transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);

        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        yield return 0;
        Vector3 angles = transform.GetChild(0).localEulerAngles;
        Quaternion m_parentToChild = Quaternion.Euler(0.0f, -angles.y, 0.0f);
        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = lookRotation * m_parentToChild;
    }
}
