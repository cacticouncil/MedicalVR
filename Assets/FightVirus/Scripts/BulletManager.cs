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

    private bool last = false;

    void Start()
    {
        isTriggered = true;
        Time = 0;
        ChangeProtein = 0;

        if (GlobalVariables.tutorial == false)
            isTriggered = true;
        
        else if (GlobalVariables.tutorial == true)
            isTriggered = false;
    }

    void Update()
    {
        bool held = Input.GetButton("Fire1");
        if (GlobalVariables.tutorial == false)
        {
            //Bullet Delay
            Time += UnityEngine.Time.deltaTime;
            if (isTriggered == true && Time >= .5f)
            {
                Time = 0.0f;
                StartShooting();
            }

            //Checking for button input
            if (held && !last)
                isTriggered = !isTriggered;

            //Gameover stop shooting
            if (FightVirusPlayer.GetComponent<Player>().isGameOver)
                isTriggered = false;
        }

        //For tutorial mode dont have them shoot unless directed to
        else if (CanIShoot == true)
        {
            Time += UnityEngine.Time.deltaTime;
            if (isTriggered == true && Time >= .5f)
            {
                Time = 0.0f;
                StartShooting();
            }

            if (held && !last)
                isTriggered = !isTriggered;
        }
        last = held;
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

        SoundManager.PlaySFX("StrategyGame/link3");
        ChangeProtein += 1;

        if (ChangeProtein >= 4)
            ChangeProtein = 0;
    }
}
