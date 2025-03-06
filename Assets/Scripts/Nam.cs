using System.Collections;
using UnityEngine;

public class Nam : MonoBehaviour
{
    public float waitTime = 3f;
    public int nextLevel = 5;

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
        GameManager.Instance.LoadLevel(nextLevel);
        AudioManager.Instance.sfxSource.Stop();
    }
}
