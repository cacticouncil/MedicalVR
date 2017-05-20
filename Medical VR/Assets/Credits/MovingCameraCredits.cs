using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCameraCredits : MonoBehaviour
{
    public float speed;
    public Renderer bloodVessel;
    public float texMod = .001f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float zMovement = speed;
        transform.position += new Vector3(0, 0, zMovement);
        Vector2 bOff = bloodVessel.material.mainTextureOffset;
        bOff.y += zMovement * texMod; // same thing you were adding to mainTex
                                  // throw in a multiplier, above, if the textures aren't scaled the same
        bloodVessel.material.mainTextureOffset = bOff;
    }
}
