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
            Debug.Log("Score in PlayerPrefs: " + scoreValue);
        }
        else
        {
            Debug.Log("Score key not found in PlayerPrefs");
        }
    }
}
