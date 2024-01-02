using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class logoutManager : MonoBehaviour
{

    public Text logoutMsg;

    public void Logout()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())    
        {
            PlayFabClientAPI.ForgetAllCredentials();
        } else{
            UnityServices.InitializeAsync();
            LogoutAnonymous();
        }
        logoutMsg.text = "Logging out...";
        StartCoroutine(Waiter());
    }

    public void LogoutAnonymous(){
        try
        {
            AuthenticationService.Instance.SignOut();
        }
        catch (AuthenticationException e)
        {
            print("Sign out failed!");
            Debug.LogException(e);
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("AuthScreen");   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
