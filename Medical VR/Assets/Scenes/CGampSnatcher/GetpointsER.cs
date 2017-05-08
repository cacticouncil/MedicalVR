using UnityEngine;
using System.Collections;

public class GetpointsER : MonoBehaviour
{

    bool ithit = false;
    public GameObject storebullets;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (ithit == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 20, transform.position.z), 0.1f);

            if (transform.position == new Vector3(transform.position.x, transform.position.y + 20, transform.position.z))
            {

                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "CBullet")
        {
            if (ithit == true)
                return;
            storebullets.GetComponent<Storebullets>().AddToScore(25);
            Storebullets.numberofstingsdone += 1;
            ithit = true;
            Destroy(collision.gameObject);
        }
    }
}
