using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour
{

    public void P()
    {
        if (!GetComponent<Animation>().Play("Rotate"))
            Debug.Log("Animation Error");
    }
}
