using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject akBulletPrefab;
    [SerializeField] private GameObject pistolBulletPrefab;
    [SerializeField] private float pistolCooldown = 0.5f;
    [SerializeField] private float akCooldown = 0.25f;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
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
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        GameObject bullet = Instantiate(akBulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    
    private void AttackPistol()
    {
        anim.SetTrigger("attackPistol");
        cooldownTimer = 0;

        GameObject bullet = Instantiate(pistolBulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }


}