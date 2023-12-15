using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class Level2LogicScript : MonoBehaviour
{
    public int lives;
    public Text livesText;
    public GameObject gameOverScreen;
    public GameObject completedLevelScreen;
    private bool levelCompleted = false;
    private BunnyController2 bc;
    public ScoreAdder scoreUpdater;
    private float timeWithoutDamage = 0f;
    public Text scoreText;
    public Text resultText;
    public TMP_Text timerText;
    public PlayfabManager PlayfabManager;

    void Start()
    {
        ResetVariables();
        if (scoreUpdater != null)
        {
            scoreUpdater.LoadScore();
        }
    }

    void Update()
    {
        if (timerText.text == "00:00")
        {
            completedLevel();
        }
        timeWithoutDamage += Time.deltaTime; // Incrementa o tempo desde o último frame
        
        if (timeWithoutDamage >= 5f){
            IncreaseScore();
            timeWithoutDamage = 0f;
        }

    }

    void ResetVariables()
    {
        lives = 5;  
        livesText = GameObject.Find("Current Lives").GetComponent<Text>();
        if (livesText == null)
        {
            Debug.LogError("Unable to find livesText. Make sure the GameObject is present in the scene and has a Text component.");
        }
        livesText.text = lives.ToString();

        bc = GameObject.FindGameObjectWithTag("Bunny").GetComponent<BunnyController2>();
        if (bc == null)
        {
            Debug.LogError("Unable to find BunnyController. Make sure the GameObject with the 'Bunny' tag is present in the scene.");
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

        resultText = GameObject.Find("ResultMessage").GetComponent<Text>();
        if (resultText == null)
        {
            Debug.LogError("Unable to find resultText. Make sure the GameObject is present in the scene and has a Text component.");
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
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            PlayfabManager.SendLeaderboard(int.Parse(scoreText.text), 2);
        }
    }

    public void completedLevel()
    {
        if (!levelCompleted)
        {
            Time.timeScale = 0f;
            completedLevelScreen.SetActive(true);
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                PlayfabManager.SendLeaderboard(int.Parse(scoreText.text), 2);
                PlayfabManager.AnalyzeResult(int.Parse(scoreText.text));
                string result = PlayfabManager.DetermineItemBasedOnScore(int.Parse(scoreText.text));
                if (result == "")
                {
                    resultText.text = "Congratulations! You have completed the level! Unfortunaly your performance wasn't the required to get a reward!";
                }
                else
                {
                    resultText.text = "Congratulations! You have completed the level! You won: " + result + "!";
                }
            }

            levelCompleted = true;
        }
    }

    public void IncreaseScore()
    {
        if (scoreUpdater != null)
        {
            // Increase the score using ScoreUpdater script
            scoreUpdater.IncreaseScore(100); // You can adjust the score value as needed
        }
    }

    public void resetTimeWithoutDamage()
    {
        timeWithoutDamage = 0f;
    }
}
