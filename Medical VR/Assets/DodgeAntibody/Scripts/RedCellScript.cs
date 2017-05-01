using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RedCellScript : MonoBehaviour
{
    public GameObject TutorialGameObject;
    // Use this for initialization
    public float speed;
    public GameObject virus, spawner, banner, blackCurtain;
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

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            timer += Time.deltaTime;
            float a = blackCurtain.GetComponent<Renderer>().material.color.a;
            if (a < 0)
                a = 0;
            blackCurtain.GetComponent<Renderer>().material.color = new Color(0, 0, 0, a + (Time.deltaTime * 1.5f));
            if (timer > 1)
            {
                //For tutorial and story mode
                if (GlobalVariables.arcadeMode == false)
                {
                    //When you beat tutorial mode, go to story mode
                    if (GlobalVariables.tutorial == true)
                    {
                        GlobalVariables.tutorial = false;
                        VirusGameplayScript.loadCase = 1;
                        SceneManager.LoadScene("DodgeAnitbodies");
                    }

                    //When you beat tutorial mode only once, go back to main menu

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
        if (other.tag == "virus")
        {
            if (GlobalVariables.arcadeMode == true)
            {
                virus.GetComponent<MovingCamera>().WinresetPos();
                virus.GetComponent<MovingCamera>().speed++;
                spawner.GetComponent<AnitbodySpawnerScript>().GenerateObstacles();

            }
            else
            {
                fade = true;
                timer = 0;
            }

            if (PlayerPrefs.GetInt("Red Cell") != 1)
            {
                banner.GetComponent<BannerScript>().ShowUp();
                PlayerPrefs.SetInt("Red Cell", 1);
                SoundManager.PlaySFX("MenuEnter");
            }
        }
    }
}
