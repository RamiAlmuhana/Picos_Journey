using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound;
    private bool isCollected = false; // Controleer of het item al is opgepakt

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected) return; // Voorkom dubbele triggers
        if (collision.CompareTag("Player"))
        {
            isCollected = true; // Markeer als opgepakt
            if (collectSound != null)
            {
                PlaySoundAndDestroy(collectSound);
            }

            LevelProgressManager.Instance.CollectItem();
            Destroy(gameObject); // Vernietig het item
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