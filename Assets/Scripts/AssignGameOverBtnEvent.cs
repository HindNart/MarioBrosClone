using UnityEngine;
using UnityEngine.UI;

public class AssignGameOverBtnEvent : MonoBehaviour
{
    public Button btnReplay;
    public Button btnRewarded;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            btnReplay.onClick.RemoveAllListeners();
            btnReplay.onClick.AddListener(GameManager.Instance.MenuGame);
        }

        if (AdsManager.Instance != null)
        {
            btnRewarded.onClick.RemoveAllListeners();
            btnRewarded.onClick.AddListener(AdsManager.Instance.ShowRewardedAd);
        }
    }
}
