using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private Transform firePointLeft;
    [SerializeField] private Transform firePointRight;
    [SerializeField] private float fireInterval = 5f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
        GameObject bulletBill = bulletPool.GetObject();

        if (transform.position.x >= player.position.x)
        {
            bulletBill.transform.position = firePointLeft.position;
            bulletBill.transform.rotation = firePointLeft.rotation;
            bulletBill.transform.localScale = firePointLeft.localScale;
            bulletBill.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * 5f, 0);
        }
        else if (transform.position.x < player.position.x)
        {
            bulletBill.transform.position = firePointRight.position;
            bulletBill.transform.rotation = firePointRight.rotation;
            bulletBill.transform.localScale = firePointRight.localScale;
            bulletBill.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * 5f, 0);
        }
    }
}
