using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Reticle;
    public Transform BulletPos;
    public bool PowerUp;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", 0.8f, 0.8f);
        PowerUp = true;
    }

    void Spawn()
    {
        Instantiate(Bullet, Reticle.transform.position, Reticle.transform.rotation);
        if (PowerUp == true)
        {
            Instantiate(Bullet, new Vector3(Reticle.transform.position.x - .5f, 0, 0), Reticle.transform.rotation);
            Instantiate(Bullet, new Vector3(Reticle.transform.position.x + .5f, 0, 0), Reticle.transform.rotation);
        }
    }
}
