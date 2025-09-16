using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public ObjectPool fireBallPool;
    public Transform firePoint;

    private Player player;
    private Camera _camera;
    private Rigidbody2D rb;
    private float move;
    private bool isGrounded;
    private bool isJumping;
    private bool moveLeft;
    private bool moveRight;
    private int maxJumps = 2;
    private int jumpCount = 1;
    public bool enterPipe { get; private set; }

    private void Start()
    {
        _camera = Camera.main;
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckJump();
    }

    private void FixedUpdate()
    {
        Move();
        Vector2 position = rb.position;
        Vector2 leftEdge = _camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);
        rb.position = new Vector2(position.x, rb.position.y);
    }

    void Move()
    {
        // move = Input.GetAxis("Horizontal");

        if (moveLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void CheckJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        if (isGrounded)
        {
            jumpCount = 1;
        }
        isJumping = !isGrounded;
        player.Jump(isJumping);
    }

    public void Jump()
    {
        if (jumpCount < maxJumps)
        {
            AudioManager.Instance.PlayJumpSound();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }

    public void MoveLeftStart()
    {
        moveLeft = true;
        player.Run(moveLeft);
    }

    public void MoveLeftStop()
    {
        moveLeft = false;
        player.Run(moveLeft);
    }

    public void MoveRightStart()
    {
        moveRight = true;
        player.Run(moveRight);
    }

    public void MoveRightStop()
    {
        moveRight = false;
        player.Run(moveRight);
    }

    public void EnterPipe()
    {
        enterPipe = true;
    }

    public void EnteredPipe()
    {
        enterPipe = false;
    }

    public void FireBall()
    {
        if (player.fireFlowerPower)
        {
            GameObject fireBall = fireBallPool.GetObject();
            fireBall.transform.position = firePoint.position;
            fireBall.transform.rotation = firePoint.rotation;

            AudioManager.Instance.PlayShootFireBallSound();

            float direction = transform.localScale.x > 0 ? 1f : -1f;
            fireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * 8f, 0);
        }
    }
}
