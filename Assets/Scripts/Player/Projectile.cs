using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float range = 15f;
    private float direction;
    private bool hit;
    private float traveledDistance;

    private BoxCollider2D boxCollider;
    
    public float damage;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * -direction;
        transform.Translate(movementSpeed, 0, 0);
        
        traveledDistance += Mathf.Abs(movementSpeed);
        
        if (traveledDistance >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;

        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null) 
        {
            if (enemyHealth.enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                enemyHealth.enemyHealth -= damage;
            }
        }

        Destroy(gameObject);
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = Mathf.Abs(transform.localScale.x) * _direction;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        
        traveledDistance = 0f;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}