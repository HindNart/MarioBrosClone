using UnityEngine;

public class BulletBill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Vector2 direction = transform.position - collision.transform.position;

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
                Hit();
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<ObjectPool>().ReturnObject(gameObject);
        }
    }

    private void Hit()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        GetComponent<Collider2D>().enabled = false;
        FindObjectOfType<ObjectPool>().ReturnObject(gameObject);
    }
}

