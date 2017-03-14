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
}
