using UnityEngine;
using System.Collections;

public class BloodVesselScript : MonoBehaviour
{
    public GameObject Effects;
    public GameObject Cam;

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    if (respawn == true)
    //    {
    //        Cam.GetComponent<MovingCamera>().stopMoving = true;
    //        Cam.GetComponent<MovingCamera>().speed = -100;
    //        Cam.GetComponent<SphereCollider>().enabled = false;
    //        if (Cam.transform.position == Cam.GetComponent<MovingCamera>().orgPos)
    //        {
    //            respawn = false;
    //            Cam.GetComponent<MovingCamera>().stopMoving = false;
    //            Cam.GetComponent<MovingCamera>().speed = saveSpeed;
    //            Cam.GetComponent<SphereCollider>().enabled = true;
    //        }
    //    }
    //}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            SoundManager.PlaySFX("wall");
            other.gameObject.GetComponent<MovingCamera>().LoseResetPos();
            Effects.GetComponent<ParticleSystem>().Stop();
            Effects.GetComponent<ParticleSystem>().Play();
        }
    }
}
