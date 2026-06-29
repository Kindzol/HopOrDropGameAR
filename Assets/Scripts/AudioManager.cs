using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Music")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;

    [Header("Sound effects")]
    public AudioClip loseLifeSound;
    public AudioClip levelCompleteSound;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.5f;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.volume = 1f;
    }

    void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged += HandleStateChanged;

        PlayMusic(mainMenuMusic);
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged -= HandleStateChanged;
    }

    void HandleStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.MainMenu:
                PlayMusic(mainMenuMusic);
                break;
            case GameManager.GameState.Playing:
                PlayMusic(gameplayMusic);
                break;
            case GameManager.GameState.GameOver:
                musicSource.Stop();
                PlaySFX(loseLifeSound);
                break;
            case GameManager.GameState.LevelComplete:
                musicSource.Stop();
                PlaySFX(levelCompleteSound);
                break;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayLoseLife()
    {
        PlaySFX(loseLifeSound);
    }

    public void PlayLevelComplete()
    {
        PlaySFX(levelCompleteSound);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }
}