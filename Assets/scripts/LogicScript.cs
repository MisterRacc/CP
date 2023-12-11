using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class LogicScript : MonoBehaviour
{
    public int lives;
    public Text livesText;
    public GameObject gameOverScreen;
    private bool bunnyCaught = false;
    private BunnyController bc;
    private PlayerScript ps;
    public ScoreAdder scoreUpdater;
    public Text scoreText;
    public PlayfabManager PlayfabManager;

    void Start()
    {
        ResetVariables();
        if (scoreUpdater != null)
        {
            scoreUpdater.LoadScore();
        }
    }
    void ResetVariables()
{
    lives = 5;  // Or any other initial value you want to set

    // Find and assign Text component dynamically
    livesText = GameObject.Find("Current Lives").GetComponent<Text>();
    if (livesText == null)
    {
        Debug.LogError("Unable to find livesText. Make sure the GameObject is present in the scene and has a Text component.");
    }
    livesText.text = lives.ToString();

    bc = GameObject.FindGameObjectWithTag("Bunny").GetComponent<BunnyController>();
    if (bc == null)
    {
        Debug.LogError("Unable to find BunnyController. Make sure the GameObject with the 'Bunny' tag is present in the scene.");
    }

    ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    if (ps == null)
    {
        Debug.LogError("Unable to find PlayerScript. Make sure the GameObject with the 'Player' tag is present in the scene.");
    }

    // Find and assign other components dynamically
    scoreText = GameObject.Find("Current Score").GetComponent<Text>();
    if (scoreText == null)
    {
        Debug.LogError("Unable to find scoreText. Make sure the GameObject is present in the scene and has a Text component.");
    }

    scoreUpdater = GetComponent<ScoreAdder>();
    if (scoreUpdater == null)
    {
        Debug.LogError("Unable to find ScoreAdder. Make sure the ScoreAdder script is attached to the same GameObject as LogicScript.");
    }
}

    public void takeDamage()
{
    if(lives>0){
        lives -= 1;
    }

    if (livesText != null)
    {
        livesText.text = lives.ToString();
    }

    if (lives <= 0)
    {
        Time.timeScale = 0;
        gameOver();
    }

    Debug.Log("lives: " + lives);
}


    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            PlayfabManager.SendLeaderboard(int.Parse(scoreText.text), 1);
        }
    }

    public bool IsBunnyCaught()
    {
        return bunnyCaught;
    }

    public void setBunnyCaught(bool boolean)
    {
        if (bc.areTheyTouching())
        {
            Debug.Log("bunny caught");
            bunnyCaught = boolean;
            IncreaseScore();
        }

        if (ps.getIfHit())
        {
            bunnyCaught = boolean;
        }
    }

    public void IncreaseScore()
    {
        if (scoreUpdater != null)
        {
            // Increase the score using ScoreUpdater script
            scoreUpdater.IncreaseScore(50); // You can adjust the score value as needed
        }
    }
}
