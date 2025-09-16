using System.Collections;
using UnityEngine;

public class Lakitu : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float followDistance = 5f;
    [SerializeField] private Transform spinySpawnPoint;
    [SerializeField] private ObjectPool spinyPool;
    [SerializeField] private float throwInterval = 3f;

    private void Start()
    {
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
        StartCoroutine(ThrowRoutine());
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float targetX = player.position.x + followDistance;
        transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, targetX, moveSpeed * Time.deltaTime), transform.position.y);

        if (transform.position.x > player.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private IEnumerator ThrowRoutine()
    {
        while (true)
        {
            ThrowSpiny();
            yield return new WaitForSeconds(throwInterval);
        }
    }

    private void ThrowSpiny()
    {
        GameObject spiny = spinyPool.GetObject();
        spiny.transform.position = spinySpawnPoint.position;
        spiny.transform.rotation = spinySpawnPoint.rotation;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            Vector2 direction = transform.position - other.transform.position;

            if (player.starPower)
            {
                Hit();
            }
            else if (Vector2.Dot(direction.normalized, Vector2.down) > 0.25f)
            {
                AudioManager.Instance.PlayStompSound();
                player.JumpEff();
                Hit();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void Hit()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, 2);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Lakitu>().enabled = false;
        Destroy(gameObject, 2.5f);
    }
}
