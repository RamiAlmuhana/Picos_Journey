using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1.0f;
    [SerializeField] private float destroyDelay = 2.0f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(Fall), fallDelay);
        }
    }

    private void Fall()
    {
        rb.isKinematic = false;
        Destroy(gameObject, destroyDelay); 
    }
}