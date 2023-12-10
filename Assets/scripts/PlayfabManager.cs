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
