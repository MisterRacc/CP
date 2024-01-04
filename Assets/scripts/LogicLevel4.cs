using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using PlayFab;
using PlayFab.ClientModels;
using TMPro;
public class LogicLevel4 : MonoBehaviour
{
    public int lives;
    public Text livesText;
    public Text scoreText;
    public ScoreAdder scoreUpdater;
    public GameObject gameOverScreen;
    public TMP_Text timerText;
    public GameObject completedLevelScreen;
    private bool levelCompleted = false;
    public PlayfabManager PlayfabManager;
    public Text resultText;


    void Update(){
        if (timerText.text == "00:00")
        {
            completedLevel();
        }
    }
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

    public void completedLevel()
    {
        if (!levelCompleted)
        {
            Time.timeScale = 0f;
            completedLevelScreen.SetActive(true);
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                PlayfabManager.SendLeaderboard(int.Parse(scoreText.text), 3);
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
            PlayerPrefs.SetInt("Level" + 4 + "Completed",2);
            if(PlayerPrefs.GetInt("Level" + 5 + "Completed",0)!=2){
                PlayerPrefs.SetInt("Level" + 5 + "Completed",1);
            }

            levelCompleted = true;
        }
    }

    public void TakeDamage()
    {
        lives -= 1;
        UpdateLivesUI();

        if (lives == 0)
        {
            gameOver();
        }
    }

    public void increaseLives(int amount){
        lives += amount;
        livesText = GameObject.Find("Current Lives").GetComponent<Text>();
        livesText.text = lives.ToString();
    }

    void UpdateLivesUI()
    {
        livesText.text = lives.ToString();
    }

    void gameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
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
