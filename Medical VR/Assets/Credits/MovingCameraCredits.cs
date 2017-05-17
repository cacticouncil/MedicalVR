using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCameraCredits : MonoBehaviour
{
    public float speed;
    public Transform cam;
    public Renderer bloodVessel;

    private Rigidbody rig;

    // Use this for initialization
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.velocity = new Vector3(0, 0, cam.forward.z * speed);
        Vector2 bOff = bloodVessel.material.mainTextureOffset;
        bOff.y += rig.velocity.z * .001f; // same thing you were adding to mainTex
                                  // throw in a multiplier, above, if the textures aren't scaled the same
        bloodVessel.material.mainTextureOffset = bOff;
    }
}
