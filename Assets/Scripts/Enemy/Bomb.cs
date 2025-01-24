using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionDelay = 2f;
    public float explosionRadius = 3f;
    public float damage = 50f;

    private void Start()
    {
        // Start een timer voor de explosie
        Invoke("Explode", explosionDelay);
    }

    private void Explode()
    {
        // Voeg logica toe voor schade of effecten
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerHealth>().health -= damage;
            }
        }

        // Speel een explosie-effect en verwijder de bom
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Teken de explosieradius in de editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}