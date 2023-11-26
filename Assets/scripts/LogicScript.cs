using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int lives;
    public Text livesText;
    public GameObject gameOverScreen;

    [ContextMenu("Take Damage")]
    public void takeDamage(){
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
}
