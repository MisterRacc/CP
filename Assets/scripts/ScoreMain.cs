using UnityEngine;

public class ScoreMain : MonoBehaviour
{
    public static ScoreMain Instance;

    // Initial score value
    private int initialScore = 0;

    // Current score value
    private int score = 0;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Set the score to its initial value
        ResetScore();
    }

    public void IncreaseScore(int amount)
    {
        // Increase the score by the specified amount
        score += amount;
    }

    public void ResetScore()
{
    // Reset the score to its initial value
    score = initialScore;

    // Reset the PlayerPrefs value
    PlayerPrefs.DeleteKey("Score");
    PlayerPrefs.Save();
}

}
