using UnityEngine;
using System.Collections;
using System;

public class BulletManager : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Reticle;
    public GameObject Player;
    public bool isTriggered;
    public float Time;

    void Start()
    {
        isTriggered = true;
        Time = 0;
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
        Instantiate(Bullet, Reticle.transform.position, Reticle.transform.rotation);
    }
}
