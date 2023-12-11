using UnityEngine;

public class SaveLevelButton : MonoBehaviour
{
    // Public variable to store the level value
    public string levelValue;

    // Function to be called when the button is clicked
    public void SaveLevel()
    {
        // Save the level value to PlayerPrefs with the key "CurrentLevel"
        PlayerPrefs.SetString("CurrentLevel", levelValue);

        // Save the PlayerPrefs to disk (optional, but ensures data persistence)
        PlayerPrefs.Save();

        Debug.Log("Current Level saved: " + levelValue);
    }
}
