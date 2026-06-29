using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [Header("Lifes")]
    public Image[] lifeIcons;

    [Header("Screens")]
    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;

    [Header("Game Over Buttons")]
    public Button restartButton;
    public Button mainMenuButtonGameOver;

    [Header("Level Complete Buttons")]
    public Button mainMenuButtonComplete;

    [Header("Controller")]
    public PlayerControllerAR playerController;

    [Header("AR")]
    public ARLevelSpawner levelSpawner;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLivesChanged += UpdateLives;
            GameManager.Instance.OnStateChanged += HandleStateChanged;
            UpdateLives(GameManager.Instance.CurrentLives);
        }

        gameOverScreen.SetActive(false);
        levelCompleteScreen.SetActive(false);

        restartButton.onClick.AddListener(() => {
            if (levelSpawner != null) levelSpawner.ResetLevel();
            GameManager.Instance.RestartGame();
        });
        mainMenuButtonGameOver.onClick.AddListener(() => GameManager.Instance.GoToMainMenu());
        mainMenuButtonComplete.onClick.AddListener(() => GameManager.Instance.GoToMainMenu());
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLivesChanged -= UpdateLives;
            GameManager.Instance.OnStateChanged -= HandleStateChanged;
        }
    }

    void UpdateLives(int currentLives)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
            lifeIcons[i].enabled = i < currentLives;
    }

    void HandleStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.GameOver)
            gameOverScreen.SetActive(true);

        if (state == GameManager.GameState.LevelComplete)
            levelCompleteScreen.SetActive(true);
    }

    
    public void OnUpPressed() { if (playerController != null) playerController.SetMoveInput(Vector2.up); }
    public void OnDownPressed() { if (playerController != null) playerController.SetMoveInput(Vector2.down); }
    public void OnLeftPressed() { if (playerController != null) playerController.SetMoveInput(Vector2.left); }
    public void OnRightPressed() { if (playerController != null) playerController.SetMoveInput(Vector2.right); }
    public void OnMoveReleased() { if (playerController != null) playerController.SetMoveInput(Vector2.zero); }
    public void OnJumpPressed()
    {
        Debug.Log("OnJumpPressed called! playerController: " + playerController);
        if (playerController != null) playerController.Jump();
    }
}