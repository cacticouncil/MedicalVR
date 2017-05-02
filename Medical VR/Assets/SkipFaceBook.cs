using UnityEngine;
using System.Collections;

public class SkipFaceBook : MonoBehaviour
{
    public static bool booked = false;
    public GameObject faceBookIn;
    public GameObject faceBookOut;
    public GameObject mainMenu;

    // Use this for initialization
    void Start()
    {
        if (booked)
        {
            faceBookIn.SetActive(false);
            faceBookOut.SetActive(false);
            Fade.FadeIn(mainMenu);
        }
    }

    public void SetBool(bool b)
    {
        booked = b;
    }
}
