using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 1200;
    public bool timerIsRunning = false;
    
    public TMPro.TMP_Text timeText;

    void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                TimerEnded();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        timeText.text = Mathf.Ceil(timeRemaining).ToString();
    }

    void TimerEnded()
    {
        Debug.Log("Timer ended!");
        
    }
}
