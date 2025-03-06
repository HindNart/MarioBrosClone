using UnityEngine;
using UnityEngine.UI;

public class AssignMenuBtnEvent : MonoBehaviour
{
    public Button btnPlay;
    public Button btnQuit;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            btnPlay.onClick.RemoveAllListeners();
            btnPlay.onClick.AddListener(GameManager.Instance.NewGame);

            btnQuit.onClick.RemoveAllListeners();
            btnQuit.onClick.AddListener(GameManager.Instance.QuitGame);
        }
    }
}
