using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterScoreMenu : MonoBehaviour
{
    public GameObject facebookLoggedIn, facebookLoggedOut;

    // Use this for initialization
    public void Click()
    {
        if (FacebookManager.Instance.IsLoggedIn)
            Fade.FadeOut(transform.parent.gameObject, facebookLoggedIn);
        else
            Fade.FadeOut(transform.parent.gameObject, facebookLoggedOut);
    }
}
