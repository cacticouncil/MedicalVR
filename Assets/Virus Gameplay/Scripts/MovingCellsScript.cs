using UnityEngine;
using System.Collections;

public class MovingCellsScript : MonoBehaviour
{
    public GameObject Cam;
   float speed = 5/10f;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Cam.transform.position) < 800/10f)
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
