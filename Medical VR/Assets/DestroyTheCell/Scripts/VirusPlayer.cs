using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class VirusPlayer : MonoBehaviour
{
    public GameObject TimerText;
    public float TimeLeft;
    public float Speed;

    public Image Screen;
    Color OriginalColor;
    Color FlashColor;
    void Start()
    {
        TimeLeft = 60.0f;
        Speed = .01f;

        OriginalColor = Screen.color;
        FlashColor = new Color(1, 1, 1, .2f);
    }


    void Update()
    {
        TimeLeft -= Time.deltaTime;
        TimerText.GetComponent<TextMesh>().text = "Timer: " + TimeLeft.ToString("f0");

        if (TimeLeft <= 0.0f)
        {
            TimeLeft = 0.0f;
        }

        Collider[] AntibodiesCloseBy = Physics.OverlapSphere(transform.position, 5.0f);

        if (AntibodiesCloseBy.Length != 0)
        {
            foreach (Collider enemy in AntibodiesCloseBy)
            {
                //If there is an enemy close then check if player is in the field of view
                if (enemy.GetComponent<Antibody>())
                {
                    if (enemy.GetComponent<Antibody>().CheckFOV() == true)
                    {
                        //Alert the player
                        StartCoroutine(FlashScreen());
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * Speed;
        GetComponent<Rigidbody>().velocity *= Speed;
    }

    IEnumerator FlashScreen()
    {
        Screen.color = FlashColor;
        yield return new WaitForSeconds(1.0f);
        Screen.color = OriginalColor;
    }
}

