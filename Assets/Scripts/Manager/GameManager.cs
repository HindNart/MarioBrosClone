using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event System.Action OnCoinChanged;
    public static event System.Action OnLifeChanged;

    public int lives { get; set; }
    public int coins { get; set; }
    public int world { get; private set; }

    private bool inGame = false;
    private bool pauseGame = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void NewGame()
    {
        inGame = true;
        lives = 3;
        coins = 0;
        world = 1;

        LoadLevel(world);
    }

    public void PauseGame(bool c)
    {
        pauseGame = c;
    }

    public void LoadLevel(int world)
    {
        this.world = world;

        if (world == 4 || world == 10)
        {
            AudioManager.Instance.PlayCastleSong();
        }
        else
        {
            AudioManager.Instance.PlayGroundSong();
        }

        inGame = true;

        SceneManager.LoadScene($"{world}");
    }

    public IEnumerator ResetLevelDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!pauseGame && inGame)
        {
            ResetLevel();
        }
    }

    private void ResetLevel()
    {
        lives--;
        if (lives > 0)
        {
            LoadLevel(world);
        }
        else
        {
            GameOver();
        }
    }

    public void AddCoin()
    {
        AudioManager.Instance.PlayCoinSound();
        coins++;
        if (coins == 100)
        {
            AddLife();
            coins = 0;
        }
        OnCoinChanged?.Invoke();
    }

    public void AddLife()
    {
        AudioManager.Instance.Play1upSound();
        lives++;
        OnLifeChanged?.Invoke();
    }

    private void GameOver()
    {
        AudioManager.Instance.songSource.Stop();
        AudioManager.Instance.PlayGameOverSound();
        SceneManager.LoadScene("GameOver");
    }

    public void ReturnToMenuGame()
    {
        inGame = false;
        pauseGame = false;

        AdsManager.Instance.LoadRewardedAd();
        AudioManager.Instance.songSource.Stop();

        if (MenuManager.Instance != null)
        {
            Destroy(MenuManager.Instance.gameObject);
        }
        if (AudioManager.Instance != null)
        {
            Destroy(AudioManager.Instance.gameObject);
        }
        Destroy(this.gameObject);

        SceneManager.LoadScene("Menu");
    }
}