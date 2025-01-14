using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private HealthBarController healthBarController;
    private PlayerMovement playerMovement;

    [SerializeField]
    private AudioClip deathSound;

    private AudioSource audioSource;
    private bool isDead = false;

    void Start()
    {
        maxHealth = health;
        healthBarController = GetComponentInChildren<HealthBarController>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        
        if (healthBarController != null)
        {
            healthBarController.UpdateHealthBar(health, maxHealth);
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(HandleDeath()); 
        }
    }

    private IEnumerator HandleDeath()
    {
        PlayDeathSound();
        DisablePlayerMovement();
        HidePlayerAndHealthBar();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PlayDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    private void HidePlayerAndHealthBar()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        if (healthBarController != null)
        {
            var healthBarSpriteRenderer = healthBarController.spriteRenderer;
            if (healthBarSpriteRenderer != null)
            {
                healthBarSpriteRenderer.enabled = false;
            }
        }

    }

    private void DisablePlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.canMove = false;
        }
    }
}
