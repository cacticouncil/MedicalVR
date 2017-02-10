using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public float LifeSpan;
    public float Speed;
	// Use this for initialization
	void Start ()
    {
        LifeSpan = 0.0f;
        Speed = 10.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        LifeSpan += Time.deltaTime;
        if (LifeSpan >= 2.0f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
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
