using System.Collections;
using UnityEngine;

public class Nam : MonoBehaviour
{
    public float waitTime = 3f;
    public int nextLevel = 5;

    public static event System.Action<int> OnUnlockLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.songSource.Stop();
            AudioManager.Instance.PlayCastleClearSound();
            StartCoroutine(WaitToNextLvl(waitTime));
        }
    }

    private IEnumerator WaitToNextLvl(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        OnUnlockLevel?.Invoke(nextLevel);
        GameManager.Instance.LoadLevel(nextLevel);
        AudioManager.Instance.sfxSource.Stop();
    }
}
