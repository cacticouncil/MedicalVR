using UnityEngine;
using System.Collections;

public class BloodVesselScript : MonoBehaviour {

    public GameObject enter;
    public GameObject exit;
    public GameObject Effects;
    public GameObject Cam;
    bool respawn = false;
    float saveSpeed = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(respawn == true)
        {
            Cam.GetComponent<MovingCamera>().stopMoving = true;
            Cam.GetComponent<MovingCamera>().speed = -100;
            Cam.GetComponent<SphereCollider>().enabled = false;
            if (Cam.transform.position == Cam.GetComponent<MovingCamera>().originPos)
            {
                respawn = false;
                Cam.GetComponent<MovingCamera>().stopMoving = false;
                Cam.GetComponent<MovingCamera>().speed = saveSpeed;
                Cam.GetComponent<SphereCollider>().enabled = true;
            }
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "virus")
        {
            if (enter.GetComponent<VesselOpening>().shouldCollide == true && exit.GetComponent<VesselOpening>().shouldCollide == true)
            {
                saveSpeed = Cam.GetComponent<MovingCamera>().speed;
                respawn = true;
                other.GetComponent<MovingCamera>().LoseresetPos();
                Effects.GetComponent<ParticleSystem>().Stop();
                Effects.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
