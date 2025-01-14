using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float heal;
    public AudioClip healSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on the HealthPack. Please add one.");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            
            if (playerHealth != null && playerHealth.health < playerHealth.maxHealth)
            {
                playerHealth.health = Mathf.Min(playerHealth.health + heal, playerHealth.maxHealth);
                
                if (healSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(healSound);
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("You are full HP!");
            }
        }
    }
}