using UnityEngine;
using System.Collections;

public class LooseLifesER : MonoBehaviour {

    public GameObject storebullets;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "CBullet")
        {
            if (SoundManager.IsJordanPlaying("341633__padsterpat__karplus-strong") == false)
                SoundManager.PlayJordanVoice("341633__padsterpat__karplus-strong");
            
            Storebullets.LoseresetPos();
            Destroy(collision.gameObject);
        }
    }
}
