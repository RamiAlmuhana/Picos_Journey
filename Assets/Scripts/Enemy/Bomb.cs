using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionDelay = 2f;
    public float explosionRadius = 3f;
    public float damage = 50f;

    private void Start()
    {
        Invoke("Explode", explosionDelay);
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerHealth>().health -= damage;
            }
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}