using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Attack variables
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject akBulletPrefab;
    [SerializeField] private GameObject pistolBulletPrefab;
    [SerializeField] private float pistolCooldown = 0.5f;
    [SerializeField] private float akCooldown = 0.25f;

    // Sound effect variables
    [SerializeField] private AudioClip akSoundEffect;
    [SerializeField] private AudioClip pistolSoundEffect;
    private AudioSource audioSource;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing on PlayerAttack GameObject!");
        }
    }

    private void Update()
    {
        if (playerMovement.canMove == false && playerMovement.canAttack == false)
        {
            return;
        }

        if (playerMovement.hasAK || playerMovement.hasPistol)
        {
            playerMovement.canAttack = true;
        }
        else
        {
            return;
        }

        if (playerMovement.hasPistol && (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)) && cooldownTimer > pistolCooldown)
        {
            AttackPistol();
        }
        else if (playerMovement.hasAK && (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)) && cooldownTimer > akCooldown)
        {
            AttackAK();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void AttackAK()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        // Play AK sound effect
        if (audioSource != null && akSoundEffect != null)
        {
            audioSource.PlayOneShot(akSoundEffect);
        }

        // Spawn AK bullet
        GameObject bullet = Instantiate(akBulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void AttackPistol()
    {
        anim.SetTrigger("attackPistol");
        cooldownTimer = 0;

        // Play Pistol sound effect
        if (audioSource != null && pistolSoundEffect != null)
        {
            audioSource.PlayOneShot(pistolSoundEffect);
        }

        // Spawn Pistol bullet
        GameObject bullet = Instantiate(pistolBulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
