using UnityEngine;
using System.Collections;

public class MovingCellsScript : MonoBehaviour
{
    public GameObject Cam;
    public float speed = .1f;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Cam.transform.position) < 800)
        {
            if (GetComponent<Renderer>())
            {
                GetComponent<Renderer>().enabled = true;
            }
        }
        else
        {
            if (GetComponent<Renderer>())
            {
                GetComponent<Renderer>().enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * speed;
    }
}
