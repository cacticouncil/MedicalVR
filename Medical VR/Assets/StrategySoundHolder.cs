using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategySoundHolder : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();
    [System.NonSerialized]
    public List<float> timeLeft = new List<float>();
    public AudioSource sus;

    private float playTime = 0;

    // Use this for initialization
    void Start()
    {
        sus.volume = GlobalVariables.bgmVolume;
        timeLeft.Capacity = clips.Count;
        for (int i = 0; i < clips.Count; i++)
        {
            timeLeft.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < clips.Count; i++)
        {
            timeLeft[i] = Mathf.Max(timeLeft[i] - Time.deltaTime, 0);
        }

        playTime -= Time.deltaTime;

        if (playTime <= 0)
            UpdateRandomSound();
    }

    private void UpdateRandomSound()
    {
        int loopCount = 0;
        for (int i = Random.Range(0, clips.Count); loopCount < clips.Count; i++)
        {
            if (i >= clips.Count)
            {
                i = 0;
            }

            if (timeLeft[i] == 0)
            {
                sus.PlayOneShot(clips[i]);
                timeLeft[i] = clips[i].length;
                playTime = clips[i].length * Random.Range(.2f, .75f);
                break;
            }

            loopCount++;
        }
    }

    public void PlayRandomSound()
    {
        int loopCount = 0;
        for (int i = Random.Range(0, clips.Count); loopCount < clips.Count; i++)
        {
            if (i >= clips.Count)
            {
                i = 0;
            }

            if (timeLeft[i] == 0)
            {
                sus.PlayOneShot(clips[i]);
                timeLeft[i] = clips[i].length;
                break;
            }

            loopCount++;
        }
    }
}
