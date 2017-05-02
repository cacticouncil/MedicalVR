using UnityEngine;
using System.Collections;
using System;

public class CGampBullet : MonoBehaviour, TimedInputHandler {

    public GameObject Trial;
    bool isgrabbed = false;

    void Start()
    {
        Instantiate(Trial, transform.position, transform.rotation, transform);
        Trial.GetComponent<ParticleSystem>().Play();
    }

    private void FixedUpdate()
    {
        if(isgrabbed == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 5, 2), 0.1f);

            if (transform.position == new Vector3(0, 5, 2))
            {
                Destroy(gameObject);
                Storebullets.bulletamount += 1;
            }
        }
        
       


    }
    public void HandleTimeInput()
    {
        isgrabbed = true;
    }
}
