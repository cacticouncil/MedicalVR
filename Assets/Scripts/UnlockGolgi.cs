using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockGolgi : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "CBullet")
        {
            BannerScript.UnlockTrophy("Golgi");
        }
    }
}
