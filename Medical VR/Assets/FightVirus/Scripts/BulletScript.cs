using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public float LifeSpan;
    public float Speed;
    public bool PowerUp;

	void Start ()
    {
        LifeSpan = 0.0f;
        Speed = 2.5f;
	}
	
	void Update ()
    {
        LifeSpan += Time.deltaTime;
        if (LifeSpan >= 3.0f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.position += transform.up * Random.Range(-.01f, .01f);
        //transform.Rotate(RandomX, RandomY, 0);
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
