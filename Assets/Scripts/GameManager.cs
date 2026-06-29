using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver,
        LevelComplete
    }

    public GameState CurrentState { get; private set; }

    public int MaxLives = 3;
    public int CurrentLives { get; private set; }

    public event System.Action<int> OnLivesChanged;
    public event System.Action<GameState> OnStateChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CurrentLives = MaxLives;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetState(GameState.MainMenu);
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
        Debug.Log("Game state: " + newState);
    }

    public void StartGame()
    {
        CurrentLives = MaxLives;
        SetState(GameState.MainMenu);
        SceneManager.LoadScene("Gameplay");
    }

    public void LoseLife()
    {
        if (CurrentState != GameState.Playing) return;

        CurrentLives--;
        OnLivesChanged?.Invoke(CurrentLives);

        if (CurrentLives <= 0)
            TriggerGameOver();
    }

    public void TriggerGameOver()
    {
        SetState(GameState.GameOver);
    }

    public void TriggerLevelComplete()
    {
        SetState(GameState.LevelComplete);
    }

    public void RestartGame()
    {
        CurrentLives = MaxLives;
        SetState(GameState.Playing);
        SceneManager.LoadScene("Gameplay");
    }

    public void GoToMainMenu()
    {
        SetState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }
}