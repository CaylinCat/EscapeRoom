using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer Instance;
    public float timeRemaining;
    public bool timerIsRunning = false;
    public float fadeawaySpeed;
    public TMPro.TMP_Text timeText, addTimeText;
    
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

        if (addTimeText.alpha > 0)
        {
            addTimeText.alpha -= fadeawaySpeed * Time.deltaTime;
        }
    }
    
    public void AddTimePuzzleComplete(float time)
    {
        if (timeRemaining + time < 0)
        {
            timeRemaining = 0.1f;
        }
        else
        {
            timeRemaining += time;
        }
        ShowAddedTime(time);
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
        GameManager.Instance.LoadDeathScene();
    }

    void ShowAddedTime(float time)
    {
        int minutes = Mathf.FloorToInt(Mathf.Abs(time) / 60);
        int seconds = Mathf.FloorToInt(Mathf.Abs(time) % 60);
        addTimeText.text = (time > 0 ? "+" : "-") + string.Format("{0:00}:{1:00}", minutes, seconds);
        addTimeText.alpha = 1;
        if (time > 0)
        {
            addTimeText.color = Color.green;
        }
        else
        {
            addTimeText.color = Color.red;
        }
    }
}
