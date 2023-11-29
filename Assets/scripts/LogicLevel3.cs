using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicLevel3 : MonoBehaviour
{
    public int lives;
    public Text livesText;
    public int score;
    public Text scoretext;
    public GameObject gameOverScreen;

    public void TakeDamage(){
        lives -= 1;
        livesText.text = lives.ToString();

        if(lives == 0){
            Time.timeScale = 0;
            gameOver();
        }
    }

    public void gameOver(){
        gameOverScreen.SetActive(true);
    }

    public void IncreaseScore(){
        score += 10;
        scoretext.text = score.ToString();
    }
}
