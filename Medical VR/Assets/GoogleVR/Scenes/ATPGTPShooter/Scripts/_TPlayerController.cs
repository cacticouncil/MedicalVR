using UnityEngine;
using System.Collections;

public class _TPlayerController : MonoBehaviour
{
    enum ShotNumber { ATPOne, ATPTwo, ATPThree, GTPOne, GTPTwo, GTPThree, reset };
    private ShotNumber currentRound;

    public GameObject Camera;

    public float fireRate;
    public GameObject[] shot;
    public Transform shotSpawn;
    public int shotInClip;

    private float nextFire;
    private bool isATP;
    //private AudioSource audioSource;

    void Start()
    {
        currentRound = ShotNumber.ATPOne;
        isATP = true;
        //audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool bPressed = Input.GetButtonDown("Fire1");
   //     bool bHeld = Input.GetButton("Fire1");
   //     bool bUp = Input.GetButtonUp("Fire1");

       
        
        if (bPressed && Time.time > nextFire)
        {
            if (isATP)
                shootATP();
            else
                shootGTP();

            if (++currentRound == ShotNumber.reset)
                currentRound = ShotNumber.ATPOne;
            if (currentRound == ShotNumber.GTPOne || currentRound == ShotNumber.ATPOne)
                isATP = !isATP;

        }
    }
    private void FixedUpdate()
    {
        gameObject.transform.rotation = Camera.transform.rotation;
    }
    private void shootATP()
    {
        if (shot[0])
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot[0], shotSpawn.position, shotSpawn.rotation);
        }
    }
    private void shootGTP()
    {
        if (shot[1])
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot[1], shotSpawn.position, shotSpawn.rotation);
        }
    }
}