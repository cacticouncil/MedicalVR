using UnityEngine;
using System.Collections;
using System;

public class CGampBullet : MonoBehaviour, TimedInputHandler {

    public GameObject Trial;

    void Start()
    {
        Instantiate(Trial, transform.position, transform.rotation, transform);
        Trial.GetComponent<ParticleSystem>().Play();
    }
    public void HandleTimeInput()
    {
        Destroy(gameObject);
       Storebullets.bulletamount += 1;
    }
}
