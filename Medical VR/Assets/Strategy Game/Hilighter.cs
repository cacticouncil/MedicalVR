using UnityEngine;
using System.Collections;

public class Hilighter : MonoBehaviour {

    public Color baseColor;
    void Start()
    {
        baseColor = GetComponent<Renderer>().material.color;
    }

    public void SetGazedAt(bool gazedAt)
    {
        if (gazedAt)
        {
            GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r * .4f, GetComponent<Renderer>().material.color.g * .4f, GetComponent<Renderer>().material.color.b * .4f);
        }
        else
        {
            GetComponent<Renderer>().material.color = baseColor;
        }
    }
}
