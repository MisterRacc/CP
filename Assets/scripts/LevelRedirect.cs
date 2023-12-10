using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelNumber; // Set this in the inspector for each button (1 for Button_lvl1, 2 for Button_lvl2, etc.)

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        int currentLevelState = PlayerPrefs.GetInt("Level" + levelNumber + "Completed", 0);

        // Set the button state based on the level completion
        if (currentLevelState == 1 || currentLevelState == 2)
        {
            // If the previous level is completed, set the button to be interactable
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
