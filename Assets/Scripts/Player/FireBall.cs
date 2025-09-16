using UnityEngine;

public class FireBall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<ObjectPool>().ReturnObject(gameObject);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<ObjectPool>().ReturnObject(gameObject);
        }
    }
}
