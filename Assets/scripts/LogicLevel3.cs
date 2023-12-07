using UnityEngine;
using UnityEngine.UI;

public class LogicLevel3 : MonoBehaviour
{
    public int lives;
    public Text livesText;
    public ScoreAdder scoreUpdater; // Reference to ScoreUpdater script
    public GameObject gameOverScreen;

    void Start()
    {
        // Initialize lives and update the UI
        lives = 3; // Set your initial lives value
        UpdateLivesUI();

        // Access the ScoreUpdater script and call LoadScore
        if (scoreUpdater != null)
        {
            scoreUpdater.LoadScore();
        }
    }

    public void TakeDamage()
    {
        lives -= 1;
        UpdateLivesUI();

        if (lives == 0)
        {
            Time.timeScale = 0;
            gameOver();
        }
    }

    void UpdateLivesUI()
    {
        livesText.text = lives.ToString();
    }

    void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void IncreaseScore()
{
    if (scoreUpdater != null)
    {
        // Increase the score using ScoreUpdater script
        scoreUpdater.IncreaseScore(10); // You can adjust the score value as needed
    }
}

}
