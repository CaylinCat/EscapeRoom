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
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the GameManager singleton!");
            Destroy(this);
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
        Debug.Log("Start button clicked! Loading credits scene...");
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadGameScene()
    {
        Debug.Log("Start button clicked! Loading game scene...");
        SceneManager.LoadScene("Game");
    }
}