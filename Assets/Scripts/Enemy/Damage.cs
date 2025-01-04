using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;
        }
    }
}
