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

    public void SendLeaderboard(int score, int level) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "Level "+level+" Score",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) {
        Debug.Log("Successfully updated leaderboard");
    }

    private bool IsLeaderboardScreen()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name == "Leaderboard Screen";
    }

    public void GetLeaderboard(int level) {
        var request = new GetLeaderboardRequest {
            StatisticName = "Level "+level+" Score",
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result) {
        if (result.Leaderboard.Count > 0) {
            foreach (var item in result.Leaderboard) {
                Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
            }
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        if (passwordInput != null)
        {
            passwordInput.inputType = InputField.InputType.Password;
        }

        if (IsLeaderboardScreen())
        {
            GetLeaderboard(1);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
