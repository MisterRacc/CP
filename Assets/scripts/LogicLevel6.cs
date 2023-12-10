using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicLevel6 : MonoBehaviour
{
    public Text timertext;
    public int initialTime;
    public GameObject gameOverScreen;

    private int time;
    private int amoutOfLeaves;

    // Start is called before the first frame update
    void Start()
    {
        time = initialTime;
        amoutOfLeaves = 0;
        UpdateTimertext();
        InvokeRepeating("UpdateTimer", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
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
