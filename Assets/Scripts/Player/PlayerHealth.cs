using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private HealthBarController healthBarController;

    void Start()
    {
        maxHealth = health;
        healthBarController = GetComponentInChildren<HealthBarController>();
    }
    
    void Update()
    {
        if (healthBarController != null)
        {
            healthBarController.UpdateHealthBar(health, maxHealth);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.loadedSceneCount);
        }
    }
}
