using System.Collections;
using UnityEngine;

public class Bowser : MonoBehaviour
{
    public ObjectPool fireBreathPool;
    public Transform firePoint;
    public float fireInterval = 2f;

    private void Start()
    {
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
        StartCoroutine(FireRoutine());
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(fireInterval);
        }
    }

    private void Fire()
    {
        AudioManager.Instance.PlayBowserFireSound();
        GameObject fireBreath = fireBreathPool.GetObject();
        fireBreath.transform.position = firePoint.position;
        fireBreath.transform.rotation = firePoint.rotation;
        fireBreath.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * 5f, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

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

    public void Hit()
    {
        AudioManager.Instance.PlayBowserFallSound();
        GetComponent<Animator>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Bowser>().enabled = false;
        Destroy(gameObject, 2f);
    }
}
