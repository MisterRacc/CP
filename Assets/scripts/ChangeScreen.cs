using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class ChangeScreen : MonoBehaviour
{
    public Text messageText;
    public void MoveToScene(string ecra)
    {
        if (ecra == "Leaderboard Screen"){
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                SceneManager.LoadScene(ecra);
                Time.timeScale = 1;
            }
            else{
                messageText.text = "You must be logged in to access the Leaderboard!";
            }
        }else{
            SceneManager.LoadScene(ecra);
            Time.timeScale = 1;
        }
    }
}
