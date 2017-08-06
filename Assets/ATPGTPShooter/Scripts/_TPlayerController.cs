using UnityEngine;
using System.Collections;


enum ShotNumber { ATPOne, ATPTwo, ATPThree, GTPOne, GTPTwo, GTPThree, reset };

public class _TPlayerController : MonoBehaviour
{
    private ShotNumber currentRound;
    public AudioClip[] shotSounds;

    public GameObject Camera;

    public float fireRate;
    public GameObject[] shot;
    public Transform shotSpawn;
    public int shotInClip;

    private float nextFire;
    private bool isATP;
    private AudioSource source;
    bool isActive = false;

    bool isInit = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void Initialize()
    {
        if (isInit)
            return;
        isInit = true;

        currentRound = ShotNumber.ATPOne;
        isATP = true;
    }

    public void SetActiveShooting(bool setActive)
    {
        isActive = setActive;
    }

    void Update()
    {
        bool bPressed = Input.GetButtonDown("Fire1");

        if (bPressed && Time.time > nextFire)
        {
            SetFireMode();
        }
    }
    void SetFireMode()
    {
        if (!isActive)
            return;

        if (isATP)
            Shoot(0);
        else
            Shoot(1);

        if (++currentRound == ShotNumber.reset)
            currentRound = ShotNumber.ATPOne;
        if (currentRound == ShotNumber.GTPOne || currentRound == ShotNumber.ATPOne)
        {
            isATP = !isATP;
            nextFire += fireRate * 3;
        }
    }
    private void FixedUpdate()
    {
        gameObject.transform.rotation = Camera.transform.rotation;
    }
    private void Shoot(int i)
    {
        if (shot[i])
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot[i], shotSpawn.position, shotSpawn.rotation);

            if (shotSounds.Length > 0)
            {
                int randomShot = Random.Range(0, shotSounds.Length);
                source.PlayOneShot(shotSounds[randomShot]);
            }
        }
    }

    public int GetShotNumber()
    {
        return (int)currentRound;
    }
}