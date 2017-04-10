using UnityEngine;
using System.Collections;
using System;

public class BulletManager : MonoBehaviour
{
    public GameObject BlueProtein;
    public GameObject RedProtein;
    public GameObject YellowProtein;
    public GameObject GreenProtein;

    public GameObject Reticle;
    public GameObject FightVirusPlayer;
    public bool isTriggered;
    public float Time;
    int ChangeProtein;
    public bool CanIShoot = false;

    void Start()
    {
        isTriggered = true;
        Time = 0;
        ChangeProtein = 0;

        if (Player.TutorialMode == false)
            isTriggered = true;
        
        else if (Player.TutorialMode == true)
            isTriggered = false;
    }

    void Update()
    {
        if (Player.TutorialMode == false)
        {
            //Bullet Delay
            Time += UnityEngine.Time.deltaTime;
            if (isTriggered == true && Time >= .2f)
            {
                Time = 0.0f;
                StartShooting();
            }

            //Checking for button input
            if (GvrViewer.Instance.Triggered)
                isTriggered = !isTriggered;

            //Gameover stop shooting
            if (FightVirusPlayer.GetComponent<Player>().isGameOver)
                isTriggered = false;
        }

        //For tutorial mode dont have them shoot unless directed to
        else if (Player.TutorialMode == true && CanIShoot == true)
        {
            Time += UnityEngine.Time.deltaTime;
            if (isTriggered == true && Time >= .2f)
            {
                Time = 0.0f;
                StartShooting();
            }

            if (GvrViewer.Instance.Triggered)
                isTriggered = !isTriggered;
        }
    }

    void StartShooting()
    {
        switch (ChangeProtein)
        {
            case 0:
                Instantiate(BlueProtein, Reticle.transform.position, Reticle.transform.rotation);
                break;

            case 1:
                Instantiate(RedProtein, Reticle.transform.position, Reticle.transform.rotation);
                break;

            case 2:
                Instantiate(YellowProtein, Reticle.transform.position, Reticle.transform.rotation);
                break;

            case 3:
                Instantiate(GreenProtein, Reticle.transform.position, Reticle.transform.rotation);
                break;

            default:
                break;
        }

        ChangeProtein += 1;

        if (ChangeProtein >= 4)
            ChangeProtein = 0;
    }
}
