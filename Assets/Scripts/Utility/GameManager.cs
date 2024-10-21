using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Texture2D cursor;
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
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
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
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}