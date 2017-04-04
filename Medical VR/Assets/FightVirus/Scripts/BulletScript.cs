using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public float LifeSpan;
    public float Speed;
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
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        //GetComponent<Rigidbody>().velocity += transform.forward * Random.Range(-.1f, .1f) + transform.up * Random.Range(-.1f, .1f);
        //GetComponent<Rigidbody>().velocity += transform.up * Random.Range(-.1f, .1f);

        //Remember that the object is rotated so when it moves forward it needs to use the up axis to do so. 
        GetComponent<Rigidbody>().velocity = transform.up * Speed;
    }

    void OnTriggerEnter(Collider Virus)
    {
        if (Virus.tag == "Virus")
            Destroy(gameObject);
    }
}
