using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    
    [SerializeField] private GameObject akBulletPrefab;
    [SerializeField] private GameObject pistolBulletPrefab;
    [SerializeField] private GameObject shotgunBulletPrefab;
    
    [SerializeField] private float pistolCooldown = 0.5f;
    [SerializeField] private float akCooldown = 0.25f;
    [SerializeField] private float shotgunCooldown = 1f;
    
    [SerializeField] private AudioClip akSoundEffect;
    [SerializeField] private AudioClip pistolSoundEffect;
    [SerializeField] private AudioClip shotgunSoundEffect;
    
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

        if (playerMovement.hasAK || playerMovement.hasPistol || playerMovement.hasShotgun)
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
        else if (playerMovement.hasShotgun && (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)) && cooldownTimer > shotgunCooldown)
        {
            AttackShotgun();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void AttackAK()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        
        if (audioSource != null && akSoundEffect != null)
        {
            audioSource.PlayOneShot(akSoundEffect);
        }
        
        GameObject bullet = Instantiate(akBulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void AttackPistol()
    {
        anim.SetTrigger("attackPistol");
        cooldownTimer = 0;
        
        if (audioSource != null && pistolSoundEffect != null)
        {
            audioSource.PlayOneShot(pistolSoundEffect);
        }
        
        GameObject bullet = Instantiate(pistolBulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    
    private void AttackShotgun()
    {
        anim.SetTrigger("attackShotgun");
        cooldownTimer = 0;

        if (audioSource != null && shotgunSoundEffect != null)
        {
            audioSource.PlayOneShot(shotgunSoundEffect);
        }

        GameObject bullet = Instantiate(shotgunBulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        
    }

}
