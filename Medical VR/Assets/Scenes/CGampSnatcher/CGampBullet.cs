using UnityEngine;
using System.Collections;
using System;

public class CGampBullet : MonoBehaviour
{
    public GameObject Trial;
    
   
    public GameObject cameras;
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
            transform.position = Vector3.MoveTowards(transform.position, cameras.transform.position + new Vector3(0,0,1), 0.3f);

            if (transform.position == cameras.transform.position + new Vector3(0, 0, 1))
            {
                Destroy(gameObject);
                Storebullets.amount -= 1;
                Storebullets.bulletamount += 1;
            }
        }

        if(cameras.GetComponent<Storebullets>().finish == true)
        {
            Destroy(gameObject);
            Storebullets.amount -= 1;
        }




    }
    public void HandleTimeInput()
    {
        isgrabbed = true;
    }
}
