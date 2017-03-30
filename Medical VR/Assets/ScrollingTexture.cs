using UnityEngine;
using System.Collections;

public class ScrollingTexture : MonoBehaviour
{
    public float xSpeed, ySpeed;

    void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset += new Vector2(Time.deltaTime * xSpeed, Time.deltaTime * ySpeed);
    }
}
