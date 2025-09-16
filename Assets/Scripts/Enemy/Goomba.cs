using UnityEngine;

public class Goomba : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

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
                Flatten();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void Flatten()
    {
        anim.SetTrigger("death");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

    private void Hit()
    {
        anim.enabled = false;
        rb.velocity = new Vector2(rb.velocity.x, 2);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        Destroy(gameObject, 0.5f);
    }
}

