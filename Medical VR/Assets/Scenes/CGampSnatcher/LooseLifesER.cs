using UnityEngine;
using System.Collections;

public class LooseLifesER : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "CBullet")
        {

            Storebullets.score -= 50;
            Storebullets.LoseresetPos();
            Destroy(collision.gameObject);
        }
    }
}
