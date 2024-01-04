using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public int lives;
    public Text livesText;
    public Button Interact;
    public GameObject gameOverScreen;
    public GameObject completedLevelScreen;
    private bool bunnyCaught;
    private float timeBunnyCaught = 0f;
    private bool levelCompleted = false;
    private BunnyController bc;
    private PlayerScript ps;
    public ScoreAdder scoreUpdater;
    public Text scoreText;
    public Text resultText;
    public TMP_Text timerText;
    public PlayfabManager PlayfabManager;
    private bool canDeleteTrash;

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
            if(!IsBunnyCaught()){
                gameOver();
            }
            else{
                completedLevel();
            }
        }

        if (IsBunnyCaught())
        {
            timeBunnyCaught += Time.deltaTime; // Incrementa o tempo desde o último frame

            if (timeBunnyCaught >= 10f)
            {
                IncreaseScore();
                timeBunnyCaught = 0f; // Reinicia o contador para o próximo décimo de segundo
            }
        }
    }

    void ResetVariables()
    {
        lives = 5;  
        Interact.interactable = true;
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
    }

    public void increaseLives(int amount){
        lives += amount;
        livesText = GameObject.Find("Current Lives").GetComponent<Text>();
        livesText.text = lives.ToString();
    }

    public void takeDamage(int amount)
    {
        if(lives > 0){
            lives += amount;
        }

        if (livesText != null)
        {
            livesText.text = lives.ToString();
        }

        if (lives <= 0)
        {
            Time.timeScale = 0;
            Interact.interactable = false;
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
            PlayfabManager.SendLeaderboard(int.Parse(scoreText.text), 1);
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
                PlayfabManager.SendLeaderboard(int.Parse(scoreText.text), 1);
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
            PlayerPrefs.SetInt("Level" + 1 + "Completed",2);
            if(PlayerPrefs.GetInt("Level" + 2 + "Completed",0)!=2){
                PlayerPrefs.SetInt("Level" + 2 + "Completed",1);
            }
            levelCompleted = true;
        }
    }

    public bool IsBunnyCaught()
    {
        return bunnyCaught;
    }

    public void setBunnyCaught()
    {
        if(ps.GetTouchingTrash()){
            if(bunnyCaught){
                bunnyCaught = false;
            }

            SetCanDeleteTrash(true);
            IncreaseScore();
        }
        else{
            if (bc.areTheyTouching())
            {
                bunnyCaught = true;
                IncreaseScore();
            }
        }

        if (ps.getIfHit())
        {
            bunnyCaught = false;
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

    public void SetCanDeleteTrash(bool boolean){
        canDeleteTrash = boolean;
    }

    public bool GetCanDeleteTrash(){
        return canDeleteTrash;
    }
}
