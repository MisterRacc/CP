using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayfabManager : MonoBehaviour
{

    public Text messageText;

    public TextMeshProUGUI user1, user2, user3, user4, user5;

    public TextMeshProUGUI score1, score2, score3, score4, score5;

    public InputField usernameInput, passwordInput;

    private List<string> playerIds;

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
                DisplayName = usernameInput.text,
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
        //Reset dos estados dos levels
            PlayerPrefs.SetInt("Level" + 1 + "Completed", 1);
            PlayerPrefs.SetInt("Level" + 2 + "Completed", 0);
            PlayerPrefs.SetInt("Level" + 3 + "Completed", 0);
            PlayerPrefs.SetInt("Level" + 4 + "Completed", 0);
            PlayerPrefs.SetInt("Level" + 5 + "Completed", 0);
            PlayerPrefs.SetInt("Level" + 6 + "Completed", 0);
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
        RetrievePlayerStatistics();
        //insert here the call of the function that gets the level info
        StartCoroutine(Waiter());
    }


    void RetrievePlayerStatistics()
    {
        // Request to get player statistics
        var request = new GetPlayerStatisticsRequest();

        PlayFabClientAPI.GetPlayerStatistics(request, OnGetPlayerStatisticsSuccess, OnError);
    }
    void OnGetPlayerStatisticsSuccess(GetPlayerStatisticsResult result)
    {
        int maxCompletedLevel=0;
        foreach (var stat in result.Statistics)
        {
            Debug.Log("Statistic Name: " + stat.StatisticName + ", Value: " + stat.Value);
        }

         for (int levelNumber = 1; levelNumber <= 6; levelNumber++)
        {
            // Construct the statistic name for the current level
            string levelScoreStatisticName = "Level " + levelNumber + " Score";

            // Check if the score statistic exists for the current level
            int levelScore = GetStatisticValue(result, levelScoreStatisticName, -1);

            if (levelScore >= 0)
            {
                // If the score exists, set the corresponding "LevelXCompleted" PlayerPrefs key to 2
                string levelCompletedKey = "Level" + levelNumber + "Completed";
                PlayerPrefs.SetInt(levelCompletedKey, 2);
                maxCompletedLevel = levelNumber;
            }
            else{
               // If the score exists, set the corresponding "LevelXCompleted" PlayerPrefs key to 2
                string levelCompletedKey = "Level" + levelNumber + "Completed";
                PlayerPrefs.SetInt(levelCompletedKey, 0); 
            }
        }
        if(maxCompletedLevel>0){
            string levelCompletedKey = "Level" + (maxCompletedLevel+1) + "Completed";
            PlayerPrefs.SetInt(levelCompletedKey, 1); 
        }
        if(PlayerPrefs.GetInt("Level1Completed")==0){
            PlayerPrefs.SetInt("Level1Completed", 1); 
        }
        PlayerPrefs.Save(); 
    }

    // Helper function to get statistic value by name
    int GetStatisticValue(GetPlayerStatisticsResult result, string statisticName, int defaultValue)
    {
        foreach (var stat in result.Statistics)
        {
            if (stat.StatisticName == statisticName)
            {
                return stat.Value;
            }
        }

        return defaultValue;
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

            playerIds = new List<string>();
            cleanLeaderboard();
            for (int i = 0; i < result.Leaderboard.Count; i++) {
                var item = result.Leaderboard[i];

                playerIds.Add(item.PlayFabId);

                TextMeshProUGUI userText = GetUserText(i + 1);
                TextMeshProUGUI scoreText = GetScoreText(i + 1);

                if (scoreText != null) scoreText.text = item.StatValue.ToString();
                

            }

            GetPlayerProfiles();

        }
    }
    void cleanLeaderboard() {
    // Clear existing entries
    playerIds.Clear();

    // Clear TextMeshProUGUI components
    for (int i = 1; i <= 5; i++) {
        TextMeshProUGUI userText = GetUserText(i);
        TextMeshProUGUI scoreText = GetScoreText(i);

        if (userText != null) userText.text = "";
        if (scoreText != null) scoreText.text = "";
    }
}


    TextMeshProUGUI GetUserText(int index) {
        switch (index) {
            case 1: return user1;
            case 2: return user2;
            case 3: return user3;
            case 4: return user4;
            case 5: return user5;
            default: return null;
        }
    }

    TextMeshProUGUI GetScoreText(int index) {
        switch (index) {
            case 1: return score1;
            case 2: return score2;
            case 3: return score3;
            case 4: return score4;
            case 5: return score5;
            default: return null;
        }
    }

    void GetPlayerProfiles() {
        foreach (var playerId in playerIds) {
            var request = new GetPlayerProfileRequest {
                PlayFabId = playerId,
                ProfileConstraints = new PlayerProfileViewConstraints {
                    ShowDisplayName = true
                }
            };

            PlayFabClientAPI.GetPlayerProfile(request, result => OnPlayerProfileGet(result), OnError);
        }
    }

    void OnPlayerProfileGet(GetPlayerProfileResult result) {
        if (result.PlayerProfile != null) {
            int index = playerIds.IndexOf(result.PlayerProfile.PlayerId) + 1;
            TextMeshProUGUI userText = GetUserText(index);

            if (userText != null) userText.text = result.PlayerProfile.DisplayName;
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
