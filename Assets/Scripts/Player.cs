using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer smallState;
    public SpriteRenderer bigState;
    private SpriteRenderer activeState;

    public Animator animSmall;
    public Animator animBig;

    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;

    public bool big;
    public bool starPower { get; private set; }
    public bool fireFlowerPower { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeState = smallState;
    }

    private void Update()
    {
        big = bigState.enabled;
    }

    public void Run(bool move)
    {
        if (smallState.enabled)
        {
            animSmall.SetBool("run", move);
        }
        else
        {
            animBig.SetBool("run", move);
        }
    }

    public void JumpEff()
    {
        rb.velocity = new Vector2(rb.velocity.x, 11 / 2);
    }

    public void Jump(bool jump)
    {
        if (smallState.enabled)
        {
            animSmall.SetBool("jump", jump);
        }
        else
        {
            animBig.SetBool("jump", jump);
        }
    }

    public void Hit()
    {
        if (!starPower)
        {
            if (big)
            {
                Shrink();
            }
            else
            {
                Death();
            }
        }
    }

    private void Death()
    {
        animSmall.SetTrigger("death");
        JumpEff();

        AudioManager.Instance.PlayMarioDieSound();

        fireFlowerPower = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().sortingLayerName = "MarioDie";

        StartCoroutine(GameManager.Instance.ResetLevelDelay(4f));
    }

    public void Grow()
    {
        AudioManager.Instance.PlayPowerUpSound();

        smallState.enabled = false;
        bigState.enabled = true;
        activeState = bigState;

        capsuleCollider.size = new Vector2(0.7489042f, 1.574623f);
        capsuleCollider.offset = new Vector2(0f, 0.2973117f);

        StartCoroutine(ScaleAnimation());
    }

    private void Shrink()
    {
        smallState.enabled = true;
        bigState.enabled = false;
        activeState = smallState;
        fireFlowerPower = false;

        capsuleCollider.size = new Vector2(0.7489042f, 0.9855137f);
        capsuleCollider.offset = new Vector2(0f, 0.002756834f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallState.enabled = !smallState.enabled;
                bigState.enabled = !smallState.enabled;
            }

            yield return null;
        }

        smallState.enabled = false;
        bigState.enabled = false;
        activeState.enabled = true;
    }

    public void Starpower()
    {
        AudioManager.Instance.PlayPowerUpSound();

        StartCoroutine(StarpowerAnimation(5f));
    }

    private IEnumerator StarpowerAnimation(float duration)
    {
        starPower = true;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                activeState.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activeState.color = Color.white;
        starPower = false;
    }

    public void FireFlowerPower()
    {
        AudioManager.Instance.PlayPowerUpSound();

        fireFlowerPower = true;
    }
}