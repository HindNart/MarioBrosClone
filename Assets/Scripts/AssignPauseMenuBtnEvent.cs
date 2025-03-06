using UnityEngine;
using UnityEngine.UI;

public class AssignPauseMenuBtnEvent : MonoBehaviour
{
    public Button menu;
    public Button btnContinue;
    public Button btnBack;

    public GameObject menuPause;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            btnBack.onClick.RemoveAllListeners();
            btnBack.onClick.AddListener(GameManager.Instance.MenuGame);
        }
    }

    public void Pause()
    {
        menuPause.SetActive(true);
        GameManager.Instance.PauseGame(true);
        AudioManager.Instance.PlayPauseSound();
        AudioManager.Instance.songSource.Pause();
        Time.timeScale = 0;
    }

    public void Continue()
    {
        menuPause.SetActive(false);
        GameManager.Instance.PauseGame(false);
        AudioManager.Instance.songSource.UnPause();
        Time.timeScale = 1;
    }
}
