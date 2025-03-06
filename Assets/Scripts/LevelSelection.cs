using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public GameObject levels;

    private void Awake()
    {
        levels.SetActive(false);
    }

    public void Show()
    {
        levels.SetActive(true);
    }

    public void Hide()
    {
        levels.SetActive(false);
    }

    public void LoadLevel(int level)
    {
        GameManager.Instance.lives = 3;
        GameManager.Instance.coins = 0;
        GameManager.Instance.LoadLevel(level);
    }
}
