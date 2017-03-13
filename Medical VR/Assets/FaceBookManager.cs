using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System;
public class FacebookManager : MonoBehaviour {

    private static FacebookManager _instance;
    public static FacebookManager Instance
    {
        get
        { 
            if(_instance == null)
            {
                GameObject fbm = new GameObject("FBManager");
                fbm.AddComponent<FacebookManager>();
            }
            return _instance;   
        }
    }

    public bool IsLoggedIn { get; set;}
    public string ProfileName { get; set;}
    public Sprite ProfilePic { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;
        IsLoggedIn = true;
    }

    public void InitFB()
    {
        if (!FB.IsInitialized)
            FB.Init(SetInit, OnHideUnity);
        else
        IsLoggedIn = FB.IsLoggedIn;
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is Logged in");
            GetProfile();
        }
        else
            Debug.Log("FB is Not Logged in");

        IsLoggedIn = FB.IsLoggedIn;
    }
    void OnHideUnity(bool IsGameShown)
    {
        if (!IsGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void GetProfile()
    {
        FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
        FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
    }

    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
            ProfileName = "" + result.ResultDictionary["first_name"];
        else
            Debug.Log(result.Error);
    }

    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null) //maybe it is ==
            ProfilePic = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
    }
}
