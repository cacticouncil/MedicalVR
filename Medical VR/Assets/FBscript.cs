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
    public Text ScoreDebug;


    void Awake()
    {
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
        FacebookManager.Instance.ShareWithUsers();
    }

    public void QueryScore()
    {
        FB.API("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, getScoresCallback);
    }

    private void getScoresCallback(IResult result)
    {

        IDictionary<string, object> data = result.ResultDictionary;
        List<object> scoreList = (List<object>)data["data"];

        foreach(object obj in scoreList)
        {
            var entry = (Dictionary<string, object>)obj;
            var user = (Dictionary<string, object>)entry["user"];

            Debug.Log(user["name"].ToString() + " , " + entry["score"].ToString());

            ScoreDebug.text = result.RawResult;

        }
        //Debug.Log("Scores callback: " + result.RawResult);


        ////scoresList =  Util.DeserializeScores(result.RawResult);

        //foreach (Transform child in ScoreScrollList.transform)
        //{
        //    GameObject.Destroy(child.gameObject);
        //}

        //foreach (object score in scoresList)
        //{

        //    var entry = (Dictionary<string, object>)score;
        //    var user = (Dictionary<string, object>)entry["user"];




        //    GameObject ScorePanel;
        //    ScorePanel = Instantiate(ScoreEntryPanel) as GameObject;
        //    ScorePanel.transform.parent = ScoreScrollList.transform;

        //    Transform ThisScoreName = ScorePanel.transform.Find("FriendName");
        //    Transform ThisScoreScore = ScorePanel.transform.Find("FriendScore");
        //    Text ScoreName = ThisScoreName.GetComponent<Text>();
        //    Text ScoreScore = ThisScoreScore.GetComponent<Text>();

        //    ScoreName.text = user["name"].ToString();
        //    ScoreScore.text = entry["score"].ToString();

        //    Transform TheUserAvatar = ScorePanel.transform.Find("FriendAvatar");
        //    Image UserAvatar = TheUserAvatar.GetComponent<Image>();


        //    FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, delegate (IGraphResult pictureResult) {

        //        if (pictureResult.Error != null) // if there was an error
        //        {
        //            Debug.Log(pictureResult.Error);
        //        }
        //        else // if everything was fine
        //        {
        //            UserAvatar.sprite = Sprite.Create(pictureResult.Texture, new Rect(0, 0, 128, 128), new Vector2(0, 0));
        //        }

        //    });



        //}


    }

    public void SetScore()
    {
        var ScoreData = new Dictionary<string, string>();

        ScoreData["score"] = UnityEngine.Random.Range(0, 250).ToString();

        FB.API("me/scores", HttpMethod.POST, delegate (IGraphResult result)
            {Debug.Log("Score Submitted successfully" + result.RawResult);},
            ScoreData);
    }
}
