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
        Speed = 3.2f;
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
