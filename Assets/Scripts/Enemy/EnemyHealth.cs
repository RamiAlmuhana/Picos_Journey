using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;
    public float maxHealth;
    private HealthBarController healthBarController;

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
            Destroy(gameObject);
        }
    }
}