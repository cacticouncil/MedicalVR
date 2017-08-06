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
        Invoke("KillMe", 30);
    }

    private void FixedUpdate()
    {
        if (isgrabbed == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, cameras.transform.position + new Vector3(0, 0, 1), 0.3f);
            if (transform.position == cameras.transform.position + new Vector3(0, 0, 1))
            {
                Storebullets.amount -= 1;
                Storebullets.bulletamount += 1;
                Destroy(gameObject);
            }
        }
    }

    private void KillMe()
    {
        if (!isgrabbed)
        {
            Storebullets.amount -= 1;
            Destroy(gameObject);
        }
    }

    public void HandleTimeInput()
    {
        SoundManager.PlayJordanVoice("50565__broumbroum__sf3-sfx-menu-validate");
        isgrabbed = true;
        GetComponent<Collider>().enabled = false;
    }
}
