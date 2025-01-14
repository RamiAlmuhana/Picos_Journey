using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float heal;
    public AudioClip healSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null && playerHealth.health < playerHealth.maxHealth)
            {
                playerHealth.health = Mathf.Min(playerHealth.health + heal, playerHealth.maxHealth);
                
                if (healSound != null)
                {
                    PlaySoundAndDestroy(healSound);
                }

                Destroy(gameObject);
            }
        }
    }

    private void PlaySoundAndDestroy(AudioClip clip)
    {
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
        
        audioSource.clip = clip;
        audioSource.Play();
        
        Destroy(tempAudio, clip.length);
    }
}