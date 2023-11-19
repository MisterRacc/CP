using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{   
    private Text scoreText; // Reference to the Text component displaying the score
    private int score = 0;   // Current score value

    private void Start()
    {
        // Find the "Score1" GameObject and get the Text component
        GameObject scoreObject = GameObject.Find("Score1");

        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<Text>();
        }
        else
        {
            Debug.LogError("Score1 GameObject not found.");
        }

        // Set the initial score
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // Update the score text with the current score value
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        else
        {
            Debug.LogError("Score1 Text component not found.");
        }
    }

    public void IncreaseScore()
    {
        // Increase the score by 100 each time the button is pressed
        score += 100;

        // Update the score text
        UpdateScoreText();
    }
}
