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
        }
        else if(ecra == "InventoryScreen"){
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                SceneManager.LoadScene(ecra);
                Time.timeScale = 1;
            } else {
                messageText.text = "You must be logged in to use the Inventory!";
                StartCoroutine(HideMessageTextAfterDelay(3f));
            }
        }
        else{
            SceneManager.LoadScene(ecra);
            Time.timeScale = 1;
        }
    }

    private IEnumerator HideMessageTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = string.Empty; // Clear the messageText
    }

}
