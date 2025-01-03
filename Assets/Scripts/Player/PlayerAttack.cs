using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

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

        if (playerMovement.hasAK)
        {
            playerMovement.canAttack = true;
        }
        else
        {
            return;
        }
        
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.IsGrounded())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        // Spawn de kogel
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

}