using UnityEngine;
using System.Collections;
using System;

public class Bullet : MonoBehaviour, TimedInputHandler {
    public void HandleTimeInput()
    {
        Destroy(gameObject);
        Storebullets.bulletamount += 1;
    }
}
