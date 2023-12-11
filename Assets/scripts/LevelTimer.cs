using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float startingTime = 100; // Tempo inicial em segundos
    private float timeRemaining;
    public TMP_Text timeText;
    public bool timerIsRunning = true;

    void Start()
    {
        timeRemaining = startingTime;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Tempo esgotado!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
