using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    private Text scoreText; // Reference to the Text component displaying the score
    private void Start()
{
    // Find the "Current Score" GameObject
    GameObject scoreObject = GameObject.Find("Current Score");

    if (scoreObject != null)
    {
        // Get the Text component directly from the GameObject
        scoreText = scoreObject.GetComponent<Text>();

        if (scoreText != null)
        {
            Debug.Log("Text component found on 'Current Score' GameObject.");
            
            // Load the initial score from PlayerPrefs
            LoadScore();
            // Update the score text
            UpdateScoreText();
        }
        else
        {
            Debug.LogError("Text component not found on 'Current Score' GameObject.");
        }
    }
    else
    {
        Debug.LogError("Current Score GameObject not found.");
    }
}


    private void UpdateScoreText()
    {
        // Update the score text with the current score value
        if (scoreText != null)
        {
            scoreText.text = ScoreMain.Instance.Score.ToString();
        }
        else
        {
            Debug.LogError("Current Score Text component not found.");
        }
    }

    public void IncreaseScore()
    {
        // Increase the score by 100 each time the button is pressed
        ScoreMain.Instance.IncreaseScore(100);

        // Save the updated score to PlayerPrefs
        SaveScore();

        // Update the score text
        UpdateScoreText();
    }

    private void SaveScore()
    {
        // Save the score to PlayerPrefs
        PlayerPrefs.SetInt("Score", ScoreMain.Instance.Score);
        PlayerPrefs.Save();
    }

    private void LoadScore()
{
    // Load the score from PlayerPrefs
    ScoreMain.Instance.Score = PlayerPrefs.GetInt("Score", 0);
}

}
