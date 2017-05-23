using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategySoundHolder : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();
    public AudioSource sus;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayRandomSound()
    {
        if (sus.isPlaying)
        {

        }
    }
}
