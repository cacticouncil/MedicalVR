using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System;
using TMPro;

enum MiniGames
{
    FightVirus,
    DestroyCell,
    DodgeAntibodies,
    SimonDNA,
    MemoryGame,
    ATPGTPShooter,
    cGampSnatcher,
    StrategyGame
}

public class FBscript : MonoBehaviour
{
    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    public GameObject DialogUsername;
    public GameObject DialogProfilePic;
    public static int GlobalScore;

    public GameObject ScoreEntryPanel;
    public GameObject ScrollScoreList;

    void Awake()
    {
        StartCoroutine(FacebookManager.Instance.InitFB(HandleTimeInput));
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
        if (!IsGameShown)
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
        if (FB.IsLoggedIn)
        {
            FacebookManager.Instance.IsLoggedIn = true;
            FacebookManager.Instance.GetProfile();
        }

        DealWithFBMenus(FB.IsLoggedIn);
    }

    void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            DialogLoggedIn.SetActive(true);
            DialogLoggedOut.SetActive(false);

            StartCoroutine(LogIn());
        }
        else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
        }
    }

    IEnumerator LogIn()
    {
        while (FacebookManager.Instance.ProfileName == null)
            yield return 0;

        TextMeshPro UserName = DialogUsername.GetComponent<TextMeshPro>();
        UserName.text = "Hi, " + FacebookManager.Instance.ProfileName;

        while (FacebookManager.Instance.ProfilePic == null)
            yield return 0;

        Image ProfilePic = DialogProfilePic.GetComponent<Image>();
        ProfilePic.sprite = FacebookManager.Instance.ProfilePic;

        SetScore();
        QueryScore();
    }

    IEnumerator WaitForProfileName()
    {
        while (FacebookManager.Instance.ProfileName == null)
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

    public void NewQueryScore(string filename)
    {
        string filepath = "/app/scores/" + filename + "?fields=score,user.limit(30)";
        //FB.API("/app/scores/filetwo?fields=score,user.limit(30)", HttpMethod.GET, getScoresCallback);
        FB.API(filepath, HttpMethod.GET, getScoresCallback);
    }

    private void getScoresCallback(IResult result)
    {

        IDictionary<string, object> data = result.ResultDictionary;
        List<object> scoreList = (List<object>)data["data"];

        foreach (Transform child in ScrollScoreList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (object obj in scoreList)
        {
            var entry = (Dictionary<string, object>)obj;
            var user = (Dictionary<string, object>)entry["user"];

            //Debug.Log(user["name"].ToString() + " , " + entry["score"].ToString());

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

            FB.API("/" + user["id"].ToString() + "/picture?type=square&height=40&width=40", HttpMethod.GET, delegate (IGraphResult picResult)
            {
                if (picResult.Texture != null)
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

    public void SetScore()
    {
        if (GlobalScore > PlayerPrefs.GetInt("GlobalScore"))
        {
            PlayerPrefs.SetInt("GlobalScore", GlobalScore);
        }
        else
            GlobalScore = PlayerPrefs.GetInt("GlobalScore");

        var ScoreData = new Dictionary<string, string>();
        ScoreData["score"] = GlobalScore.ToString();

        FB.API("/me/scores", HttpMethod.POST, delegate (IGraphResult result) { Debug.Log("Score Submitted successfully" + result.RawResult); }, ScoreData);
        Debug.Log("Sending a score of " + GlobalScore);
    }

    public void newSetScore()
    {

        int Scoreint = 70;

        var ScoreData = new Dictionary<string, string>();
        GlobalScore = Scoreint;

        ScoreData["score"] = GlobalScore.ToString();
        FB.API("/me/scores/fileone", HttpMethod.POST, delegate (IGraphResult result) { Debug.Log("Score Submitted successfully" + result.RawResult); }, ScoreData);
        Debug.Log("Sending a score of file1 " + GlobalScore);


        var ScoreData2 = new Dictionary<string, string>();
        GlobalScore += 10;
        ScoreData2["score"] = GlobalScore.ToString();
        FB.API("/me/scores/filetwo", HttpMethod.POST, delegate (IGraphResult result) { Debug.Log("Score Submitted successfully" + result.RawResult); }, ScoreData2);
        Debug.Log("Sending a score of file2 " + GlobalScore);


        var ScoreData3 = new Dictionary<string, string>();
        GlobalScore += 10;
        ScoreData3["score"] = GlobalScore.ToString();
        FB.API("/me/scores/filethree", HttpMethod.POST, delegate (IGraphResult result) { Debug.Log("Score Submitted successfully" + result.RawResult); }, ScoreData3);
        Debug.Log("Sending a score of file3 " + GlobalScore);
    }
}
