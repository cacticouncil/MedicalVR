using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetText : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<TMPro.TextMeshPro>().text = transform.parent.position.ToString();
    }
}
