using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float moveSpeed = 2;
    public float gravity = 0f;
    public Vector2 direction = Vector2.left;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool flipSprite = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * moveSpeed;
        velocity.y += gravity * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        //kiểm tra va chạm -> đổi hướng enemy
        float radius = 0.25f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rb.position, radius, direction.normalized, distance, LayerMask.GetMask("Default"));
        if (hit.collider != null && hit.rigidbody != rb)
        {
            flipSprite = !flipSprite;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = flipSprite;
            direction = -direction;
        }
        //
    }
}
