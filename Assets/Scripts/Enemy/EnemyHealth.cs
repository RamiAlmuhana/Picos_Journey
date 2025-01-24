using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;
    public float maxHealth;
    private HealthBarController healthBarController;
    
    [SerializeField] private GameObject bombPrefab;

    void Start()
    {
        maxHealth = enemyHealth;
        healthBarController = GetComponentInChildren<HealthBarController>();
    }

    void Update()
    {
        if (healthBarController != null)
        {
            healthBarController.UpdateHealthBar(enemyHealth, maxHealth);
        }

        if (enemyHealth <= 0)
        {
            LevelProgressManager.Instance.EnemyDefeated();
            
            DropBomb();

            Destroy(gameObject);
        }
    }

    private void DropBomb()
    {
        if (bombPrefab != null)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Bomb prefab is not assigned!");
        }
    }
}