using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform telePoint;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player.transform.position.x >= transform.position.x)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (player.transform.position.x < transform.position.x - 10f)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = telePoint.position;
            Camera.main.GetComponent<SideScrolling>().transform.position = new Vector3(telePoint.position.x, Camera.main.GetComponent<SideScrolling>().transform.position.y, Camera.main.GetComponent<SideScrolling>().transform.position.z);
        }
    }
}
