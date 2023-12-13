using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float startingTime;
    public TMP_Text timeText;

    private bool timerIsRunning = true;
    private float timeRemaining;

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
