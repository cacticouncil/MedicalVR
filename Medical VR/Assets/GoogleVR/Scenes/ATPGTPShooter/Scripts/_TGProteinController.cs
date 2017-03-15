using UnityEngine;
using System.Collections;

public class _TGProteinController : MonoBehaviour
{
    public Transform target;

    public float speed = 10;

    public void SetTarget(Transform _target)
    {
        target = _target;
        if (!target)
            Debug.Log("Target not Set Successfully.");
    }

    private void Update()
    {
        if (target)
            MoveToTarget();
    }

    void MoveToTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
