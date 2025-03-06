using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int lives;
    public int coins;
    public int world { get; private set; }

    private bool inGame = false;
    private bool pause = false;
    private TextMeshProUGUI txtCoins;
    private TextMeshProUGUI txtWorld;
    private TextMeshProUGUI txtLives;
    private GameObject UIController;

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

        Time.timeScale = 1;
    }

    public void NewGame()
    {
        inGame = true;
        lives = 3;
        coins = 0;
        world = 1;

        LoadLevel(world);
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
        Invoke(nameof(GetUI), 0.1f);
    }

    public IEnumerator ResetLevelDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!pause && inGame)
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
    }

    public void AddLife()
    {
        AudioManager.Instance.Play1upSound();
        lives++;
    }

    private void GameOver()
    {
        AudioManager.Instance.songSource.Stop();
        AudioManager.Instance.PlayGameOverSound();
        SceneManager.LoadScene("GameOver");
    }

    public void MenuGame()
    {
        inGame = false;
        pause = false;

        AdsManager.Instance.LoadRewardedAd();
        AudioManager.Instance.songSource.Stop();
        SceneManager.LoadScene("Menu");
    }

    public void PauseGame(bool c)
    {
        pause = c;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    #region HUDUI
    public void GetUI()
    {
        GameObject coinObject = GameObject.FindWithTag("UI_Coin");
        if (coinObject != null)
        {
            txtCoins = coinObject.GetComponent<TextMeshProUGUI>();
        }

        GameObject worldObject = GameObject.FindWithTag("UI_World");
        if (worldObject != null)
        {
            txtWorld = worldObject.GetComponent<TextMeshProUGUI>();
        }

        GameObject livesObject = GameObject.FindWithTag("UI_Live");
        if (livesObject != null)
        {
            txtLives = livesObject.GetComponent<TextMeshProUGUI>();
        }

        UIController = GameObject.FindWithTag("UI_Controller");
    }

    private void FixedUpdate()
    {
        if (txtCoins != null)
        {
            txtCoins.text = "Coins\n" + coins.ToString();
        }
        if (txtWorld != null)
        {
            txtWorld.text = "World\n" + world.ToString();
        }
        if (txtLives != null)
        {
            txtLives.text = "Lives\n" + lives.ToString();
        }
    }

    #endregion
}