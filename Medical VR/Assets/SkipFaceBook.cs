using UnityEngine;
using System.Collections;

public class SkipFaceBook : MonoBehaviour
{
    public static bool booked = false;
    public Fade mainMenu;

    // Use this for initialization
    void Start()
    {
        if (booked)
        {
            gameObject.SetActive(false);
            mainMenu.FadeIn();
        }
    }

    public void SetBool(bool b)
    {
        booked = b;
    }
}
