using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Princess : MonoBehaviour
{
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private GameObject winBanner;

    private void Awake()
    {
        winBanner.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().JumpEff();
            winBanner.SetActive(true);
            AudioManager.Instance.songSource.Stop();
            AudioManager.Instance.PlayWinSound();
            StartCoroutine(WaitToWin(waitTime));
        }
    }

    private IEnumerator WaitToWin(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.Instance.ReturnToMenuGame();
        AudioManager.Instance.sfxSource.Stop();
    }
}
