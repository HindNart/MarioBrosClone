using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("In-Game UI Elements")]
    [SerializeField] private TextMeshProUGUI txtCoins;
    [SerializeField] private TextMeshProUGUI txtWorld;
    [SerializeField] private TextMeshProUGUI txtLives;

    [Header("Pause Menu Buttons")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button btnPauseMenu;
    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnReturnToMainMenu;

    private void Start()
    {
        txtCoins.text = "Coins\n" + GameManager.Instance.coins.ToString();
        txtWorld.text = "World\n" + GameManager.Instance.world.ToString();
        txtLives.text = "Lives\n" + GameManager.Instance.lives.ToString();

        GameManager.OnCoinChanged += () => UpdateCoins(GameManager.Instance.coins);
        GameManager.OnLifeChanged += () => UpdateLives(GameManager.Instance.lives);

        btnPauseMenu.onClick.AddListener(TogglePauseMenu);
        btnResume.onClick.AddListener(OnResumeGameClicked);
        btnReturnToMainMenu.onClick.AddListener(OnReturnToMainMenuClicked);
        pauseMenu.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        bool isActive = pauseMenu.activeSelf;
        pauseMenu.SetActive(!isActive);
        Time.timeScale = isActive ? 1 : 0;
        GameManager.Instance.PauseGame(!isActive);
        AudioManager.Instance.PlayPauseSound();
        if (!isActive)
        {
            AudioManager.Instance.songSource.Pause();
        }
        else
        {
            AudioManager.Instance.songSource.UnPause();
        }
    }

    private void OnResumeGameClicked()
    {
        TogglePauseMenu();
    }

    private void OnReturnToMainMenuClicked()
    {
        if (GameManager.Instance != null)
        {
            Time.timeScale = 1;
            GameManager.Instance.ReturnToMenuGame();
        }
    }

    public void UpdateCoins(int coins)
    {
        txtCoins.text = "Coins\n" + coins.ToString();
    }

    public void UpdateLives(int lives)
    {
        txtLives.text = "Lives\n" + lives.ToString();
    }

    private void OnDestroy()
    {
        btnPauseMenu.onClick.RemoveAllListeners();
        btnResume.onClick.RemoveAllListeners();
        btnReturnToMainMenu.onClick.RemoveAllListeners();
    }
}