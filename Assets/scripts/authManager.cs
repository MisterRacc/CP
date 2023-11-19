using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class authManager : MonoBehaviour
{

    public Text logTxt;

    async void Start(){
        await UnityServices.InitializeAsync();
    }

    public async void SignIn(){
        await signInAnonymous();
    }

    async Task signInAnonymous()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            print("Sign in successful!");
            print("Player ID: " + AuthenticationService.Instance.PlayerId);
            logTxt.text = "Player id: " + AuthenticationService.Instance.PlayerId;

            SceneManager.LoadScene(0);        
        }
        catch (AuthenticationException e)
        {
            print("Sign in failed!");
            Debug.LogException(e);
        }
    }

}
