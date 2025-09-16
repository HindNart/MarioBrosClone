using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;
    public int nextLevel = 1;

    public static event System.Action<int> OnUnlockLevel;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayFlagPoleSound();
            StartCoroutine(MoveTo(flag, poleBottom.position));
            AudioManager.Instance.songSource.Stop();
            AudioManager.Instance.PlayStageClearSound();
            StartCoroutine(LevelCompleteSequence(other.transform));
        }
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<PlayerController>().enabled = false;

        yield return MoveTo(player, poleBottom.position);
        // yield return MoveTo(player, player.position + Vector3.right);
        // yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
        yield return MoveTo(player, castle.position);

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(5.5f);

        OnUnlockLevel?.Invoke(nextLevel);

        GameManager.Instance.LoadLevel(nextLevel);
    }

    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while (Vector3.Distance(subject.position, destination) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = destination;
    }
}
