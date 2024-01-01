using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerupTimer : MonoBehaviour
{
    public TextMeshProUGUI powerupTimerText;
    private float currentTimer;
    private bool isCounting = false;

    public void StartTimer(float duration)
    {
        powerupTimerText.enabled = true;
        currentTimer = duration;
        isCounting = true;
        UpdateTimerText();
    }

    void Update()
    {
        if (isCounting)
        {
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0)
            {
                currentTimer = 0;
                isCounting = false;
                powerupTimerText.enabled = false;
            }

            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTimer / 60F);
        int seconds = Mathf.FloorToInt(currentTimer % 60);
        string timerString = string.Format("{00:00}:{01:00}", minutes, seconds);
        powerupTimerText.text = timerString;
    }
}