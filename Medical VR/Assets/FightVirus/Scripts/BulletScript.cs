using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    [System.NonSerialized]
    public float LifeSpan;
    [System.NonSerialized]
    public float Speed;
    [System.NonSerialized]
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
        //Remember that the object is rotated so when it moves forward it needs to use the up axis to do so. 
        GetComponent<Rigidbody>().velocity = transform.up * Speed;
    }

    void OnTriggerEnter(Collider Virus)
    {
        if (Virus.tag == "Virus")
            Destroy(gameObject);
    }
}
