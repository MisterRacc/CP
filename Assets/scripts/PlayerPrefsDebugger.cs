using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerPrefsDebugger : MonoBehaviour
{
    void Start()
    {
        // Check if the "Score" key exists in PlayerPrefs
        if (PlayerPrefs.HasKey("Score"))
        {
            // Retrieve and print the value
            int scoreValue = PlayerPrefs.GetInt("Score");
            int guestNumber = PlayerPrefs.GetInt("GuestNumber");
            string userNumber = PlayerPrefs.GetString("Username");
            int audioSetting = PlayerPrefs.GetInt("AudioSetting");
            Debug.Log("Score in PlayerPrefs: " + scoreValue);
            Debug.Log("Guest: " + guestNumber);
            Debug.Log("User: "+ userNumber);
            Debug.Log("Audio: "+ audioSetting);
        }
        else
        {
            Debug.Log("Score key not found in PlayerPrefs");
        }
    }
}
