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
    public int GlobalScore;

    public GameObject ScoreEntryPanel;
    public GameObject ScrollScoreList;
    void Awake()
    {
        GlobalScore = 0;
        FacebookManager.Instance.InitFB();
        DealWithFBMenus(FB.IsLoggedIn);
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
            {
                FacebookManager.Instance.IsLoggedIn = true;
                FacebookManager.Instance.GetProfile();
                Debug.Log("FB is Logged in");
            }
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

            if(FacebookManager.Instance.ProfileName != null)
            {
                Text UserName = DialogUsername.GetComponent<Text>();
                UserName.text = "Hi, " + FacebookManager.Instance.ProfileName;
            }
            else
            {
                StartCoroutine("WaitForProfileName");
            }

            if (FacebookManager.Instance.ProfilePic != null)
            {
                Image ProfilePic = DialogProfilePic.GetComponent<Image>();
                ProfilePic.sprite = FacebookManager.Instance.ProfilePic;
            }
            else
            {
                StartCoroutine("WaitForProfilePic");
            }
        }
        else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
        }
    }

    IEnumerator WaitForProfileName()
    {
        while(FacebookManager.Instance.ProfileName == null)
        {
          yield return null;
        }

        DealWithFBMenus(FacebookManager.Instance.IsLoggedIn);

    }

    IEnumerator WaitForProfilePic()
    {
        while (FacebookManager.Instance.ProfilePic == null)
        {
            yield return null;
        }

        DealWithFBMenus(FB.IsLoggedIn);

    }

    public void Share()
    {
        FacebookManager.Instance.Share();
    }

    public void Invite()
    {
        FacebookManager.Instance.Invite();
    }

    public void ShareWithUsers()
    {
        FacebookManager.Instance.ShareWithUsers(GlobalScore);
    }

    public void QueryScore()
    {
        FB.API("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, getScoresCallback);
    }

    private void getScoresCallback(IResult result)
    {

        IDictionary<string, object> data = result.ResultDictionary;
        List<object> scoreList = (List<object>)data["data"];

        foreach (Transform child in ScrollScoreList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (object obj in scoreList)
        {
            var entry = (Dictionary<string, object>)obj;
            var user = (Dictionary<string, object>)entry["user"];

            Debug.Log(user["name"].ToString() + " , " + entry["score"].ToString());

            GameObject ScorePanel;
            ScorePanel = Instantiate(ScoreEntryPanel) as GameObject;
            ScorePanel.transform.SetParent(ScrollScoreList.transform, false);

            Transform FName = ScorePanel.transform.Find("FriendName");
            Transform FScore = ScorePanel.transform.Find("FriendScore");
            Transform FAvatar = ScorePanel.transform.Find("FriendAvatar");

            Text Fnametext = FName.GetComponent<Text>();
            Text Fscoretext = FScore.GetComponent<Text>();
            Image FUserAvatar = FAvatar.GetComponent<Image>();

            Fnametext.text = user["name"].ToString();
            Fscoretext.text = entry["score"].ToString();

            FB.API("/" + user["id"].ToString() + "/picture?type=square&height=40&width=40", HttpMethod.GET, delegate(IGraphResult picResult) {
                if(picResult.Texture != null)
                {
                    FUserAvatar.sprite = Sprite.Create(picResult.Texture, new Rect(0, 0, 40, 40), new Vector2());
                }
                else
                {
                    Debug.Log(picResult.Error);
                }

            });

        }
    }

    public void SetScore(int Scoreint)
    {
        var ScoreData = new Dictionary<string, string>();
        GlobalScore += Scoreint; 

        ScoreData["score"] = GlobalScore.ToString();

        FB.API("/me/scores", HttpMethod.POST, delegate(IGraphResult result) {Debug.Log("Score Submitted successfully" + result.RawResult);}, ScoreData);
    }
}
