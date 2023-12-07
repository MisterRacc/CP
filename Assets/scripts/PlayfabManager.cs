using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{

    public Text messageText;

    public InputField usernameInput, passwordInput;

    public void RegisterButton(){
        if (usernameInput != null && passwordInput != null)
        {      
            if (passwordInput.text.Length < 6){
                messageText.text = "Password must be at least 6 characters long.";
                return;
            }
            var request = new RegisterPlayFabUserRequest{
                Username = usernameInput.text,
                Password = passwordInput.text,
                RequireBothUsernameAndEmail = false
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
        else{
            Debug.LogError("Username or password input field is empty!");
        }
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result){
        messageText.text = "Registered and logged in!";
        PlayerPrefs.SetString("Username", usernameInput.text);
        PlayerPrefs.SetInt("Score", 0);
        StartCoroutine(Waiter());
    }

    void OnError(PlayFabError error){
        messageText.text = error.ErrorMessage;
        Debug.LogError(error.GenerateErrorReport());
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);     
    }

    public void LoginButton(){
        var request = new LoginWithPlayFabRequest{
            Username = usernameInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result){
        messageText.text = "Successfully logged in!";
        PlayerPrefs.SetString("Username", usernameInput.text);
        PlayerPrefs.SetInt("Score", 0);
        StartCoroutine(Waiter());
    }

    // Start is called before the first frame update
    void Start()
    {
         if (passwordInput != null)
        {
            passwordInput.inputType = InputField.InputType.Password;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
