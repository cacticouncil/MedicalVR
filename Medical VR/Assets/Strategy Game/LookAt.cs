using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    public GameObject l;

    // Update is called once per frame
    void OnEnable()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(l.transform.position - transform.position));
        }
        else
        {
            transform.LookAt(l.transform);
        }
    }
}
