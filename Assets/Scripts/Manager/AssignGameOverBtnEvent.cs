using UnityEngine;
using UnityEngine.UI;

public class AssignGameOverBtnEvent : MonoBehaviour
{
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnRewarded;

    private void Start()
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
        }
    }
}
