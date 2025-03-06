
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(GameManager.Instance.ResetLevelDelay(4f));
        }
        else Destroy(other.gameObject);
    }
}
