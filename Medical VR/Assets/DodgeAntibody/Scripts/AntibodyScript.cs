using UnityEngine;
using System.Collections;

public class AntibodyScript : MonoBehaviour
{
    public GameObject cam, Effects;
    bool reswpawn = false;

    public GameObject TutorialMode;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, cam.transform.position) < 3000)
        {
            GetComponent<Renderer>().enabled = true;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
        }
        if (reswpawn == true)
        {
            if (Effects.GetComponent<ParticleSystem>().isPlaying == false)
            {
                if (GlobalVariables.tutorial == false)
                {
                    if (cam.transform.position == cam.GetComponent<MovingCamera>().orgPos)
                    {
                        reswpawn = false;
                        cam.GetComponent<MovingCamera>().stopMoving = false;
                        cam.GetComponent<SphereCollider>().enabled = true;
                    }
                    //Cam.GetComponent<MovingCamera>().LoseresetPos();
                    //Cam.GetComponent<MovingCamera>().speed = saveSpeed;
                }
                else if (TutorialMode.GetComponent<DodgeAntiBodyTutorial>().WhiteCellHitsPlayerFirstTime == true)
                {
                    cam.GetComponent<MovingCamera>().stopMoving = false;
                    cam.GetComponent<MovingCamera>().speed = 10;
                    cam.GetComponent<SphereCollider>().enabled = true;
                    reswpawn = false;
                    TutorialMode.GetComponent<DodgeAntiBodyTutorial>().RespawnPlayer();
                }
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // other.transform.position -= other.transform.forward * 50;
            reswpawn = true;
            Effects.GetComponent<ParticleSystem>().Stop();
            Effects.GetComponent<ParticleSystem>().Play();

            other.gameObject.GetComponent<MovingCamera>().LoseResetPos();
            //For tutorial mode 
            if (GlobalVariables.tutorial)
            {
                if (TutorialMode.GetComponent<DodgeAntiBodyTutorial>().WhiteCellHitsPlayerFirstTime == false)
                {
                    TutorialMode.GetComponent<DodgeAntiBodyTutorial>().MoveStoy();
                    TutorialMode.GetComponent<DodgeAntiBodyTutorial>().WhiteCellHitsPlayerFirstTime = true;
                }

                //else if (TutorialMode.GetComponent<DodgeAntiBodyTutorial>().WhiteCellHitsPlayerFirstTime == true)
                //{
                //    TutorialMode.GetComponent<DodgeAntiBodyTutorial>().RepawnPlayer();
                //}
            }
        }
    }
}
