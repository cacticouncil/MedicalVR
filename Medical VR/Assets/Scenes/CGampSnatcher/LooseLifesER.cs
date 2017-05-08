using UnityEngine;
using System.Collections;

public class LooseLifesER : MonoBehaviour {

    public GameObject storebullets;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "CBullet")
        {

            storebullets.GetComponent<Storebullets>().score -= 50;
            Storebullets.LoseresetPos();
            Destroy(collision.gameObject);
        }
    }
}
