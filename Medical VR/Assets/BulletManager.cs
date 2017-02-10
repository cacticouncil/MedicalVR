using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Reticle;
    public Transform BulletPos;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", 0.2f, 0.2f);
    }

    void Spawn()
    {
        Instantiate(Bullet, Reticle.transform.position, Reticle.transform.rotation);
    }
}
