using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public GameObject player; // Sleep hier het spelerobject in de Inspector
    public bool isPistol; // Dit maakt het pickup-item een pistool of iets anders

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            
            if (isPistol)
            {
                playerMovement.hasPistol = true;
                playerMovement.hasAK = false;
                Animator playerAnimator = player.GetComponent<Animator>();
                playerAnimator.SetBool("HasPistol", true);
                playerAnimator.SetBool("HasAK", false);
                playerAnimator.SetBool("walkWithAK", false);
            }
            else
            {
                playerMovement.hasAK = true;
                playerMovement.hasPistol = false;
                Animator playerAnimator = player.GetComponent<Animator>();
                playerAnimator.SetBool("HasAK", true);
                playerAnimator.SetBool("HasPistol", false);
                playerAnimator.SetBool("walkWithPistol", false);
            }
            
            Destroy(gameObject);
        }
    }
}
