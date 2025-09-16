using UnityEngine;

public class BridgeDown : MonoBehaviour
{
    [SerializeField] private GameObject bridge;
    [SerializeField] private GameObject bridgeConnection;
    [SerializeField] private GameObject bowser;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bridge.GetComponent<BoxCollider2D>().size = new Vector2(bridge.GetComponent<BoxCollider2D>().size.x - 0.1f, 1);
            bridge.GetComponent<Rigidbody2D>().isKinematic = false;
            bridgeConnection.GetComponent<Rigidbody2D>().isKinematic = false;

            bowser.GetComponent<Bowser>().Hit();

            gameObject.SetActive(false);
        }
    }
}
