using UnityEngine;

public class TornadoFollow : MonoBehaviour
{
    public float speed = 2f; // Snelheid waarmee de tornado beweegt
    public float damage = 10f; // Schade die de tornado toebrengt
    private Transform player; // Referentie naar de speler

    void Start()
    {
        // Zoek de speler op basis van de tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Controleer of de speler bestaat
        if (player != null)
        {
            // Bereken de horizontale richting naar de speler
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Controleer of de tornado de speler raakt
        if (collision.CompareTag("Player"))
        {
            // Verminder de gezondheid van de speler
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.health -= damage;
            }
        }
    }
}
