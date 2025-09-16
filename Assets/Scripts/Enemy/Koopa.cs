using System;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    [SerializeField] private float shellSpeed = 10f;

    private Animator anim;
    private Rigidbody2D rb;
    private bool shelled;
    private bool shellMoving;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
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
                EnterShell();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shelled && other.CompareTag("Player"))
        {
            if (!shellMoving)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();

                if (player.starPower)
                {
                    Hit();
                }
                else
                {
                    player.Hit();
                }
            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void EnterShell()
    {
        shelled = true;
        anim.SetTrigger("shelled");

        GetComponent<EntityMovement>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    private void PushShell(Vector2 direction)
    {
        shellMoving = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.moveSpeed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        anim.enabled = false;
        rb.velocity = new Vector2(rb.velocity.x, 2);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

    private void OnBecameInvisible()
    {
        if (shellMoving)
        {
            Destroy(gameObject);
        }
    }
}

