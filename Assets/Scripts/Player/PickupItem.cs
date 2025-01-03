using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public GameObject player; // Sleep hier het spelerobject in de Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.hasAK = true;
            
            Animator playerAnimator = player.GetComponent<Animator>();
            playerAnimator.SetBool("HasAK", true);
            
            Destroy(gameObject);
        }
    }
}