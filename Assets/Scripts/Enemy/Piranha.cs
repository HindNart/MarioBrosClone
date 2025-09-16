using System.Collections;
using UnityEngine;

public class Piranha : MonoBehaviour
{
    [SerializeField] private float waitToMove = 2f;

    private CapsuleCollider2D coll;

    private void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        StartCoroutine(Animate());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Hit();
        }
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            yield return Move(transform.localPosition + Vector3.up * 1.5f);
            coll.enabled = true;
            yield return new WaitForSeconds(waitToMove);
            coll.enabled = false;
            yield return Move(transform.localPosition + Vector3.down * 1.5f);
            yield return new WaitForSeconds(waitToMove);
        }
    }

    private IEnumerator Move(Vector3 endPos)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPos = transform.localPosition;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPos;
    }
}
