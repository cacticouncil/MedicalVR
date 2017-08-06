using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    public Transform l;

    // Update is called once per frame
    void OnEnable()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(l.position - transform.position));
        }
        else
        {
            transform.LookAt(l);
        }
    }
}
