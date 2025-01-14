using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public GameObject player;
    public bool isPistol;
    public bool isShotgun;
    private GunRandomizer gunRandomizer;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        gunRandomizer = Object.FindFirstObjectByType<GunRandomizer>();
        if (gunRandomizer == null)
        {
            Debug.LogError("GunRandomizer not found in the scene.");
        }
        
        Debug.Log("PickupItem initialized for: " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("GeneratedWeapon") && gunRandomizer != null)
            {
                gunRandomizer.WeaponPickedUp();
            }
            
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

            if (isPistol)
            {
                playerMovement.hasPistol = true;
                playerMovement.hasAK = false;
                playerMovement.hasShotgun = false;
                Animator playerAnimator = player.GetComponent<Animator>();
                playerAnimator.SetBool("HasPistol", true);
                playerAnimator.SetBool("HasAK", false);
                playerAnimator.SetBool("HasShotgun", false);
                playerAnimator.SetBool("walkWithAK", false);
                playerAnimator.SetBool("walkWithShotgun", false);
            }
            else if (isShotgun)
            {
                playerMovement.hasShotgun = true;
                playerMovement.hasAK = false;
                playerMovement.hasPistol = false;
                Animator playerAnimator = player.GetComponent<Animator>();
                playerAnimator.SetBool("HasShotgun", true);
                playerAnimator.SetBool("HasPistol", false);
                playerAnimator.SetBool("HasAK", false);
                playerAnimator.SetBool("walkWithAK", false);
                playerAnimator.SetBool("walkWithPistol", false);
            }
            else
            {
                playerMovement.hasAK = true;
                playerMovement.hasPistol = false;
                Animator playerAnimator = player.GetComponent<Animator>();
                playerAnimator.SetBool("HasAK", true);
                playerAnimator.SetBool("HasPistol", false);
                playerAnimator.SetBool("HasShotgun", false);
                playerAnimator.SetBool("walkWithPistol", false);
                playerAnimator.SetBool("walkWithShotgun", false);
            }

            Destroy(gameObject);
        }
    }

}
