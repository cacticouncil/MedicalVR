﻿using UnityEngine;
using System.Collections;

public class GetpointsER : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "CBullet")
        Storebullets.score += 25;

        Destroy(collision.gameObject);
    }
}