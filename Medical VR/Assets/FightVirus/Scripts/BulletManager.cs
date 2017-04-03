using UnityEngine;
using System.Collections;
using System;

public class BulletManager : MonoBehaviour
{
    //public GameObject Bullet;
    public GameObject BlueProtein;
    public GameObject RedProtein;
    public GameObject YellowProtein;
    public GameObject GreenProtein;

    public GameObject Reticle;
    public GameObject Player;
    public bool isTriggered;
    public float Time;
    int ChangeProtein;
    void Start()
    {
        isTriggered = true;
        Time = 0;
        ChangeProtein = 0;
    }

    void Update()
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
        {
            //isTriggered = false;
            isTriggered = !isTriggered;
        }

        //Gameover stop shooting
        if (Player.GetComponent<Player>().isGameOver)
        {
            isTriggered = false;
        }
    }

    void StartShooting()
    {
        //Instantiate(Bullet, Reticle.transform.position, Reticle.transform.rotation);

        switch (ChangeProtein)
        {
            case 0:
                Instantiate(BlueProtein, Reticle.transform.position, Reticle.transform.rotation);//BlueProtein.transform.rotation);
                break;

            case 1:
                Instantiate(RedProtein, Reticle.transform.position, Reticle.transform.rotation);//RedProtein.transform.rotation);
                break;

            case 2:
                Instantiate(YellowProtein, Reticle.transform.position, Reticle.transform.rotation);//YellowProtein.transform.rotation);
                break;

            case 3:
                Instantiate(GreenProtein, Reticle.transform.position, Reticle.transform.rotation);//GreenProtein.transform.rotation);
                break;

            default:
                break;
        }

        ChangeProtein += 1;

        if (ChangeProtein >= 4)
            ChangeProtein = 0;
    }
}
