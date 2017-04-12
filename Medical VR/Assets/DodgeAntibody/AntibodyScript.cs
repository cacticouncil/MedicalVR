using UnityEngine;
using System.Collections;

public class AntibodyScript : MonoBehaviour
{

    public GameObject Cam, Effects, banner;
    bool reswpawn = false;
    float saveSpeed;

    public GameObject TutorialMode;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Cam.transform.position) < 3000)
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
                reswpawn = false;
                Cam.GetComponent<MovingCamera>().LoseresetPos();
                Cam.GetComponent<MovingCamera>().speed = saveSpeed;

                if (MovingCamera.TutorialMode == true)
                    Cam.GetComponent<MovingCamera>().speed = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "virus")
        {
            other.transform.position -= other.transform.forward * 50;
            saveSpeed = other.GetComponent<MovingCamera>().speed;
            other.GetComponent<MovingCamera>().speed = 0;
            reswpawn = true;
            Effects.GetComponent<ParticleSystem>().Stop();
            Effects.GetComponent<ParticleSystem>().Play();
            if (PlayerPrefs.GetInt("White Cell") != 1)
            {
                banner.GetComponent<BannerScript>().ShowUp();
                PlayerPrefs.SetInt("White Cell", 1);
                SoundManager.PlaySFX("MenuEnter");
            }

            //For tutorial mode 
            if (MovingCamera.TutorialMode == true)
            {
                TutorialMode.GetComponent<DodgeAntiBodyTutorial>().MoveStoy();
            }
        }
    }
}
