using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform connection;
    [SerializeField] private Vector3 enterDirection = Vector3.down;
    [SerializeField] private Vector3 exitDirection = Vector3.zero;
    [SerializeField] private GameObject piranha;

    private void Start()
    {
        if (piranha != null)
        {
            Instantiate(piranha, transform.position - Vector3.up, Quaternion.identity);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if (exitDirection != Vector3.zero || other.GetComponent<PlayerController>().enterPipe)
            {
                StartCoroutine(Enter(other.transform));
            }
        }
    }

    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerController>().enabled = false;

        AudioManager.Instance.PlayPipeSound();

        Vector3 enteredPos = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;

        yield return Move(player, enteredPos, enteredScale);
        yield return new WaitForSeconds(1f);

        bool underground = connection.position.y < 0;
        Camera.main.GetComponent<SideScrolling>().SetUnderground(underground);
        if (underground)
        {
            AudioManager.Instance.PlayUndergroundSong();
        }
        else
        {
            AudioManager.Instance.PlayGroundSong();
        }

        bool teleportBack = connection.position.x < transform.position.x;
        if (teleportBack)
        {
            Camera.main.GetComponent<SideScrolling>().transform.position = new Vector3(connection.position.x, Camera.main.GetComponent<SideScrolling>().transform.position.y, Camera.main.GetComponent<SideScrolling>().transform.position.z);
        }

        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }

        player.GetComponent<PlayerController>().enabled = true;
    }

    private IEnumerator Move(Transform player, Vector3 endPos, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPos = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            player.position = Vector3.Lerp(startPos, endPos, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        player.position = endPos;
        player.localScale = endScale;
    }
}
