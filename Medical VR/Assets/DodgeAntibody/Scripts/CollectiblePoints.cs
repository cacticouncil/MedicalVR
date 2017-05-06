using UnityEngine;
using System.Collections;

public class CollectiblePoints : MonoBehaviour
{
    public int score;
    public GameObject Cam, pointEffect;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Cam.transform.position) < 3000)
        {
            GetComponent<Renderer>().enabled = true;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            pointEffect.GetComponent<ParticleSystem>().Stop();
            other.GetComponent<MovingCamera>().score += score;
            pointEffect.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
    }
}
