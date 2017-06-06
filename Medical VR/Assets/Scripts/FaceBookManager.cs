using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System;
public class FacebookManager : MonoBehaviour
{

    private static FacebookManager _instance;
    public static FacebookManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject fbm = new GameObject("FBManager");
                fbm.AddComponent<FacebookManager>();
            }
            return _instance;
        }
    }

    public bool IsLoggedIn { get; set; }
    public string ProfileName { get; set; }
    public Sprite ProfilePic { get; set; }

    public int GlobalScore { get; set; }

    public string AppLinkURL { get; set; }


    //private List<object> scoresList = null;

    //public GameObject ScoreEntryPanel { get; set; }
    //public GameObject ScoreScrollList { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;
        IsLoggedIn = true;
    }

    public IEnumerator InitFB(Action t = null)
    {
        if (!FB.IsInitialized)
            FB.Init(SetInit, OnHideUnity);

        while (!FB.IsInitialized)
            yield return 0;

        IsLoggedIn = FB.IsLoggedIn;
        t();
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            GetProfile();
            FB.Mobile.RefreshCurrentAccessToken(null);
        }
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
        FB.GetAppLink(DealWithAppLink);
    }

    public void DisplayUsername(IResult result)
    {
        if (result.Error == null)
            ProfileName = "" + result.ResultDictionary["first_name"];
        else
            Debug.Log(result.Error);
    }

    public void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null) //maybe it is ==
            ProfilePic = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
    }

    void DealWithAppLink(IAppLinkResult result)
    {
        if (!String.IsNullOrEmpty(result.Url))
        {
            AppLinkURL = "" + result.Url + "";
            Debug.Log(AppLinkURL);
        }
        else
        {
            AppLinkURL = new Uri("http://www.google.com").ToString();
        }
    }

    public void Share()
    {
        FB.FeedShare(
            string.Empty,
            new Uri(AppLinkURL),
            "Hello this is the title",
            "This is the caption",
            "Check out this game",
            new Uri("http://cdn3.dualshockers.com/wp-content/uploads/2016/06/6wLbbG7bRypiRHwb8v7HtA-970-80.jpg"),
            string.Empty,
            ShareCallBack);
    }

    void ShareCallBack(IResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("Share Cancelled");
        }
        else if (!string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Error on share!");
        }
        else if (!string.IsNullOrEmpty(result.RawResult))
        {
            Debug.Log("Success on share");
        }
    }


    public void Invite()
    {
        FB.Mobile.AppInvite(
            new Uri(AppLinkURL),
            new Uri("http://cdn3.dualshockers.com/wp-content/uploads/2016/06/6wLbbG7bRypiRHwb8v7HtA-970-80.jpg"),
            InviteCallBack
            );
    }
    public void InviteCallBack(IResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("Invite Cancelled");
        }
        else if (!string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Error on invite!");
        }
        else if (!string.IsNullOrEmpty(result.RawResult))
        {
            Debug.Log("Success on invite");
        }
    }

    public void ShareWithUsers(int gscore)
    {
        FB.AppRequest(
            "Come and join me, I bet you can't beat my score! " + gscore.ToString(),
            null,
            new List<object>() { "app_users" },
            null,
            null,
            null,
            null,
            ShareWithUsersCallback
            );
    }

    void ShareWithUsersCallback(IAppRequestResult result)
    {
        Debug.Log(result.RawResult);
    }

    public void SetScore(int Scoreint)
    {

        var ScoreData = new Dictionary<string, string>();
        GlobalScore += Scoreint;

        ScoreData["score"] = GlobalScore.ToString();

        FB.API("/me/scores", HttpMethod.POST, delegate (IGraphResult result) { Debug.Log("Score Submitted successfully" + result.RawResult); }, ScoreData);
    }
}
