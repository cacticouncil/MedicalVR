using UnityEngine;
using System.Collections;

public class WriteQuaternion : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.rotation.ToString());
    }
}
