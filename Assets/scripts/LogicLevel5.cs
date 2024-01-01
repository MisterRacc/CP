using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using PlayFab;
using PlayFab.ClientModels;
using TMPro;
public class LogicLevel5 : MonoBehaviour
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
    private PlayerLvl5Script player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLvl5Script>();
        UpdateLivesUI();
    }

    // Update is called once per frame
    void Update(){
        if (timerText.text == "00:00")
        {
                completedLevel();
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
                PlayfabManager.SendLeaderboard(int.Parse(scoreText.text), 5);
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
            PlayerPrefs.SetInt("Level" + 5 + "Completed",2);
            PlayerPrefs.SetInt("Level" + 6 + "Completed",1);

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

    void gameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    void UpdateLivesUI()
    {
        livesText.text = lives.ToString();
    }
    
    public void PlayerInteractions(){
        if(player.GetInWaterArea()){
            player.AppearBucket();
        }
        else if(player.GetInContactWithPlant()){
            player.DisappearBucket();
            player.SetWaterDropped(true);
            IncreaseScore();
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
