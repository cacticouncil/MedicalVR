using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public float LifeSpan;
    public float Speed;
    public bool PowerUp;
    public int Damage;
	void Start ()
    {
        LifeSpan = 0.0f;
        Speed = 6.0f;
        Damage = 10;
	}
	
	void Update ()
    {
        LifeSpan += Time.deltaTime;
        if (LifeSpan >= 2.2f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.position += transform.up * Random.Range(-.01f, .01f);
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
    }

    void OnTriggerEnter(Collider Virus)
    {
        if (Virus.tag == "Virus")
        {
            Destroy(gameObject);
        }
    }
}
