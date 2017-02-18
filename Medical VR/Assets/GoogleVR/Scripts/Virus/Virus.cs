using UnityEngine;
using System.Collections;

public class Virus : MonoBehaviour
{
    public GameObject cellManager;
    public GameObject player;
    public float speed = .7f;
    Vector3 RandomPos;
    void Start()
    {
        RandomPos = Random.onUnitSphere;
        RandomPos *= 20.0f;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, RandomPos, speed);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
