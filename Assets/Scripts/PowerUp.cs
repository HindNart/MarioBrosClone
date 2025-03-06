using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Coin,
        _1upMushroom,
        MagicMushroom,
        Starpower,
        FireFlower,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                break;
            case Type._1upMushroom:
                GameManager.Instance.AddLife();
                break;
            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
            case Type.Starpower:
                player.GetComponent<Player>().Starpower();
                break;
            case Type.FireFlower:
                player.GetComponent<Player>().FireFlowerPower();
                break;
        }

        Destroy(gameObject);
    }
}
