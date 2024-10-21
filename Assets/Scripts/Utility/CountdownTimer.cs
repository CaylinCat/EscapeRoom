using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer Instance;
    public float timeRemaining;
    public bool timerIsRunning = false;
    
    public TMPro.TMP_Text timeText;
    
    void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the CountdownTimer singleton!");
            Destroy(this);
        }
    }

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
    
    public void AddTimePuzzleComplete(float time)
    {
        timeRemaining += time;
    }

    void UpdateTimerDisplay()
    {
        if (timeRemaining >= 0)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void TimerEnded()
    {
        Debug.Log("Timer ended!");
        
    }
}
