using UnityEngine;
using System.Collections;

public class ScrollingTexture : MonoBehaviour
{
    public float xSpeed1, ySpeed1;
    public float xSpeed2, ySpeed2;

    void Update()
    {
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(Time.time * xSpeed1, Time.time * ySpeed1));
        //GetComponent<Renderer>().material.SetTextureOffset("_Tex2", new Vector2(Time.time * xSpeed2, Time.time * ySpeed2));
    }
}
