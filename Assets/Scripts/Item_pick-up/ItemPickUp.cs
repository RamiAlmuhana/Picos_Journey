using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound;
    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected) return;
        if (collision.CompareTag("Player"))
        {
            isCollected = true;
            if (collectSound != null)
            {
                PlaySoundAndDestroy(collectSound);
            }

            LevelProgressManager.Instance.CollectItem();
            Destroy(gameObject);
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