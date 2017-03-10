using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System;

public class FBscript : MonoBehaviour, TimedInputHandler{

  public  GameObject DialogLoggedIn;
  public  GameObject DialogLoggedOut;
  public GameObject DialogUsername;
  public GameObject DialogProfilePic;



    void Awake()
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
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
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
            Debug.Log(result.Error);
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

            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);

        }
        else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
        }
    }

    void DisplayUsername(IResult result)
    {
       Text Username = DialogUsername.GetComponent<Text>();

        if(result.Error == null)
        {
            Username.text = "Hi there, " + result.ResultDictionary["first_name"];
        }
        else
            Debug.Log(result.Error);
    }

    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Error == null)
        {
            Image ProfilePic = DialogProfilePic.GetComponent<Image>();
            ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
}
