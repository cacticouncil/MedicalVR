using UnityEngine;
using System.Collections;

public class _TPlayerController : MonoBehaviour
{
    public GameObject Camera;

    public float fireRate;
    public GameObject shot;
    public Transform shotSpawn;

    private float nextFire;
    //private AudioSource audioSource;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((GvrViewer.Instance.Triggered || Input.GetButton("Fire1")) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //audioSource.Play();
        }
    }
    private void FixedUpdate()
    {
        gameObject.transform.rotation = Camera.transform.rotation;
    }
}