using UnityEngine;

public class FireBreath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Hit();
            FindObjectOfType<ObjectPool>().ReturnObject(gameObject);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<ObjectPool>().ReturnObject(gameObject);
        }
    }
}

