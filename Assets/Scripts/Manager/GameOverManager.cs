using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnRewarded;
    [SerializeField] private GameObject notification;
    [SerializeField] private TextMeshProUGUI txtNotification;
    [SerializeField] private Button btnCloseNotification;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            btnReplay.onClick.RemoveAllListeners();
            btnReplay.onClick.AddListener(GameManager.Instance.ReturnToMenuGame);
        }

        if (AdsManager.Instance != null)
        {
            btnRewarded.onClick.RemoveAllListeners();
            btnRewarded.onClick.AddListener(AdsManager.Instance.ShowRewardedAd);

            AdsManager.OnUserEarnedReward += HandleUserEarnedReward;
        }

        btnCloseNotification.onClick.RemoveAllListeners();
        btnCloseNotification.onClick.AddListener(CloseNotification);
    }

    private void Start()
    {
        notification.SetActive(false);
    }

    private void HandleUserEarnedReward(string message)
    {
        txtNotification.text = message;
        notification.SetActive(true);
    }

    public void CloseNotification()
    {
        notification.SetActive(false);
    }

    private void OnDisable()
    {
        AdsManager.OnUserEarnedReward -= (txt) => HandleUserEarnedReward(txt);
        btnCloseNotification.onClick.RemoveAllListeners();
        btnReplay.onClick.RemoveAllListeners();
        btnRewarded.onClick.RemoveAllListeners();
    }
}
