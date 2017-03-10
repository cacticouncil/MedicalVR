using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System;

public class FBscript : MonoBehaviour, TimedInputHandler{

  public  GameObject DialogLoggedIn;
  public  GameObject DialogLoggedOut;

    private void Awake()
    {
        FB.Init(SetInit, OnHideUnity);
    }
    void SetInit()
    {
        if (FB.IsLoggedIn)
            Debug.Log("FB is Logged in");
        else
            Debug.Log("FB is Not Logged in");

        DealWithFBMenus(FB.IsLoggedIn);
    }
    void OnHideUnity(bool IsGameShown)
    {
        if(!IsGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;

        }
    }

    public void HandleTimeInput()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {
        if(result.Error != null)
        {
            Debug.Log(result.Error);

        }
        else
        {
            if (FB.IsLoggedIn)
                Debug.Log("FB is Logged in");
            else
                Debug.Log("FB is Not Logged in");

            DealWithFBMenus(FB.IsLoggedIn);
        }
    }

    void DealWithFBMenus( bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            DialogLoggedIn.SetActive(true);
            DialogLoggedOut.SetActive(false);
        }
        else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
        }
    }
}
