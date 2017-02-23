using UnityEngine;
using System.Collections;
using System;

public class BulletManager : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Reticle;
    public Transform BulletPos;
    public bool PowerUp;
    public bool isTriggered;
    public float time;

    void Start()
    {
        isTriggered = true;
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (isTriggered == true && time >= 1.5f)
        {
            time = 0.0f;
            StartShooting();
        }

        //Checking for button input
        if (GvrViewer.Instance.Triggered)
        {
            isTriggered = !isTriggered;
        }
    }

    void StartShooting()
    {
        Instantiate(Bullet, Reticle.transform.position, Reticle.transform.rotation);
    }

    public void Toggle()
    {
        isTriggered = !isTriggered;
    }
}
