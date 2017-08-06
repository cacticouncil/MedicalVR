using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RedCellScript : MonoBehaviour
{
    public GameObject TutorialGameObject;
    public float speed;
    public GameObject virus, banner, blackCurtain;
    public AntibodySpawnerScript spawner;
    bool fade = false;
    float timer = 0;

    void Start()
    {
        if (GlobalVariables.arcadeMode == true)
        {
            blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
            GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x, 0.5f, GetComponent<BoxCollider>().size.z);
        }
    }

    void Update()
    {
        if (fade)
        {
            timer += Time.deltaTime;
            //float a = blackCurtain.GetComponent<Renderer>().material.color.a;
            //if (a < 0)
            //    a = 0;
            //blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a + (Time.deltaTime * 1.5f));
            blackCurtain.GetComponent<FadeIn>().enabled = true;
            blackCurtain.GetComponent<FadeOut>().enabled = false;
            if (timer > 1)
            {
                //For tutorial and story mode
                if (GlobalVariables.arcadeMode == false)
                {
                    //When you beat tutorial mode, go to story mode
                    if (GlobalVariables.tutorial == true)
                    {
                        VirusGameplayScript.loadCase = 1;
                        GlobalVariables.tutorial = false;
                        GlobalVariables.arcadeMode = TutorialGameObject.GetComponent<DodgeAntiBodyTutorial>().prevState;
                        SceneManager.LoadScene("DodgeAntibodies");
                    }

                    //When you beat story mode, go to next scene
                    else if (GlobalVariables.tutorial == false)
                    {
                        VirusGameplayScript.loadCase = 1;
                        SceneManager.LoadScene("VirusGameplay");
                    }
                }
            }
        }

        else
        {
            float a = blackCurtain.GetComponent<Renderer>().material.color.a;
            blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a - (Time.deltaTime * 1.5f));
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GlobalVariables.arcadeMode == true)
            {
                virus.GetComponent<MovingCamera>().WinResetPos();
                virus.GetComponent<MovingCamera>().speed+= 0.75f;
                virus.GetComponent<MovingCamera>().orgSpeed = virus.GetComponent<MovingCamera>().speed;
                spawner.GenerateObstacles();
            }
            else
            {
                fade = true;
                timer = 0;
            }
            
            //if (PlayerPrefs.GetInt("Red Cell") != 1)
            //{
            //    banner.GetComponent<BannerScript>().ShowUp();
            //    PlayerPrefs.SetInt("Red Cell", 1);
            //    SoundManager.PlaySFX("MenuEnter");
            //}
        }
    }
}
