using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void LoadDeathScene()
    {
        SceneManager.LoadScene("DeathScene");
    }
    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}