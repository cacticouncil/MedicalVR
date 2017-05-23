using UnityEngine;
using System.Collections;
using System;

public class CGampBullet : MonoBehaviour
{
    public GameObject Trial;
    
   
    public GameObject cameras;
    bool isgrabbed = false;
    float timer = 0;

    void Start()
    {
        Instantiate(Trial, transform.position, transform.rotation, transform);
        Trial.GetComponent<ParticleSystem>().Play();
    }
    private void Update()
    {
        timer += Time.deltaTime;
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

        if(cameras.GetComponent<Storebullets>().finish == true || timer > 30)
        {
            Destroy(gameObject);
            Storebullets.amount -= 1;
        }




    }
    public void HandleTimeInput()
    {
        if (SoundManager.IsJordanPlaying("50565__broumbroum__sf3-sfx-menu-validate") == false)
            SoundManager.PlayJordanVoice("50565__broumbroum__sf3-sfx-menu-validate");
        isgrabbed = true;
    }
}
