using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using PlayFab;
using PlayFab.ClientModels;
using TMPro;
public class LogicLevel6 : MonoBehaviour
{
    public Text timertext;
    public int initialTime;
    public GameObject gameOverScreen;

    private int time;
    private int amoutOfLeaves;
    public int lives;
    public Text livesText;
    public Text scoreText;
    public ScoreAdder scoreUpdater;
    public TMP_Text timer_Text;
    public GameObject completedLevelScreen;
    private bool levelCompleted = false;
    public PlayfabManager PlayfabManager;
    public Text resultText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLivesUI();
        time = initialTime;
        amoutOfLeaves = 0;
        UpdateTimertext();
        InvokeRepeating("UpdateTimer", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer_Text.text == "00:00")
        {
            completedLevel();
        }
    }
    
    void UpdateLivesUI()
    {
        livesText.text = lives.ToString();
    }

    void UpdateTimer()
    {
        time--;
        UpdateTimertext();

        if(time == 0){
            GameOver();
        }
    }

    void UpdateTimertext()
    {
        timertext.text = time.ToString();
    }

    public void IncreaseTimer(int amount)
    {
        time += amount;
        UpdateTimertext();
    }

    public void completedLevel()
    {
        if (!levelCompleted)
        {
            Time.timeScale = 0f;
            completedLevelScreen.SetActive(true);
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                PlayfabManager.SendLeaderboard(int.Parse(scoreText.text), 6);
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
            PlayerPrefs.SetInt("Level" + 6 + "Completed",2);

            levelCompleted = true;
        }
    }

    public void SetLeavesCount(int amount){
        amoutOfLeaves += amount;
    }

    public int GetLeavesAmount(){
        return amoutOfLeaves;
    }

    public void ResetLeaveCounter(){
        amoutOfLeaves = 0;
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
}
