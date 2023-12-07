using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    private Text scoreText;

    private void Start()
    {
        GameObject scoreObject = GameObject.Find("Current Score");

        if (scoreObject != null)
        {
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
        if (scoreText != null)
        {
            scoreText.text = ScoreMain.Instance.Score.ToString();
        }
        else
        {
            Debug.LogError("Current Score Text component not found.");
        }
    }

    public void IncreaseScore(int score)
    {
        ScoreMain.Instance.IncreaseScore(score);

        // Save the updated score to PlayerPrefs
        SaveScore();

        // Update the score text
        UpdateScoreText();
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", ScoreMain.Instance.Score);
        PlayerPrefs.Save();
    }

    private void LoadScore()
    {
        ScoreMain.Instance.Score = PlayerPrefs.GetInt("Score", 0);
    }
}
