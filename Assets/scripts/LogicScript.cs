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
        bc = GameObject.FindGameObjectWithTag("Bunny").GetComponent<BunnyController>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (scoreUpdater != null)
        {
            scoreUpdater.LoadScore();
        }
    }

    public void takeDamage(){
        Debug.Log("taking damage");
        lives -= 1;
        livesText.text = lives.ToString();
        if(lives == 0){
            Time.timeScale = 0;
            gameOver();
        }
        Debug.Log("lives: " + lives);
    }

    public void gameOver(){
        gameOverScreen.SetActive(true);
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            PlayfabManager.SendLeaderboard(int.Parse(scoreText.text), 1);
        }
    }

    public bool IsBunnyCaught(){
        return bunnyCaught;
    }
    public void setBunnyCaught(bool boolean){
        if(bc.areTheyTouching()){
            Debug.Log("bunny caught");
            bunnyCaught = boolean;
            IncreaseScore();
        }

        if(ps.getIfHit()){
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