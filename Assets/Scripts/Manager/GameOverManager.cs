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
        AdsManager.OnUserEarnedReward += (txt) => HandleUserEarnedReward(txt);
        btnCloseNotification.onClick.RemoveAllListeners();
        btnCloseNotification.onClick.AddListener(CloseNotification);

        if (GameManager.Instance != null)
        {
            btnReplay.onClick.RemoveAllListeners();
            btnReplay.onClick.AddListener(GameManager.Instance.ReturnToMenuGame);
        }

        if (AdsManager.Instance != null)
        {
            btnRewarded.onClick.RemoveAllListeners();
            btnRewarded.onClick.AddListener(AdsManager.Instance.ShowRewardedAd);
        }
    }

    private void Start()
    {
        notification.SetActive(false);
    }

    private void HandleUserEarnedReward(string txt)
    {
        txtNotification.text = txt;
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
    }
}
