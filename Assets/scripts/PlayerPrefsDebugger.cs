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
            int lvl1=PlayerPrefs.GetInt("Level1Completed");
            int lvl2=PlayerPrefs.GetInt("Level2Completed");
            int lvl3=PlayerPrefs.GetInt("Level3Completed");
            Debug.Log("Score in PlayerPrefs: " + scoreValue);
            Debug.Log("Guest: " + guestNumber);
            Debug.Log("User: "+ userNumber);
            Debug.Log("Audio: "+ audioSetting);

            Debug.Log("Level1: "+ lvl1);
            Debug.Log("Level2: "+ lvl2);
            Debug.Log("Level3: "+ lvl3);
        }
        else
        {
            Debug.Log("Score key not found in PlayerPrefs");
        }
    }
}
