using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject Player;
    
    private Rigidbody2D rb;
    
    private float timer;

    public float force;
    
    public float damage;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        
        Vector3 direction = Player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
        
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
